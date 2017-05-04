using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using DMSFrame.Loggers;

namespace DMSFrame
{
    /// <summary>
    /// 数组类的扩展
    /// </summary>
    public static class CollectionUtils
    { /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="list"></param>
        /// <param name="returnDefaultIfEmpty"></param>
        /// <returns></returns>
        public static T GetSingleItem<T>(this IList<T> list, bool returnDefaultIfEmpty)
        {
            if (list.Count == 1)
            {
                return list[0];
            }
            if (returnDefaultIfEmpty && list.Count == 0)
            {
                return default(T);
            }
            LoggerManager.FileLogger.Log("", "集合中数量超过一条!", ReflectionUtils.GetMethodBaseInfo(System.Reflection.MethodBase.GetCurrentMethod()), ErrorLevel.Fatal);
            throw new DMSFrameException("集合中数量超过一条");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rator"></param>
        public static void Disposed(this IEnumerator rator)
        {
            if (rator != null)
            {
                IDisposable disposable = rator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}
