using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Caching;
using System.Text;
using System.Threading;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using VsixMvcAppResult.Models.Configuration;
using VsixMvcAppResult.Models.Configuration.ConfigSections.ClientResources;
using VsixMvcAppResult.Models.Cryptography;
using VsixMvcAppResult.Models.Enumerations;
using VsixMvcAppResult.Models.Logging;
using VsixMvcAppResult.UI.Web.Common.Mvc.Attributes;
using System.Linq;
using VsixMvcAppResult.Resources.Helpers.GeneratedResxClasses;

namespace VsixMvcAppResult.UI.Web.Controllers
{
    [CacheFilterAttribute(Duration = 9000000)]
    public class ResourceDispatcherController : Controller
    {
        public const string ResourceDispatchParamCommonKey = "Common";
        public const string ResourceDispatchCryptoPasswordKey = "ResourceDispatchCryptoPasswordKey";
        public const string ResourceDispatchParamControllerKey = "controller";
        public const string ResourceDispatchParamVersionKey = "version";
        public const string ResourceDispatchParamCultureKey = "culture";
        public const string ResourceDispatchParamIsMobileDevice = "IsMobileDevice";
        private ObjectCache _objCacheManager = new MemoryCache("CacheManagerFoeClientResources");
        private CacheItemPolicy _objCachePolicy = new CacheItemPolicy();

        private string GenerateCacheKey(string id, MediaType mediaType)
        {
            string controller = id;
            string cacheKey = string.Empty;
            if (mediaType == MediaType.javascript)
            {
                cacheKey = string.Format("{0}.{1}.{2}", controller,
                                                        MvcApplication.UserRequest.UserProfile.Culture.Name,
                                                        mediaType == MediaType.javascript ? "js" : "css");
            }
            else
            {
                cacheKey = string.Format("{0}.{1}.{2}", controller,
                                                        string.Format("{0}{1}", Request.Browser.Browser, Request.Browser.MajorVersion),
                                                        mediaType == MediaType.javascript ? "js" : "css");
            }
            return cacheKey;
        }
        private void FileSetCache(StringBuilder lFilesContent, string cacheKey, MediaType media)
        {
            Minifier m = new Minifier();
            if (media == MediaType.javascript)
            {
                CodeSettings c = new CodeSettings();
                this._objCacheManager.Add(cacheKey,
                                        ApplicationConfiguration.IsDebugMode ? lFilesContent.ToString() : m.MinifyJavaScript(lFilesContent.ToString(), c)
                                        , _objCachePolicy);
            }
            else
            {
                this._objCacheManager.Add(cacheKey,
                                        ApplicationConfiguration.IsDebugMode ? lFilesContent.ToString() : m.MinifyStyleSheet(lFilesContent.ToString())
                                        , _objCachePolicy);
            }
        }
        private bool FileIsCached(string cacheKey)
        {
            if (ApplicationConfiguration.IsDebugMode)
            {
                this._objCacheManager.Remove(cacheKey);
            }

            return this._objCacheManager.Contains(cacheKey);
        }
        
        
        public JavaScriptResult Javascript()
        {
            string controller = Crypto.Decrypt(Request.Params[ResourceDispatchParamControllerKey], ResourceDispatcherController.ResourceDispatchCryptoPasswordKey);
            string cacheKeyJs = this.GenerateCacheKey(controller, MediaType.javascript);

            if (!this.FileIsCached(cacheKeyJs))
            {
                StringBuilder sb = new StringBuilder();

                if (controller == ResourceDispatcherController.ResourceDispatchParamCommonKey)
                {
                    this.Javascript_CommonGet(ref sb);
                }
                else
                {
                    List<string> files = new List<string>();
                    files.AddRange(this.Javascript_ControllerGet(Type.GetType(controller)));
                    files.AddRange(ApplicationConfiguration.ClientResourcesSettingsSection.WebSitePageInitScripts);
                    this.AppendFiles(ref sb, files.ToArray());
                }

                this.FileSetCache(sb, cacheKeyJs, MediaType.javascript);
            }

            JavaScriptResult jsResult = new JavaScriptResult();
            jsResult.Script = (string)this._objCacheManager.Get(cacheKeyJs);
            return jsResult;
        }
        private string[] Javascript_ControllerGet(Type controllerType)
        {
            string[] files = null;
            object instanceObj = Activator.CreateInstance(controllerType);
            IControllerWithClientResources userIntendedControllerInstance = instanceObj as IControllerWithClientResources;
            IDisposable disposableInstance = instanceObj as IDisposable;

            if (userIntendedControllerInstance != null)
            {
                files = userIntendedControllerInstance.GetControllerJavascriptResources;
            }

            if (disposableInstance != null)
            {
                disposableInstance.Dispose();
            }

            if (files != null)
            {
                return files;
            }
            else
            {
                return new string[0];
            }
        }
        private void Javascript_CommonGet(ref StringBuilder sb)
        {

            LocalizationResourcesHelper cc = new LocalizationResourcesHelper(Thread.CurrentThread.CurrentCulture);

            List<string> scripts = ApplicationConfiguration.ClientResourcesSettingsSection.JQueryLibScripts;
            scripts.Add(ApplicationConfiguration.ClientResourcesSettingsSection.jQueryUILocalizationPath(cc));
            scripts.Add(ApplicationConfiguration.ClientResourcesSettingsSection.jQueryGlobalizeLozalizationPath(cc));
            scripts.Add(ApplicationConfiguration.ClientResourcesSettingsSection.jQueryValidationLocalizationPath(cc));
            scripts.AddRange(ApplicationConfiguration.ClientResourcesSettingsSection.WebSiteCommonScripts.ToArray());
            scripts = scripts.Where(x => (!string.IsNullOrEmpty(x))).ToList();


            this.AppendFiles(ref sb, scripts.ToArray());

            this.JavascriptResources_Generate( ref sb);
        }
        private void JavascriptResources_Generate(ref StringBuilder sb)
        {

            JavascriptTextsViewModelHelper jsTexts = new JavascriptTextsViewModelHelper();
            System.Reflection.PropertyInfo[] thisObjectProperties = jsTexts.GetType().GetProperties();
            sb.Append(Environment.NewLine);
            sb.Append("VsixMvcAppResult.Resources = {");
            sb.Append(Environment.NewLine);
            for (int i = 0; i < thisObjectProperties.Length; i++)
            {
                System.Reflection.PropertyInfo info = thisObjectProperties[i];
                sb.Append("\t");
                sb.Append(info.Name);
                sb.Append(": ");
                sb.Append(string.Format("\"{0}\"", info.GetValue(jsTexts, null).ToString()));
                if (i < (thisObjectProperties.Length - 1))
                {
                    sb.Append(",");
                }
                sb.Append(Environment.NewLine);
            }
            sb.Append("};");
        }


