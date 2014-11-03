using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using VsixMvcAppResult.Models.Authentication;
using VsixMvcAppResult.Models.Common;
using VsixMvcAppResult.Models.Membership;
using VsixMvcAppResult.Models.Profile;
using VsixMvcAppResult.Models.Unity;
using VsixMvcAppResult.Resources.Account;
using VsixMvcAppResult.Resources.General;
using VsixMvcAppResult.UI.Web.Areas.Home;
using VsixMvcAppResult.UI.Web.Areas.UserAccount.Models;
using VsixMvcAppResult.UI.Web.Common.Mvc.Html;
using VsixMvcAppResult.UI.Web.Controllers;
using VsixMvcAppResult.UI.Web.Models;

namespace VsixMvcAppResult.UI.Web.Areas.UserAccount.Controllers
{
    public class UserAccountController : Controller, IControllerWithClientResources
    {
        public UserAccountController()
        {
            this.FormsAuthenticationService = DependencyFactory.Resolve<IAuthenticationProxy>();
            this.FormsMembershipService = DependencyFactory.Resolve<IMembershipProxy>();
            this.FormsProfileService = DependencyFactory.Resolve<IProfileProxy>();
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
                return new string[0];
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (this.FormsAuthenticationService != null)
            {
                this.FormsAuthenticationService.Dispose();
            }

            if (this.FormsMembershipService != null)
            {
                this.FormsMembershipService.Dispose();
            }

            if (this.FormsProfileService != null)
            {
                this.FormsProfileService.Dispose();
            }

            base.Dispose(disposing);
        }

        public IAuthenticationProxy FormsAuthenticationService;
        public IMembershipProxy FormsMembershipService;
        public IProfileProxy FormsProfileService;

        public RedirectResult RedirectResultOnLogIn()
        {
            return new RedirectResult(Url.Account_Dashboard());
        }

        #region Login / Logout
        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogOn()
        {
            LogOnViewModel model = new LogOnViewModel();
            model.BaseViewModelInfo.Title = VsixMvcAppResult.Resources.General.GeneralTexts.LogOn;
            model.UrlReferer = Request.UrlReferrer == null ? string.Empty : Request.UrlReferrer.ToString();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogOn(LogOnViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isValidUser = this.FormsAuthenticationService.LogIn(model.Email, model.Password, string.Empty, false); //model.RememberMe);
                if (isValidUser)
                {
                    this.FormsProfileService.Get().Data.ApplyClientProperties();

                    if (Url.IsLocalUrl(model.UrlReferer))   // Prevents Open Redirection Attacks
                    {
                        return new RedirectResult(model.UrlReferer);
                    }
                    else
                    {
                        return this.RedirectResultOnLogIn();
                    }
                }
                else
                {
                    ModelState.AddModelError("Invalid_Credentials", AccountResources.UserNameOrPasswordInvalid);
                }
            }
            model.BaseViewModelInfo.Title = VsixMvcAppResult.Resources.General.GeneralTexts.LogOn;
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthenticationService.LogOut();
            return new RedirectResult(Url.Home_Index());
        }
        #endregion

        #region Register

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            model.BaseViewModelInfo.Title = Resources.Account.AccountResources.Register;
            model.PasswordLength = this.FormsMembershipService.Settings().Data.MinRequiredPasswordLength;
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                DataResultUserCreateResult result = FormsMembershipService.CreateUser(model.Email, model.Password, model.Email, string.Empty, string.Empty, Url.Account_Activate());
                if (result.IsValid)
                {
                    model.Result = result.Data;

                    if (model.Result.CreateStatus == MembershipCreateStatus.Success)
                    {

                    }
                    else
                    {
                        ModelState.AddModelError(((int)model.Result.CreateStatus).ToString(), AccountValidation.ErrorCodeToString(model.Result.CreateStatus));
                    }
                }
                else
                {
                    ModelState.AddModelError("0", result.Message);
                }
            }

