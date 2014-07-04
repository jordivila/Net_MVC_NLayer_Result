using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsixMvcAppResult.Models.Globalization;
using VsixMvcAppResult.Models.Unity;
using VsixMvcAppResult.Models.UserRequestModel;
using VsixMvcAppResult.UI.Web.Unity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Hosting;

namespace VsixMvcAppResult.Tests.Common
{
    public class TestCommon
    {
        public static string CultureDefault = GlobalizationHelper.CultureInfoGetOrDefault(Thread.CurrentThread.CurrentCulture.Name).Name;
    }

    [TestClass]
    public abstract class TestBase
    {
        public static string currentCultureName = TestCommon.CultureDefault;

        internal TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
    }
}
