﻿using System;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Unity;
using VsixMvcAppResult.DAL.LoggingServices;
using VsixMvcAppResult.Models.Logging;
using VsixMvcAppResult.Models.Unity;

namespace VsixMvcAppResult.BL.LoggingServices
{
    public class LoggingBL : BaseBL, ILoggingProxy
    {
        private ILoggingDAL _dal;

        public LoggingBL()
        {
            _dal = DependencyFactory.Resolve<ILoggingDAL>();
        }

        public override void Dispose()
        {
            base.Dispose();

            if (this._dal != null)
            {
                this._dal.Dispose();
            }
        }

        public DataResultLogMessageList LoggingExceptionGetById(Guid guid)
        {
            return this._dal.LoggingExceptionGetById(guid);
        }

        public Guid LoggingExceptionSet(LogEntry logMessage)
        {
            return this._dal.LoggingExceptionSet(logMessage);
        }

        public DataResultLogMessageList LoggingExceptionGetAll(DataFilterLogger filter)
        {
            return this._dal.LoggingExceptionGetAll(filter);
        }

    }
}