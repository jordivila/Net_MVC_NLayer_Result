using System.Web.Mvc;

namespace VsixMvcAppResult.UI.Web.Areas.UserProfile
{
    public static class UrlHelperUserProfile
    {
        public static string UserProfile_View(this UrlHelper helper)
        {
            return helper.Action("Index", "UserProfile", new { Area = VsixMvcAppResult.UI.Web.Areas.UserProfile.UserProfileAreaRegistration.UserProfileAreaName });
        }
        public static string UserProfile_Edit(this UrlHelper helper)
        {
            return helper.Action("Edit", "UserProfile", new { Area = VsixMvcAppResult.UI.Web.Areas.UserProfile.UserProfileAreaRegistration.UserProfileAreaName });
        }
    }
}