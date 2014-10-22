using System.Web.Mvc;

namespace VsixMvcAppResult.UI.Web.Areas.Test
{
    public static class TestsUrlHelper
    {
        public static string Index(this UrlHelper helper)
        {
            return helper.Action("Index", "Test", new { Area = TestAreaRegistration.TestAreaName });
        }
    }
}