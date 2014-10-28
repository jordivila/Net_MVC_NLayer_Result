using VsixMvcAppResult.Models.Profile;
using VsixMvcAppResult.UI.Web.Models;
using VsixMvcAppResult.UI.Web.Common.Mvc.Attributes;

namespace VsixMvcAppResult.UI.Web.Areas.UserProfile.Models
{
    [NonValidateModelOnHttpGet]
    public class UserProfileIndexModel : baseViewModel
    {
        public DataResultUserProfile UserProfileResult { get; set; }
        public DataResultUserProfile UserProfileResultUpdated { get; set; }
    }
}