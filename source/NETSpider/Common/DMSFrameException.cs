using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DMSFrame.Loggers;

namespace DMSFrame
{
    /// <summary>
    /// 框架的Excption实体
    /// </summary>
    public class DMSFrameException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public DMSFrameException(string message)
            : base(message)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public DMSFrameException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
