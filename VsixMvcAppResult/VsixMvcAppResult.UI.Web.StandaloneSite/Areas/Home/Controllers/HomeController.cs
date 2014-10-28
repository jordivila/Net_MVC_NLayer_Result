using System;
using System.Web.Mvc;
using VsixMvcAppResult.Models.Enumerations;
using VsixMvcAppResult.Models.Globalization;
using VsixMvcAppResult.Resources.General;
using VsixMvcAppResult.UI.Web.Areas.Home.Models;
using VsixMvcAppResult.UI.Web.Common.Mvc.Html;
using VsixMvcAppResult.UI.Web.Controllers;
using VsixMvcAppResult.UI.Web.Models;
using System.Linq;

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
        public ActionResult ThemeSelect()
        {
            ThemeSelectModel model = new ThemeSelectModel();
            model.ThemesMenu = new MenuModel();
            model.ThemesMenu.MenuItems.AddRange(MenuExtensions.MenuDetailed((baseViewModel)model, this.Url).MenuItems.Where(x => x.Description == GeneralTexts.SiteThemes).First().Childs);
            model.BaseViewModelInfo.Title = GeneralTexts.Dashboard;
            return View(model);
        }


        public ActionResult CultureSet(string id)
        {
            MvcApplication.UserRequest.UserProfile.Culture = GlobalizationHelper.CultureInfoGetOrDefault(id);
            MvcApplication.UserRequest.UserProfile.ApplyClientProperties();
            return this.SettingsApplied();
        }

        public ActionResult CultureSelect()
        {
            CultureSelectModel model = new CultureSelectModel();
            model.CulturesMenu = new MenuModel();
            model.CulturesMenu.MenuItems.AddRange(MenuExtensions.MenuDetailed((baseViewModel)model, this.Url).MenuItems.Where(x => x.Description == GeneralTexts.Languages).First().Childs);
            model.BaseViewModelInfo.Title = GeneralTexts.Dashboard;
            return View(model);
        }

    }
}