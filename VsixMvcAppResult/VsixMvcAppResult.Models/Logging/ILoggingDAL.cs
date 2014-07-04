using Microsoft.Practices.EnterpriseLibrary.Logging;
using VsixMvcAppResult.Models.Logging;
using System;

namespace VsixMvcAppResult.Models.Logging
{
    public interface ILoggingDAL : IDisposable
    {
        DataResultLogMessageList LoggingExceptionGetById(Guid guid);

        Guid LoggingExceptionSet(LogEntry logMessage);

        DataResultLogMessageList LoggingExceptionGetAll(DataFilterLogger filter);
    }
}