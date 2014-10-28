using System.Collections.Generic;
using VsixMvcAppResult.Models.Membership;
using VsixMvcAppResult.UI.Web.Models;

namespace VsixMvcAppResult.UI.Web.Areas.UserAdministration.Models
{
    public class RoleViewModel : baseViewModel
    {
        public string Role { get; set; }
        public IEnumerable<MembershipUserWrapper> Users { get; set; }
    }
}