using System.Web.Mvc;

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
        public static string Home_ThemeSet(this UrlHelper helper, string theme)
        {
            return helper.Action("ThemeSet", "Home", new { Area = HomeAreaRegistration.HomeAreaName, theme = theme });
        }
        public static string Home_CultureSet(this UrlHelper helper, string culture)
        {
            return helper.Action("CultureSet", "Home", new { Area = HomeAreaRegistration.HomeAreaName, culture = culture });
        }
    }
}