        public FileStreamResult StyleSheet()
        {
            string controller = Crypto.Decrypt(Request.Params[ResourceDispatchParamControllerKey], ResourceDispatcherController.ResourceDispatchCryptoPasswordKey);
            string cacheKeyCss = this.GenerateCacheKey(controller, MediaType.stylesheet);

            if (controller == ResourceDispatcherController.ResourceDispatchParamCommonKey)
            {
                cacheKeyCss = "Common.css";
            }

            if (!this.FileIsCached(cacheKeyCss))
            {
                StringBuilder sb = new StringBuilder();
                if (controller == ResourceDispatcherController.ResourceDispatchParamCommonKey)
                {
                    this.Stylesheet_CommonGet(ref sb);
                }
                else
                {
                    this.AppendFiles(ref sb, this.Stylesheet_ControllerGet(Type.GetType(controller)));
                }

                this.FileSetCache(sb, cacheKeyCss, MediaType.stylesheet);
            }

            FileStreamResult fsr = new FileStreamResult(new MemoryStream(UTF8Encoding.UTF8.GetBytes((string)this._objCacheManager.Get(cacheKeyCss))), "text/css");
            return fsr;
        }
        private string[] Stylesheet_ControllerGet(Type controllerType)
        {
            string[] files = null;
            object instanceObj = Activator.CreateInstance(controllerType);
            IControllerWithClientResources userIntendedControllerInstance = instanceObj as IControllerWithClientResources;
            IDisposable disposableInstance = instanceObj as IDisposable;

            if (userIntendedControllerInstance != null)
            {
                files = userIntendedControllerInstance.GetControllerStyleSheetResources;
            }

            if (disposableInstance != null)
            {
                disposableInstance.Dispose();
            }

            if (files != null)
            {
                return files;
            }
            else
            {
                return new string[0];
            }
        }
        private void Stylesheet_CommonGet(ref StringBuilder sb)
        {
            List<string> scripts = ApplicationConfiguration.ClientResourcesSettingsSection.WebSiteCommonStyleSheets;
            this.AppendFiles(ref sb, scripts.ToArray());
        }

        private void AppendFiles(ref StringBuilder sb, string[] filesVirtualPath)
        {
            foreach (string item in filesVirtualPath)
            {
                string filePath = Server.MapPath(item);

                if (System.IO.File.Exists(filePath))
                {
                    using (StreamReader reader = System.IO.File.OpenText(filePath))
                    {
                        sb.Append(reader.ReadToEnd());
                    }
                }
                else
                {
                    LoggingHelper.Write(new ArgumentException(string.Format("File not found: {0}", filePath)));
                }
            }
        }

    }
}