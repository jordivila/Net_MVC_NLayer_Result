using System;
using System.Web.Mvc;
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
            model.BaseViewModelInfo.Title = "About";
            return View(model);
        }
    }
}
