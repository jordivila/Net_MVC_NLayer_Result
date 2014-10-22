using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using VsixMvcAppResult.Models.Unity;
using VsixMvcAppResult.Models.UserRequestModel;
using VsixMvcAppResult.Models.UserSessionPersistence;
using VsixMvcAppResult.UI.Web.Common.AspNetApplicationServices;
using VsixMvcAppResult.UI.Web.Common.Mvc.Attributes;
using VsixMvcAppResult.UI.Web.Controllers;

namespace VsixMvcAppResult.UI.Web
{
    [Serializable]
    public partial class MvcApplication : HttpApplication
    {
        private static string _version = string.Empty;
        public static string Version
        {
            get
            {
                if (_version == string.Empty)
                {
                    _version = @System.Reflection.Assembly.GetAssembly(typeof(VsixMvcAppResult.UI.Web.MvcApplication)).GetName().Version.ToString();
                }
                return _version;
            }
            set { }
        }

        private static IUserRequestContextFrontEnd _userRequest = null;

        public static IUserRequestContextFrontEnd UserRequest
        {
            get
            {
                if (_userRequest == null)
                {
                    _userRequest = DependencyFactory.Resolve<IUserRequestContextFrontEnd>();
                }

                return _userRequest;
            }
        }

        private static IUserSessionModel _userSession = null;

        public static IUserSessionModel UserSession
        {
            get
            {
                if (_userSession == null)
                {
                    _userSession = DependencyFactory.Resolve<IUserSessionModel>();
                }

                return _userSession;
            }
        }

        public static string Name
        {
            get
            {
                return "VsixMvcAppResult.UI.Web";
            }
        }

        public void Application_Start()
        {
            HostingEnvironment.RegisterVirtualPathProvider(new EmbeddedVirtualPathProvider());

            //MvcApplication.RegisterViewEngines();
            MvcApplication.RegisterGlobalFilters(GlobalFilters.Filters);
            AreaRegistration.RegisterAllAreas();
            MvcApplication.RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new CustomControllerFactory());

            Application_InitEnterpriseLibrary();
        }

        public virtual void Application_InitEnterpriseLibrary()
        {
            LogWriterFactory logWriterFactory = new LogWriterFactory();
            Logger.SetLogWriter(logWriterFactory.Create());
        }

        public void Application_End()
        {
            MvcApplication.UserRequest.Dispose();
        }

        public static RouteCollection GetRoutes()
        {
            return RouteTable.Routes;
        }

        protected void Application_BeginRequest()
        {
            // Warning !!!! Si quitamos esto no funciona la Valuidacion con DataAnnotations !!!
            Thread.CurrentThread.CurrentCulture = MvcApplication.UserRequest.UserProfile.Culture;
            Thread.CurrentThread.CurrentUICulture = MvcApplication.UserRequest.UserProfile.Culture;
        }

        protected static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new RequestLoggerActionAttribute());
            //filters.Add(new RequestContextItemsSetterAttibute());
            filters.Add(new HandleErrorAttributeExtended());
            filters.Add(new CompressFilterActionAttribute());
            filters.Add(new MembershipUpdateLastActivityActionAttribute());
            //filters.Add(new CacheFilterAttribute());  // Warning !!! all pages will be cached in case this line is uncomented
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected static void RegisterViewEngines()
        {
            /* Performance tip */
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            //ViewEngines.Engines.Add(new RazorCustomizedViewEngine());
            //ViewEngines.Engines.Add(new MobileViewEngine());
            //ViewEngines.Engines.Add(new MvcApplication3.UI.Web.ViewEngines_Custom.Samples.jQueryRazorViewEngine());
        }

        private void RegisterValueProviderFactories()
        {
            // this allow automatic Json Model Binding--> read more about JsonValueProviderFactory on msdn 
            //ValueProviderFactories.Factories.Add(new JsonValueProviderFactory()); // --> JsonValueProviderFactory became a built-in functionallity on MVC 3
        }
    }

    public class EmbeddedVirtualPathProvider : VirtualPathProvider
    {
        // Nested class representing the "virtual file"
        public class EmbeddedVirtualFile : VirtualFile
        {
            private Stream _stream;

            public EmbeddedVirtualFile(string virtualPath,
                Stream stream)
                : base(virtualPath)
            {
                if (null == stream)
                    throw new ArgumentNullException("stream");

                _stream = stream;
            }

            public override Stream Open()
            {
                return _stream;
            }
        }

        public EmbeddedVirtualPathProvider()
        {
        }

        public override CacheDependency GetCacheDependency(
            string virtualPath,
            IEnumerable virtualPathDependencies,
            DateTime utcStart)
        {
            string embedded = _GetEmbeddedPath(virtualPath);

            // not embedded? fall back
            if (string.IsNullOrEmpty(embedded))
            {
                var h = base.GetCacheDependency(virtualPath,virtualPathDependencies, utcStart);


                return h;
            }

            // there is no cache dependency for embedded resources
            return null;
        }

        public override bool FileExists(string virtualPath)
        {
            string embedded = _GetEmbeddedPath(virtualPath);

            // You can override the embed by placing a real file
            // at the virtual path...
            var h = base.FileExists(virtualPath)
                || !string.IsNullOrEmpty(embedded);
            return h;
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            // You can override the embed by placing a real file
            // at the virtual path...
            if (base.FileExists(virtualPath))
                return base.GetFile(virtualPath);

            string embedded = _GetEmbeddedPath(virtualPath);

            // sanity...
            if (string.IsNullOrEmpty(embedded))
                return null;

            return new EmbeddedVirtualFile(virtualPath,
                GetType().Assembly
                .GetManifestResourceStream(embedded));
        }

        private string _GetEmbeddedPath(string path)
        {
            if (path.StartsWith("~/"))
            {
                path = path.Substring(1);
            }

            path = "VsixMvcAppResult.UI.Web.Common" + path.Replace('/', '.');
            var h = GetType().Assembly.GetManifestResourceNames().Where(o => o.ToLower() == path.ToLower()).FirstOrDefault();
            return h;
        }
    }
}

