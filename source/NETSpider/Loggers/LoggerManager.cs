using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DMSFrame.Helpers;

namespace DMSFrame.Loggers
{
    /// <summary>
    /// 日志处理管理器
    /// </summary>
    public static class LoggerManager
    {
        /// <summary>
        /// 
        /// </summary>
        public const string InputValidtion = "传值输入错误";

        private static IAgileLogger logger;
        private static IAgileLogger datalogger;
        /// <summary>
        /// 
        /// </summary>
        static LoggerManager()
        {
            if (logger == null)
            {
                string logFilePath = Path.Combine(ConfigurationHelper.AppSettingPath("LogFile"), "Logs");
                if (!Directory.Exists(logFilePath))
                {
                    Directory.CreateDirectory(logFilePath);
                }
                logger = new FileAgileLogger(Path.Combine(logFilePath, DateTime.Now.ToString("yyyy-MM-dd") + ".log"));
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public static IAgileLogger FileLogger
        {
            get { return logger; }
        }
        /// <summary>
        /// 
        /// </summary>
        public static IAgileLogger DataLogger
        {
            get { return datalogger; }
            set { datalogger = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Obsolete("use LoggerManager.FileLogger")]
        public static IAgileLogger Logger
        {
            get { return logger; }
        }
    }
}