            model.BaseViewModelInfo.Title = Resources.Account.AccountResources.Register;
            model.PasswordLength = this.FormsMembershipService.Settings().Data.MinRequiredPasswordLength;
            return View(model);
        }
        #endregion

        #region CantAccessYourAccount

        [HttpGet]
        public ActionResult CantAccessYourAccount()
        {
            CantAccessYourAccountViewModel model = new CantAccessYourAccountViewModel();
            model.BaseViewModelInfo.Title = VsixMvcAppResult.Resources.Account.AccountResources.CanNotAccessYourAccount;
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CantAccessYourAccount(CantAccessYourAccountViewModel model)
        {
            model.BaseViewModelInfo.Title = VsixMvcAppResult.Resources.Account.AccountResources.CanNotAccessYourAccount;
            if (ModelState.IsValid)
            {
                DataResultUserCantAccess result = this.FormsMembershipService.CantAccessYourAccount(Url.Account_ResetPassword(), model.EmailAddress);
                model.Result = result;

                if (result.IsValid)
                {

                }
                else
                {
                    ModelState.AddModelError("0", result.Message);
                }
            }
            return View(model);
        }

        #endregion

        #region ResetPassword

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPassword(object id)
        {
            ResetPasswordClientModel model = new ResetPasswordClientModel();
            model.BaseViewModelInfo.Title = VsixMvcAppResult.Resources.Account.AccountResources.ChangePassword;
            model.MinPasswordLength = this.FormsMembershipService.Settings().Data.MinRequiredPasswordLength;
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(object id, ResetPasswordClientModel model)
        {
            model.BaseViewModelInfo.Title = VsixMvcAppResult.Resources.Account.AccountResources.ChangePassword;
            model.MinPasswordLength = this.FormsMembershipService.Settings().Data.MinRequiredPasswordLength;

            if (ModelState.IsValid)
            {
                DataResultUserCantAccess result = this.FormsMembershipService.ResetPassword(Guid.Parse((string)id), model.NewPassword, model.ConfirmPassword);
                model.Result = new DataResultBoolean()
                {
                    IsValid = result.IsValid,
                    Message = result.Message,
                    MessageType = result.MessageType,
                    Data = result.IsValid
                };
                if (result.IsValid)
                {
                    return this.RedirectResultOnLogIn();
                }
                else
                {
                    ModelState.AddModelError("InvalidPassword", result.Message);
                }
            }
            return View(model);
        }

        #endregion

        #region ChangePassword

        private ChangePasswordViewModel ChangePassword_setCommonValues(ChangePasswordViewModel modelTmp)
        {
            modelTmp.BaseViewModelInfo.Title = VsixMvcAppResult.Resources.Account.AccountResources.ChangePassword;
            modelTmp.MinPasswordLength = this.FormsMembershipService.Settings().Data.MinRequiredPasswordLength;
            return modelTmp;
        }

        [HttpGet]
        [VsixMvcAppResult.UI.Web.Common.Mvc.Attributes.Authorize]
        public ActionResult ChangePassword()
        {
            ChangePasswordViewModel model = ChangePassword_setCommonValues(new ChangePasswordViewModel());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [VsixMvcAppResult.UI.Web.Common.Mvc.Attributes.Authorize]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            model = ChangePassword_setCommonValues(model);

            if (ModelState.IsValid)
            {
                FormsIdentity identity = this.FormsAuthenticationService.GetFormsIdentity();
                DataResultBoolean result = this.FormsMembershipService.ChangePassword(identity.Name, model.OldPassword, model.NewPassword, model.ConfirmPassword);
                if (result.IsValid)
                {
                    model.Result = result;
                }
                else
                {
                    ModelState.AddModelError("InvalidPassword", result.Message);
                }
            }
            return View(model);
        }

        #endregion

        #region Activation
        public ActionResult Activate(object id)
        {
            AccountActivationClientModel model = new AccountActivationClientModel();
            model.ActivateUserToken = Guid.Parse((string)id);
            DataResultUserActivate result = this.FormsMembershipService.ActivateAccount(Guid.Parse((string)id));
            if (result.IsValid)
            {
                return this.RedirectResultOnLogIn();
            }
            else
            {
                model.Result = result;
                return View(model);
            }
        }
        #endregion

        #region DashBoard
        [HttpGet]
        [VsixMvcAppResult.UI.Web.Common.Mvc.Attributes.Authorize]
        public ActionResult Dashboard()
        {
            DashBoardViewModel model = new DashBoardViewModel();
            model.UserProfile = this.FormsProfileService.Get().Data;
            model.DashboardMenu = new MenuModel();
            model.DashboardMenu.MenuItems.AddRange(MenuExtensions.MenuDetailed((baseViewModel)model, this.Url).MenuItems.Where(x => x.DataAction == UserAccountUrlHelper.Account_Dashboard(this.Url)).First().Childs);
            model.BaseViewModelInfo.Title = GeneralTexts.Dashboard;
            return View(model);
        }
        #endregion
    }
}
