﻿using System.Web.Mvc;

namespace VsixMvcAppResult.UI.Web.Areas.Home
{
    public static class HomeUrlHelper
    {
        public static string Home_Index(this UrlHelper helper)
        {
            return helper.Action("Index", "Home", new { Area = HomeAreaRegistration.HomeAreaName });
        }
        public static string Home_About(this UrlHelper helper)
        {
            return helper.Action("About", "Home", new { Area = HomeAreaRegistration.HomeAreaName });
        }
        public static string Home_ThemeSet(this UrlHelper helper, string id)
        {
            return helper.Action("ThemeSet", "Home", new { Area = HomeAreaRegistration.HomeAreaName, id = id });
        }
        public static string Home_ThemeSelect(this UrlHelper helper)
        {
            return helper.Action("ThemeSelect", "Home", new { Area = HomeAreaRegistration.HomeAreaName });
        }
        public static string Home_CultureSet(this UrlHelper helper, string id)
        {
            return helper.Action("CultureSet", "Home", new { Area = HomeAreaRegistration.HomeAreaName, id = id });
        }
        public static string Home_CultureSelect(this UrlHelper helper)
        {
            return helper.Action("CultureSelect", "Home", new { Area = HomeAreaRegistration.HomeAreaName });
        }


    }
}