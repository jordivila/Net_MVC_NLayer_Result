using System;
using System.ServiceModel;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace VsixMvcAppResult.Models.Logging
{

    [ServiceContract]
    public interface ILoggingProxy : IDisposable
    {
        [OperationContract]
        DataResultLogMessageList LoggingExceptionGetById(Guid guid);

        [OperationContract]
        Guid LoggingExceptionSet(LogEntry logMessage);

        [OperationContract]
        DataResultLogMessageList LoggingExceptionGetAll(DataFilterLogger filter);
    }
}
