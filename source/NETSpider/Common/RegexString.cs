using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NETSpider
{
    public class RegexString
    {
        /// <summary>
        /// 取大括号内的数字分页数据
        /// </summary>
        public const string Regex01 = @"(?<={)[\d,\d,\d]+(?=})";
        /// <summary>
        /// 取大括号内的a-z数据
        /// </summary>
        public const string Regex02 = @"(?<={)[(a-z)\-(a-z)]+(?=})";

        /// <summary>
        /// 取大括号内的A-Z数据
        /// </summary>
        public const string Regex03 = @"(?<={)[(A-Z)\-(A-Z)]+(?=})";

        /// <summary>
        /// 取大括号内的字典数据
        /// </summary>
        public const string Regex04 = @"(?<={)[dict:(^}\S*)]+(?=})";


        /// <summary>
        /// 获取匹配的网址,下一页
        /// </summary>
        public const string Regex11 = "((?<=href=[\'|\"])\\S[^#+$<>\\s]*(?=[\'|\"]))[^<]*(?<={0})";


        /// <summary>
        /// 网址前缀
        /// </summary>
        public const string Regex30 = @"(?<=[href=|src=|open(][\W])";

        //正则表达式转义
        public static string RegexReplaceTrans(string str)
        {
            if (str == "" || str == null)
                return "";

            string conStr = "";
            if (Regex.IsMatch(str, @"[\$\*\[\]\?\\\(\)]"))
            {
                Regex re = new Regex(@"\\", RegexOptions.IgnoreCase);
                str = re.Replace(str, @"\\");
                re = null;

                re = new Regex(@"\$", RegexOptions.IgnoreCase);
                str = re.Replace(str, @"\$");
                re = null;

                //re = new Regex(@"\.", RegexOptions.IgnoreCase);
                //str = re.Replace(str, @"\.");
                //re = null;

                re = new Regex(@"\*", RegexOptions.IgnoreCase);
                str = re.Replace(str, @"\*");
                re = null;

                re = new Regex(@"\[", RegexOptions.IgnoreCase);
                str = re.Replace(str, @"\[");
                re = null;

                re = new Regex(@"\]", RegexOptions.IgnoreCase);
                str = re.Replace(str, @"\]");
                re = null;

                re = new Regex(@"\?", RegexOptions.IgnoreCase);
                str = re.Replace(str, @"\?");
                re = null;

                re = new Regex(@"\(", RegexOptions.IgnoreCase);
                str = re.Replace(str, @"\(");
                re = null;

                re = new Regex(@"\)", RegexOptions.IgnoreCase);
                str = re.Replace(str, @"\)");
                re = null;

                conStr = str;

            }
            else
            {
                conStr = str;
            }
            return conStr;
        }

    }
}
