﻿using System.Web.Mvc;
using VsixMvcAppResult.Models.Common;
using VsixMvcAppResult.Models.Globalization;

namespace VsixMvcAppResult.UI.Web.Controllers
{
    //public class CultureController : Controller
    //{
    //    [HttpPost]
    //    public JsonResult Set(string culture)
    //    {
    //        //CulturesAvailable cultureSelected = (CulturesAvailable)Enum.Parse(typeof(CulturesAvailable), culture.Replace("-", "_"));

    //        MvcApplication.UserRequest.UserProfile.Culture = GlobalizationHelper.CultureInfoGetOrDefault(culture);
    //        MvcApplication.UserRequest.UserProfile.ApplyClientProperties();

    //        DataResultBoolean result = new DataResultBoolean();
    //        result.IsValid = true;
    //        result.Data = true;
    //        return Json(result);
    //    }
    //}
}
