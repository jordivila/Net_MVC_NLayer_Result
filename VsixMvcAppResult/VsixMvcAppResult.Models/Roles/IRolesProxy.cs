using System;
using System.ServiceModel;

namespace VsixMvcAppResult.Models.Roles
{
    [ServiceContract]
    public interface IRolesProxy : IDisposable
    {
        [OperationContract]
        string[] GetRolesForCurrentUser();

        [OperationContract]
        bool IsCurrentUserInRole(string roleName);
    }
}
