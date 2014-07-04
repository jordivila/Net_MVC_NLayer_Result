using System.IO;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsixMvcAppResult.Models.Globalization;
using VsixMvcAppResult.Models.UserRequestModel;
using VsixMvcAppResult.Models.Unity;
using VsixMvcAppResult.UI.Web.Unity;
using VsixMvcAppResult.Tests.Common;


namespace VsixMvcAppResult.Tests.Integration.Common
{
    [TestClass]
    public abstract class TestControllerBase<Tarea> : TestIntegrationBase where Tarea : AreaRegistration, new()
    {
        public virtual void MyTestInitialize()
        {
            TestControllerBase<Tarea>.Application_InitEnterpriseLibrary();
            TestControllerBase<Tarea>.SetHttpContext();
        }

        private Tarea Area
        {
            get
            {
                return new Tarea();
            }
        }
    }
}
