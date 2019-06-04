using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace GTFSValidatorAndImportTool.Utilities
{
    /// <summary>
    /// Log operation
    /// </summary>
    public class GTFSValidatorAndImportLog
    {
        protected static readonly ILog log = LogManager.GetLogger("GTFSValidatorAndImportLog");
        static GTFSValidatorAndImportLog()
        {
            LoadConfig();
        }

        public static void DetailDebug(object message, Exception ex = null)
        {
            if (AppConfigReader.DetailedLogEnabled())
            {
                if (ex != null)
                    log.Debug(message, ex);
                else log.Debug(message);
            }
        }

        /// <summary>
        /// used to log the Debug message
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(object message, Exception ex = null)
        {
            if (AppConfigReader.DetailedLogEnabled() && ex != null)
            {
                log.Debug(message, ex);
            }
            else log.Debug(message);
        }

        /// <summary>
        /// used to log the Error message
        /// </summary>
        /// <param name="message"></param>
        public static void Error(object message)
        {
            log.Error(message);
        }

        /// <summary>
        /// used to log the Warning message
        /// </summary>
        /// <param name="message"></param>
        public static void Warn(object message)
        {
            log.Warn(message);
        }

        /// <summary>
        /// used to log the Info message
        /// </summary>
        /// <param name="message"></param>
        public static void Info(object message)
        {
            log.Info(message);
        }

        private static void LoadConfig()
        {
            log4net.GlobalContext.Properties["LogName"] = DirectoryHelper.Instance.GetCommonLocation();
            log4net.Config.XmlConfigurator.Configure();
            DirectoryHelper.Instance.CreateLogDirectory();
        }
     

    }
}
