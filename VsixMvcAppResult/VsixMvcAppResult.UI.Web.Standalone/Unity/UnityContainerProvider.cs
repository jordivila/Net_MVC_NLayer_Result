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
using VsixMvcAppResult.Models.SmtpModels;
using VsixMvcAppResult.DAL.MembershipServices;
using VsixMvcAppResult.DAL.MembershipRoleServices;
using VsixMvcAppResult.DAL.LoggingServices;
using VsixMvcAppResult.DAL.SyndicationServices;
using VsixMvcAppResult.DAL.TokenTemporaryPersistenceServices;
using VsixMvcAppResult.Models.TokenPersistence;
using VsixMvcAppResult.BL.SyndicationServices;
using VsixMvcAppResult.BL.LoggingServices;
using VsixMvcAppResult.BL.MembershipServices;
using VsixMvcAppResult.BL.AuthenticationServices;

namespace VsixMvcAppResult.UI.Web.Unity
{
    public class UnityContainerProvider
    {
        public static IUnityContainer GetContainer(FrontEndUnityContainerAvailable containerSelected)
        {
            IUnityContainer result = new UnityContainer();
            /*Front End Interfaces begin*/
            result.RegisterType(typeof(IAuthenticationProxy), typeof(AuthenticationBL), new InjectionMember[0]);
            result.RegisterType(typeof(IMembershipProxy), typeof(MembershipBL), new InjectionMember[0]);
            result.RegisterType(typeof(IRoleManagerProxy), typeof(RoleAdminBL), new InjectionMember[0]);
            result.RegisterType(typeof(IRolesProxy), typeof(RoleAdminBL), new InjectionMember[0]);
            result.RegisterType(typeof(IProfileProxy), typeof(ProfileBL), new InjectionMember[0]);
            result.RegisterType(typeof(ILoggingProxy), typeof(LoggingBL), new InjectionMember[0]);
            result.RegisterType(typeof(ISyndicationProxy), typeof(SyndicationBL), new InjectionMember[0]);
            result.RegisterType(typeof(IUserRequestContextFrontEnd), typeof(UserRequestContextFrontEnd), new InjectionMember[0]);
            result.RegisterType(typeof(IUserSessionModel), typeof(UserSessionAtHttpCookies), new InjectionMember[0]);
            /*Front End Interfaces end*/

            /*Back End Interfaces begin*/
            result.RegisterType(typeof(ISmtpClient), typeof(SmtpClientMock), new InjectionMember[0]);
            result.RegisterType(typeof(IMembershipDAL), typeof(MembershipDAL), new InjectionMember[0]);
            result.RegisterType(typeof(IRoleAdminDAL), typeof(RoleAdminDAL), new InjectionMember[0]);
            result.RegisterType(typeof(IProfileDAL), typeof(ProfileDAL), new InjectionMember[0]);
            result.RegisterType(typeof(ILoggingDAL), typeof(LoggingDAL), new InjectionMember[0]);
            result.RegisterType(typeof(ISyndicationDAL), typeof(SyndicationDAL), new InjectionMember[0]);
            result.RegisterType(typeof(ITokenTemporaryPersistenceDAL<>), typeof(TokenTemporaryDatabasePersistenceDAL<>), new InjectionMember[0]);
            //result.RegisterType(typeof(IUserRequestModel), typeof(UserRequestContextBackEndNetTcp), new InjectionMember[0]);
            //result.RegisterType(typeof(IUserRequestModel), typeof(UserRequestContextBackEndHttp), new InjectionMember[0]);
            result.RegisterType(typeof(IUserRequestModel), typeof(UserRequestContextBackEndStandaloneVersion), new InjectionMember[0]);
            /*Back End Interfaces end*/

            return result;
        }
    }
}