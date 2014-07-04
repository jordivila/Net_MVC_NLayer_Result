using VsixMvcAppResult.Models.ProxyProviders;

namespace VsixMvcAppResult.Models.Roles
{
    public class RolesProxy : ProviderBaseChannel<IRolesProxy>, IRolesProxy
    {
        public string[] GetRolesForCurrentUser()
        {
            return proxy.GetRolesForCurrentUser();
        }
        public bool IsCurrentUserInRole(string roleName)
        {
            return proxy.IsCurrentUserInRole(roleName);
        }
        public override void Dispose()
        {
            base.Dispose();
        }
    }
}