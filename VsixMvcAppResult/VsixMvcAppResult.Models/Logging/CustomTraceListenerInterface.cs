using VsixMvcAppResult.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsixMvcAppResult.Models.Logging
{
    public interface ICustomTraceListener
    {
        DataResultLogMessageList SearchLogMessages(string LogginConfigurationSectionName, DataFilterLogger dataFilter);
    }
}