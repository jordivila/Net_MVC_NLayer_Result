using System.Web.Mvc;
using Microsoft.Practices.Unity;
using VsixMvcAppResult.Models.Configuration.ConfigSections.ClientResources;
using VsixMvcAppResult.Models.Profile;
using VsixMvcAppResult.Models.Unity;
using VsixMvcAppResult.UI.Web.Areas.UserProfile.Models;
using VsixMvcAppResult.UI.Web.Controllers;

namespace VsixMvcAppResult.UI.Web.Areas.UserProfile.Controllers
{
    [VsixMvcAppResult.UI.Web.Common.Mvc.Attributes.Authorize]
    public class UserProfileController : Controller, IControllerWithClientResources
    {
        protected override void Dispose(bool disposing)
        {
            if (this.ProviderProfile != null)
            {
                this.ProviderProfile.Dispose();
            }

            base.Dispose(disposing);
        }

        public string[] GetControllerJavascriptResources
        {
            get { return new string[0]; }
        }

        public string[] GetControllerStyleSheetResources
        {
            get { return new string[0]; }
        }

        private IProfileProxy ProviderProfile;

        public UserProfileController()
        {
            this.ProviderProfile = DependencyFactory.Resolve<IProfileProxy>();
        }

        [HttpGet]
        public ActionResult Index()
        {
            UserProfileIndexModel model = new UserProfileIndexModel();
            model.BaseViewModelInfo.Title = Resources.Account.AccountResources.UserProfile;
            model.UserProfileResult = this.ProviderProfile.Get();
            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Index(UserProfileIndexModel model)
        //{
        //    model.BaseViewModelInfo.Title = Resources.Account.AccountResources.UserProfile;
        //    model.UserProfileResult = this.ProviderProfile.Get();
        //    return View(model);
        //}


        [HttpGet]
        public ActionResult Edit()
        {
            UserProfileIndexModel model = new UserProfileIndexModel();
            model.BaseViewModelInfo.Title = Resources.Account.AccountResources.UserProfile;

            DataResultUserProfile userProfileResult = this.ProviderProfile.Get();
            model.UserProfileResult = userProfileResult;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfileIndexModel model)
        {
            model.BaseViewModelInfo.Title = Resources.Account.AccountResources.UserProfile;

            if (ModelState.IsValid)
            {
                // Set Culture & Theme values currently in use
                model.UserProfileResult.Data.CultureName = MvcApplication.UserRequest.UserProfile.CultureName;
                model.UserProfileResult.Data.Theme = MvcApplication.UserRequest.UserProfile.Theme;

                DataResultUserProfile result = this.ProviderProfile.Update(model.UserProfileResult.Data);
                model.UserProfileResultUpdated = result;
                MvcApplication.UserRequest.UserProfile = result.Data;
                MvcApplication.UserRequest.UserProfile.ApplyClientProperties();
                model.BaseViewModelInfo.LocalizationResources = new LocalizationResourcesHelper(MvcApplication.UserRequest.UserProfile.Culture);
            }
            return View(model);
        }

    }

}