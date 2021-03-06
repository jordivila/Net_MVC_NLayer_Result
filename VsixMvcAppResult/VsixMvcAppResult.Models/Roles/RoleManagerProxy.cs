﻿using VsixMvcAppResult.Models.Common;
using VsixMvcAppResult.Models.ProxyProviders;



namespace VsixMvcAppResult.Models.Roles
{
    public class RoleManagerProxy : ProviderBaseChannel<IRoleManagerProxy>, IRoleManagerProxy
    {
        public DataResultBoolean AddToRoles(string userName, string[] roles)
        {
            return this.proxy.AddToRoles(userName, roles);
        }
        public DataResultBoolean Create(string roleName)
        {
            return this.proxy.Create(roleName);
        }
        public DataResultBoolean Delete(string roleName)
        {
            return this.proxy.Delete(roleName);
        }
        public DataResultStringArray FindAll()
        {
            return this.proxy.FindAll();
        }
        public DataResultStringArray FindByUserName(string userName)
        {
            return this.proxy.FindByUserName(userName);
        }
        public DataResultStringArray FindUserNamesByRole(string roleName)
        {
            return this.proxy.FindUserNamesByRole(roleName);
        }
        public DataResultBoolean IsInRole(string user, string roleName)
        {
            return this.proxy.IsInRole(user, roleName);
        }
        public DataResultBoolean RemoveFromRoles(string user, string[] roles)
        {
            return this.proxy.RemoveFromRoles(user, roles);
        }
        public DataResultBoolean RoleExists(string roleName)
        {
            return this.proxy.RoleExists(roleName);
        }
        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
