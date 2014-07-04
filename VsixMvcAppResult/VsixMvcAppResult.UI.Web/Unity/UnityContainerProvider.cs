using Microsoft.Practices.Unity;
using VsixMvcAppResult.Models.Authentication;
using VsixMvcAppResult.Models.Logging;
using VsixMvcAppResult.Models.Membership;
using VsixMvcAppResult.Models.Profile;
using VsixMvcAppResult.Models.ProxyProviders;
using VsixMvcAppResult.Models.Roles;
using VsixMvcAppResult.Models.Syndication;
using VsixMvcAppResult.Models.Unity;
using VsixMvcAppResult.Models.UserRequestModel;
using VsixMvcAppResult.Models.UserSessionPersistence;
using System;
using System.Web;
using System.Web.SessionState;

namespace VsixMvcAppResult.UI.Web.Unity
{
    public class UnityContainerProvider
    {
        public static IUnityContainer GetContainer(FrontEndUnityContainerAvailable containerSelected)
        {
            IUnityContainer result = new UnityContainer();
            result.RegisterType(typeof(IAuthenticationProxy), typeof(AuthenticationProxy), new InjectionMember[0]);
            result.RegisterType(typeof(IMembershipProxy), typeof(MembershipProxy), new InjectionMember[0]);
            result.RegisterType(typeof(IRoleManagerProxy), typeof(RoleManagerProxy), new InjectionMember[0]);
            result.RegisterType(typeof(IRolesProxy), typeof(RolesProxy), new InjectionMember[0]);
            result.RegisterType(typeof(IProfileProxy), typeof(ProfileProxy), new InjectionMember[0]);
            result.RegisterType(typeof(ILoggingProxy), typeof(LoggingProxy), new InjectionMember[0]);
            result.RegisterType(typeof(ISyndicationProxy), typeof(SyndicationProxy), new InjectionMember[0]);
            result.RegisterType(typeof(IUserRequestContextFrontEnd), typeof(UserRequestContextFrontEnd), new InjectionMember[0]);
            result.RegisterType(typeof(IUserSessionModel), typeof(UserSessionAtHttpCookies), new InjectionMember[0]);

            switch (containerSelected)
            {
                case FrontEndUnityContainerAvailable.ProxiesToCustomHost:
                    result.RegisterType(typeof(IClientChannelInitializer<>), typeof(ClientChannelCustomHostInitializer<>), new InjectionMember[0]);
                    break;
                case FrontEndUnityContainerAvailable.ProxiesToAzure:
                    result.RegisterType(typeof(IClientChannelInitializer<>), typeof(ClientChannelAzureInternalRoleInitializer<>), new InjectionMember[0]);
                    break;
                default:
                    throw new Exception("IUnityContainer does not exist in the list of available providers");
            }

            return result;
        }
    }
}