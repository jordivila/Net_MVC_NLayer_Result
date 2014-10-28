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
using System.Linq;


namespace VsixMvcAppResult.UI.Web.Areas.LogViewer.Models
{
    public class LogViewerModel : baseViewModel
    {
        public LogViewerModel()
        {
            this.Severities = new List<SelectListItem>() { new SelectListItem() { Text = LogViewerTexts.Severity_All, Value = string.Empty, Selected = true }, }
                                .Union(((Enum)LoggerSeverities.Error).ToSelectList(typeof(LoggerSeverities)))
                                .Select(c => { c.Selected = false; return c; })
                                .ToList();

            this.Severities.Where(x => x.Value == string.Empty).First().Selected = true;



            this.Categories = new List<SelectListItem>() { new SelectListItem() { Text = LogViewerTexts.Severity_All, Value = string.Empty, Selected = true } }
                                .Union(((Enum)LoggerCategories.UIGeneral).ToSelectList(typeof(LoggerCategories)))
                                .Select(c => { c.Selected = false; return c; })
                                .ToList();

            this.Categories.Where(x => x.Value == string.Empty).First().Selected = true;
            
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