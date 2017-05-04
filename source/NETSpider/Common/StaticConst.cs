using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace NETSpider
{
    public class StaticConst
    {
        public const string Version = "1.0";
        public const int AutoSaveNum = 50;
        public static DateTime DateMin = DateTime.Parse("1900-01-01");
        public static TOutput CloneObject<TSource, TOutput>(TSource o)
            where TOutput : new()
        {
            PropertyInfo[] properties = typeof(TSource).GetProperties();
            TOutput result = new TOutput();
            foreach (PropertyInfo pi in properties)
            {
                PropertyInfo resultPI = typeof(TOutput).GetProperty(pi.Name);
                if (resultPI != null && resultPI.CanWrite && resultPI.PropertyType == pi.PropertyType)
                {
                    object value = pi.GetValue(o, null);
                    resultPI.SetValue(result, value, null);
                }
            }
            return result;
        }
    }
}
