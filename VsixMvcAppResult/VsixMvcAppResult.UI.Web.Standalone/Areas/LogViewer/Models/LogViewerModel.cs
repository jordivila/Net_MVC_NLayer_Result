using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using VsixMvcAppResult.Models.Logging;
using VsixMvcAppResult.UI.Web.Models;


namespace VsixMvcAppResult.UI.Web.Areas.LogViewer.Models
{
    public class LogViewerModel : baseViewModel
    {
        public NamedElementCollection<TraceSourceData> LogTraceSources { get; set; }
        public NamedElementCollection<TraceListenerReferenceData> LogTraceListeners { get; set; }
        public DataResultLogMessageList LogMessages { get; set; }
        public DataFilterLogger Filter { get; set; }
    }
}