using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.Unity;
using VsixMvcAppResult.Models.Enumerations;
using VsixMvcAppResult.Models.Logging;
using VsixMvcAppResult.Models.Unity;
using VsixMvcAppResult.Resources.General;
using VsixMvcAppResult.UI.Web.Areas.LogViewer.Models;
using VsixMvcAppResult.UI.Web.Areas.UserAccount;
using VsixMvcAppResult.UI.Web.Controllers;
using VsixMvcAppResult.UI.Web.Common.Mvc.Html;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace VsixMvcAppResult.UI.Web.Areas.LogViewer.Controllers
{
    [VsixMvcAppResult.UI.Web.Common.Mvc.Attributes.Authorize(Roles = SiteRoles.Administrator)]
    public class LogViewerController : Controller, IControllerWithClientResources
    {
        public string[] GetControllerJavascriptResources
        {
            get { return new string[0]; }
        }
        public string[] GetControllerStyleSheetResources
        {
            get { return new string[0]; }
        }

        private const string LogginConfigurationSectionName = "loggingConfiguration";

        public ActionResult LogViewerByModel(LogViewerModel model)
        {
            if (WebGrid<LogMessageModel, LogViewerModel, DataFilterLogger>.IsWebGridEvent())
            {
                this.ModelState.Clear();
                model.Filter = (DataFilterLogger)WebGrid<LogMessageModel, LogViewerModel, DataFilterLogger>.GetDataFilterFromPost();
                model.Filter.IsClientVisible = false;
            }

            model.BaseViewModelInfo.Title = GeneralTexts.LogViewer;

            LogWriterFactory logWriterFactory = new LogWriterFactory();

            using (LogWriter logWriterInstance = logWriterFactory.Create())
            {
                using (TraceListener traceListenerInstance =
                                logWriterInstance.TraceSources
                                .Select(x => x.Value)
                                .SelectMany(x => x.Listeners)
                                .Where(x=>x.Name == model.Filter.LogTraceListenerSelected)
                                .First())
                {

                    if (traceListenerInstance is ICustomTraceListener)
                    {
                        model.LogMessages = ((ICustomTraceListener)traceListenerInstance).SearchLogMessages(LogginConfigurationSectionName, model.Filter);
                        return View(LogViewerViewHelper.LogViewerDisplay, model);
                    }
                    else
                    {
                        throw new Exception("TraceListener Not Supported");
                    }
                }
            }
        }

        public ActionResult LogViewerById(string guid)
        {
            LogViewerModel model = new LogViewerModel();

            using (ILoggingProxy provider = DependencyFactory.Resolve<ILoggingProxy>())
            {
                model.BaseViewModelInfo.Title = GeneralTexts.LogViewer;
                model.LogMessages = new DataResultLogMessageList()
                {
                    Data = provider.LoggingExceptionGetById(Guid.Parse(guid)).Data
                };
            }


            return View(LogViewerViewHelper.LogViewerById, model);
        }

        public ActionResult LogViewerByDatabaseTraceListener()
        {
            LogViewerModel model = new LogViewerModel();
            model.BaseViewModelInfo.Title = GeneralTexts.LogViewer;
            model.LogTraceSources = (ConfigurationManager.GetSection(LogginConfigurationSectionName) as LoggingSettings).TraceSources;

            //string sourceName = "WCFGeneral"; // string.Empty;
            //string sourceName = string.Empty;
            string listenerName = "Database Trace Listener";// string.Empty;

            //if (!string.IsNullOrEmpty(sourceName))
            //{
                //if (!model.LogTraceSources.Contains(sourceName))
                //{
                //    throw new Exception("Trace Data Source Not Found");
                //}

                model.Filter = new DataFilterLogger()
                {
                    //LogTraceSourceSelected = sourceName,
                    CreationDate = DateTime.Now,
                    //CreationDateTo = DateTime.Now,
                    IsClientVisible = true,
                    Page = 0,
                    PageSize = (int)PageSizesAvailable.RowsPerPage10,
                    SortAscending = true,
                    SortBy = string.Empty
                };

                //model.LogTraceListeners = model.LogTraceSources.Where(x => x.Name == sourceName).First().TraceListeners;

                //if (!string.IsNullOrEmpty(listenerName))
                //{
                    model.Filter.LogTraceListenerSelected = listenerName;
                    //if (!model.LogTraceListeners.Contains(listenerName))
                    //{
                    //    throw new Exception("Trace Data Source Not Found");
                    //}
                //}
            //}
            
            return View(LogViewerViewHelper.LogViewerDisplay, model);
        }
    }
}
