using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VsixMvcAppResult.Models.Unity;
using VsixMvcAppResult.UI.Web.Unity;

namespace VsixMvcAppResult.UI.Web.StandAloneSite
{
    public class Global : MvcApplication
    {
        public override void Application_InitEnterpriseLibrary()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
            DependencyFactory.SetUnityContainerProviderFactory(UnityContainerProvider.GetContainer(FrontEndUnityContainerAvailable.ProxiesToAzure));

            base.Application_InitEnterpriseLibrary();
        }
    }
}