using Microsoft.Practices.Unity;
using VsixMvcAppResult.DAL.LoggingServices;
using VsixMvcAppResult.DAL.MembershipRoleServices;
using VsixMvcAppResult.DAL.MembershipServices;
using VsixMvcAppResult.DAL.SyndicationServices;
using VsixMvcAppResult.DAL.TokenTemporaryPersistenceServices;
using VsixMvcAppResult.Models.Logging;
using VsixMvcAppResult.Models.Membership;
using VsixMvcAppResult.Models.Profile;
using VsixMvcAppResult.Models.Roles;
using VsixMvcAppResult.Models.SmtpModels;
using VsixMvcAppResult.Models.Syndication;
using VsixMvcAppResult.Models.TokenPersistence;
using VsixMvcAppResult.Models.Unity;
using VsixMvcAppResult.Models.UserRequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace VsixMvcAppResult.WCF.ServicesHostCommon.Unity
{
    public class UnityContainerProvider
    {
        public static IUnityContainer GetContainer(BackEndUnityContainerAvailable containerSelected)
        {
            IUnityContainer result = new UnityContainer();

            switch (containerSelected)
            {
                case BackEndUnityContainerAvailable.Real:
                    result.RegisterType(typeof(ISmtpClient), typeof(SmtpClientMock), new InjectionMember[0]);
                    result.RegisterType(typeof(IMembershipDAL), typeof(MembershipDAL), new InjectionMember[0]);
                    result.RegisterType(typeof(IRoleAdminDAL), typeof(RoleAdminDAL), new InjectionMember[0]);
                    result.RegisterType(typeof(IProfileDAL), typeof(ProfileDAL), new InjectionMember[0]);
                    result.RegisterType(typeof(ILoggingDAL), typeof(LoggingDAL), new InjectionMember[0]);
                    result.RegisterType(typeof(ISyndicationDAL), typeof(SyndicationDAL), new InjectionMember[0]);
                    result.RegisterType(typeof(ITokenTemporaryPersistenceDAL<>), typeof(TokenTemporaryDatabasePersistenceDAL<>), new InjectionMember[0]);

                    //result.RegisterType(typeof(IUserRequestModel), typeof(UserRequestContextBackEndNetTcp), new InjectionMember[0]);
                    //result.RegisterType(typeof(IUserRequestModel), typeof(UserRequestContextBackEndHttp), new InjectionMember[0]);
                    result.RegisterType(typeof(IUserRequestModel), typeof(UserRequestContextBackEndNetTcp), new InjectionMember[0]);

                    break;
                default:
                    throw new Exception("IUnityContainer does not exist in the list of available providers");
            }

            return result;
        }
    }
}
