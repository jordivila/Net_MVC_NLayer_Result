using System;
using System.Web.Mvc;
using VsixMvcAppResult.Models.Enumerations;
using VsixMvcAppResult.Models.Globalization;
using VsixMvcAppResult.Resources.General;
using VsixMvcAppResult.UI.Web.Controllers;
using VsixMvcAppResult.UI.Web.Models;

namespace VsixMvcAppResult.UI.Web.Areas.Home.Controllers
{
    [Serializable]
    public class HomeController : Controller, IControllerWithClientResources
    {
        public string[] GetControllerJavascriptResources
        {
            get { return new string[0]; }
        }

        public string[] GetControllerStyleSheetResources
        {
            get { return new string[0]; }
        }

        public ActionResult Index()
        {
            baseViewModel model = new baseViewModel();
            model.BaseViewModelInfo.Title = "Asp.Net MVC N-Tier application template";
            return View(model);
        }

        public ActionResult About()
        {
            baseViewModel model = new baseViewModel();
            model.BaseViewModelInfo.Title = GeneralTexts.About;
            return View(model);
        }

        private ActionResult SettingsApplied()
        {
            baseViewModel model = new baseViewModel();
            model.BaseViewModelInfo.Title = GeneralTexts.SettingsApplied;
            return View(model);
        }

        public ActionResult ThemeSet(string id)
        {
            ThemesAvailable themeSelected = (ThemesAvailable)Enum.Parse(typeof(ThemesAvailable), id);
            MvcApplication.UserRequest.UserProfile.Theme = themeSelected;
            MvcApplication.UserRequest.UserProfile.ApplyClientProperties();
            return this.SettingsApplied();
        }

        public ActionResult CultureSet(string id)
        {
            MvcApplication.UserRequest.UserProfile.Culture = GlobalizationHelper.CultureInfoGetOrDefault(id);
            MvcApplication.UserRequest.UserProfile.ApplyClientProperties();
            return this.SettingsApplied();
        }

    }
}