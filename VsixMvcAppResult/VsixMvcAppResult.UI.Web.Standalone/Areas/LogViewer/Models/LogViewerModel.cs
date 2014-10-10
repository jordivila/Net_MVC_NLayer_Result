using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using VsixMvcAppResult.Models.Enumerations;
using VsixMvcAppResult.Models.Logging;
using VsixMvcAppResult.Resources.Helpers.GeneratedResxClasses;
using VsixMvcAppResult.Resources.LogViewer;
using VsixMvcAppResult.UI.Web.Models;
using VsixMvcAppResult.UI.Web.Common.Mvc.Html;


namespace VsixMvcAppResult.UI.Web.Areas.LogViewer.Models
{
    public class LogViewerModel : baseViewModel
    {
        public LogViewerModel()
        {
            this.Severities = new List<SelectListItem>() 
            { 
                new SelectListItem(){ Text= LogViewerTexts.Severity_All, Value=string.Empty, Selected=true },
            };

            this.Severities.AddRange(((Enum)LoggerSeverities.Error).ToSelectList(typeof(LoggerSeverities)));



             this.Categories = new List<SelectListItem>() 
            { 
                new SelectListItem(){ Text= LogViewerTexts.Severity_All, Value=string.Empty, Selected=true },
            };

            this.Categories.AddRange(((Enum)LoggerCategories.UIGeneral).ToSelectList(typeof(LoggerCategories)));


            
        }

        public NamedElementCollection<TraceSourceData> LogTraceSources { get; set; }
        public NamedElementCollection<TraceListenerReferenceData> LogTraceListeners { get; set; }
        public DataResultLogMessageList LogMessages { get; set; }

        [Display(ResourceType = typeof(LogViewerTexts), Name = LogViewerTextsKeys.Severity)]
        public List<SelectListItem> Severities { get; set; }

        [Display(ResourceType = typeof(LogViewerTexts), Name = LogViewerTextsKeys.Categories)]
        public List<SelectListItem> Categories { get; set; }

        public DataFilterLogger Filter { get; set; }
    }
}