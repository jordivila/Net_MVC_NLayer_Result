﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using VsixMvcAppResult.Models.Common;
using VsixMvcAppResult.Models.Enumerations;
using VsixMvcAppResult.Models.Membership;
using VsixMvcAppResult.Models.Roles;
using VsixMvcAppResult.Models.Unity;
using VsixMvcAppResult.Resources.General;
using VsixMvcAppResult.Resources.UserAdministration;
using VsixMvcAppResult.UI.Web.Areas.UserAdministration.Models;
using VsixMvcAppResult.UI.Web.Controllers;
using VsixMvcAppResult.UI.Web.Common.Mvc.Html;


namespace VsixMvcAppResult.UI.Web.Areas.UserAdministration.Controllers
{
    [VsixMvcAppResult.UI.Web.Common.Mvc.Attributes.Authorize(Roles = SiteRoles.Administrator)]
    public class UserAdministrationController : Controller, IControllerWithClientResources
    {
        private IMembershipProxy _providerMembership;
        private IRoleManagerProxy _providerRoles;

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (_providerMembership != null) { this._providerMembership.Dispose(); }
            if (_providerRoles != null) { this._providerRoles.Dispose(); }
        }

        public string[] GetControllerJavascriptResources
        {
            get
            {
                return new string[0];
            }
        }
        public string[] GetControllerStyleSheetResources
        {
            get
            {
                return new string[1] { "~/Areas/UserAdministration/Content/UserAdmin.css" };
            }
        }

        public UserAdministrationController()
        {
            this._providerMembership = DependencyFactory.Resolve<IMembershipProxy>();
            this._providerRoles = DependencyFactory.Resolve<IRoleManagerProxy>();

            if (!ModelBinders.Binders.ContainsKey(typeof(DetailsViewModel)))
            {
                ModelBinders.Binders.Add(typeof(DetailsViewModel), new DetailsViewModelBinder());
            }
        }

