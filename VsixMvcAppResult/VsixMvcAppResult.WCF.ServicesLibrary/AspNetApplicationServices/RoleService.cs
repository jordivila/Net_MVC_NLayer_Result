using System.Linq;
using VsixMvcAppResult.BL.MembershipServices;
using VsixMvcAppResult.Models.Roles;

namespace VsixMvcAppResult.WCF.ServicesLibrary.AspNetApplicationServices
{
    public class RoleService : BaseServiceWithCustomMessageHeaders, IRolesProxy
    {
        IRoleAdminBL _bl = null;

        public RoleService()
        {
            this._bl = new RoleAdminBL();
        }

        public override void Dispose()
        {
            if (this._bl != null)
            {
                this._bl.Dispose();
            }

            base.Dispose();
        }
        public string[] GetRolesForCurrentUser()
        {
            return this._bl.GetRolesForCurrentUser();
        }
        public bool IsCurrentUserInRole(string roleName)
        {
            return this._bl.IsCurrentUserInRole(roleName);
        }
    }
}