        [HttpGet]
        public ViewResult Index()
        {
            ViewResult result = null;

            IndexViewModel model = new IndexViewModel();
            model.Action = Actions.Search;
            model.Filter = MvcApplication.UserSession.UserAdministrationController_LastSearch;
            if (model.Filter == null)
            {
                model.Filter = new DataFilterUserList()
                {
                    IsClientVisible = true,
                    PageSize = (int)PageSizesAvailable.RowsPerPage10,
                };
                model.Filter.IsInRoleName.AddRange(_providerRoles.FindAll().Data);
                result = View(model);
            }
            else
            {
                result = this.Index_Search(model);
            }
            model.BaseViewModelInfo.Title = GeneralTexts.UserAdmin;
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ViewResult Index(IndexViewModel model)
        {
            ViewResult result = null;

            if (WebGrid<MembershipUserWrapper, IndexViewModel, DataFilterUserList>.IsWebGridEvent())
            {
                this.ModelState.Clear();
                model.Filter = (DataFilterUserList)WebGrid<MembershipUserWrapper, IndexViewModel, DataFilterUserList>.GetDataFilterFromPost();
                MvcApplication.UserSession.UserAdministrationController_LastSearch = model.Filter;
            }

            if (this.ModelState.IsValid)
            {
                switch (model.Action)
                {
                    case Actions.Search:
                        model.Filter.Page = 0;
                        model.Filter.SortBy = string.Empty;
                        model.Filter.SortAscending = true;
                        model.Filter.IsClientVisible = false;
                        MvcApplication.UserSession.UserAdministrationController_LastSearch = model.Filter;
                        break;
                    default:
                        break;
                }

                result = this.Index_Search(model);
            }
            else
            {
                model.Filter.IsClientVisible = true;
                result = View(model);
            }

            model.BaseViewModelInfo.Title = GeneralTexts.UserAdmin;
            return result;
        }

        private ViewResult Index_Search(IndexViewModel model)
        {
            DataResultUserSearch resultSearch = this._providerMembership.GetUserList(model.Filter);
            model.Filter.IsClientVisible = false;
            if (resultSearch.IsValid)
            {
                model.UserListResult = resultSearch.Data;
                if (model.UserListResult.TotalRows == 0)
                {
                    model.Filter.IsClientVisible = true;
                }
            }
            else
            {
                ModelState.AddModelError("0", resultSearch.Message);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(object id)
        {
            return this.Details(id, new DetailsViewModel(), Actions.ViewDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(object id, DetailsViewModel model, Actions formAction = Actions.ViewDetail)
        {
            switch (formAction)
            {
                case Actions.ViewDetail:
                    var user = _providerMembership.GetUserByGuid(Guid.Parse((string)id), false).Data;
                    var userRoles = _providerRoles.FindByUserName(user.UserName);

                    model = new DetailsViewModel()
                    {
                        UserOriginal = user,
                        UserUpdated = user,
                        UserRoles = userRoles.Data,
                        Roles = _providerRoles.FindAll().Data
                    };
                    break;
                case Actions.Approve:
                    model = this.Details_ChangeAproval(model, model.UserOriginal, true);
                    break;
                case Actions.UnApprove:
                    model = this.Details_ChangeAproval(model, model.UserOriginal, false);
                    break;
                case Actions.UnLock:
                    model = this.Details_UnLock(model, model.UserOriginal, formAction);
                    break;
                case Actions.Delete:
                    DataResultBoolean result = _providerMembership.DeleteUser(model.UserOriginal.UserName, true);
                    if (result.Data)
                    {
                        return Redirect(UrlHelperUserAdmin.UserAdminIndex(Url));
                    }
                    else
                    {
                        ModelState.AddModelError(formAction.ToString(), result.Message);
                    }
                    break;
                case Actions.Update:
                    //update roles
                    this.Details_UpdateRoles(model, Actions.Update);
                    //update comment
                    model.UserOriginal.Comment = model.UserUpdated.Comment;
                    model = this.Details_Update(model, model.UserOriginal, formAction);
                    break;
                default:
                    break;
            }

            model.BaseViewModelInfo.Title = GeneralTexts.UserAdmin;
            model.BaseViewModelInfo.Breadcrumb.IsVisible = true;
            model.BaseViewModelInfo.Breadcrumb.BreadcrumbPaths.AddRange(new List<KeyValuePair<string, string>>() { 
                new KeyValuePair<string, string>(UserAdminTexts.UserSearch, Url.UserAdminIndex())
            });

            return View(model);
        }


        private DetailsViewModel Details_ChangeAproval(DetailsViewModel model, MembershipUserWrapper user, bool IsApproved)
        {
            user.IsApproved = IsApproved;
            return this.Details_Update(model, user, user.IsApproved ? Actions.Approve : Actions.UnApprove);
        }
        private DetailsViewModel Details_Update(DetailsViewModel model, MembershipUserWrapper user, Actions formAction)
        {
            DataResultBoolean result = this._providerMembership.UpdateUser(user);
            if (result.Data)
            {
                model.ResultLastAction = new DataResultBoolean()
                {
                    IsValid = true,
                    MessageType = DataResultMessageType.Success,
                    Message = result.Message
                };
            }
            else
            {
                ModelState.AddModelError(formAction.ToString(), result.Message);
            }
            return model;
        }
        private DetailsViewModel Details_UpdateRoles(DetailsViewModel model, Actions formAction)
        {
            DataResultBoolean result;

            model.UserRoles = model.UserRoles == null ? new string[0] : model.UserRoles;

            if (model.UserRoles != null && model.UserRoles.Count() > 0)
            {
                result = _providerRoles.AddToRoles(model.UserOriginal.UserName, model.UserRoles.ToArray());
                if (!result.Data)
                {
                    ModelState.AddModelError(formAction.ToString(), result.Message);
                }
            }

            string[] rolesToRemove = model.Roles.Where(r => !model.UserRoles.Contains(r)).ToArray();
            if (rolesToRemove != null && rolesToRemove.Count() > 0)
            {
                result = _providerRoles.RemoveFromRoles(model.UserOriginal.UserName, rolesToRemove);
                if (!result.Data)
                {
                    ModelState.AddModelError(formAction.ToString(), result.Message);
                }
            }

            return model;
        }
        private DetailsViewModel Details_UnLock(DetailsViewModel model, MembershipUserWrapper user, Actions formAction)
        {
            DataResultBoolean result = this._providerMembership.UnlockUser(user.UserName);
            if (result.Data)
            {
                model.UserOriginal.IsLockedOut = false;
                model.ResultLastAction = new DataResultBoolean()
                {
                    IsValid = true,
                    MessageType = result.MessageType,
                    Message = result.Message
                };
            }
            else
            {
                ModelState.AddModelError(formAction.ToString(), result.Message);
            }
            return model;
        }
    }
}
