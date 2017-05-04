using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace NETSpider
{

    public class EnumGloabParas
    {
        public enum EnumTriggerType
        {
            None = 0,
            /// <summary>
            /// //发布数据前执行子任务
            /// </summary>
            GetData = 1,
            /// <summary>
            /// //发布数据后执行子任务
            /// </summary>
            Publish = 2,
        }

        public enum EnumExpireType
        {
            [Description("不过期")]
            None = 0,
            [Description("次数过期")]
            Count = 1,
            [Description("以时间过期")]
            Time = 2,
        }
        public enum EnumPlanTaskType
        {
            [Description("采集任务")]
            Task = 1,
            [Description("数据库任务")]
            DataBase = 2,
            [Description("自定义任务")]
            Custom = 3,
        }
        public enum EnumPlanExcuteType
        {
            [Description("执行一次")]
            One = 1,
            [Description("每天执行")]
            Day = 2,
            [Description("每周执行")]
            Weekly = 3,
            [Description("自定义")]
            Custom = 4,
        }
        public enum EnumConnectionType
        {
            None = 0,
            ExportTxt = 1,
            ExportExcel = 2,
            ExportAccess = 3,
            ExportMSSQL = 4,
            ExportMySql = 5,
            ExportWeb = 6,
        }

        public enum EnumDownloadResult
        {
            Succeed = 0,
            Err = 1,
            PathErr = 2,
        }
        public enum EnumCmdType
        {
            /// <summary>
            /// 只采集数据
            /// </summary>
            [Description("None")]
            None = 3000,
            /// <summary>
            /// 只采集数据
            /// </summary>
            [Description("Regex")]
            Regex = 3001,
            /// <summary>
            /// 只采集数据
            /// </summary>
            [Description("Common")]
            Common = 3002,
            /// <summary>
            /// 只采集数据
            /// </summary>
            [Description("End")]
            End = 3003,
        }
        public enum EnumMessageType
        {
            INFO = 2000,
            ERROR = 2001,
            WARNING = 2002,
            NOTIFY = 2003,
        }
        public enum EnumThreadCompleteType
        {
            Success = 0,
            Error = 1,
            FileSuccess = 2,
            FileError = 3,
        }
        public enum EnumGatherTaskType
        {
            Noraml = -1,
            //GetUrl = 0,//只获取测试网页
            //GetTestData = 1,//只获取测试数据
            //GetTestHtml = 2,//获取网页源码
            //GetUrlCount = 3,//获取导航总数量
            RunData = 4,//运行数据
        }
        public enum EnumExcuteType
        {
            /// <summary>
            /// 只采集数据
            /// </summary>
            [Description("只采集数据")]
            GetOnly = 2001,
            [Description("采集并发布数据")]
            GetAndPublish = 2002,
        }
        public enum EnumDataFileType
        {
            [Description("文本")]
            Text = 2011,
            [Description("图片")]
            Image = 2012,
            [Description("文件")]
            File = 2013,
            [Description("Flash")]
            Flash = 2014,
        }

        public enum EnumDataTextType
        {
            [Description("标题")]
            Title = 2011,
            [Description("描述")]
            Remark = 2012,
        }
        public enum EnumTaskType
        {
            [Description("普通采集")]
            Normal = 2011,
            [Description("使用AJAX采集")]
            Ajax = 2012,
        }

        public enum EnumLimitSign
        {
            [Description("不做任意格式的限制")]
            LimitSign1 = 2011,
            [Description("匹配时去掉网页符号")]
            LimitSign2 = 2012,
            [Description("只匹配中文")]
            LimitSign3 = 2013,
            [Description("只匹配双字节字符")]
            LimitSign4 = 2014,
            [Description("只匹配数字")]
            LimitSign5 = 2015,
            [Description("只匹配字母数字及常用字符")]
            LimitSign6 = 2016,
            [Description("自定义正则匹配表达式")]
            LimitSign7 = 2017,
        }
        public enum EnumExportLimit
        {
            /// <summary>
            /// 不做输出控制
            /// </summary>
            [Description("不做输出控制")]
            ExportLimit1 = 2011,
            /// <summary>
            /// 输出时去掉网页符号
            /// </summary>
            [Description("输出时去掉网页符号")]
            ExportLimit2 = 2012,
            /// <summary>
            /// 左起去掉字符
            /// </summary>
            [Description("左起去掉字符")]
            ExportLimit3 = 2013,
            /// <summary>
            /// 右起去掉字符
            /// </summary>
            [Description("右起去掉字符")]
            ExportLimit4 = 2014,
            /// <summary>
            /// 去掉字符串首尾空格
            /// </summary>
            [Description("去掉字符串首尾空格")]
            ExportLimit5 = 2015,


            /// <summary>
            /// 输出时附加前缀
            /// </summary>
            [Description("输出时附加前缀")]
            ExportLimit6 = 2016,
            /// <summary>
            /// 输出时附加后缀
            /// </summary>
            [Description("输出时附加后缀")]
            ExportLimit7 = 2017,
            /// <summary>
            /// 替换其中符合条件的字符
            /// </summary>
            [Description("替换其中符合条件的字符")]
            ExportLimit8 = 2018,
            /// <summary>
            /// 输出时采用正则表达式进行替换
            /// </summary>
            [Description("输出时采用正则表达式进行替换")]
            ExportLimit9 = 2019,
        }

        public enum EnumEncodeType
        {
            [Description("AUTO")]
            AUTO = 2011,
            [Description("UTF-8")]
            UTF8 = 2012,
            [Description("GB2312")]
            GB2312 = 2013,
            [Description("BIG5")]
            BIG5 = 2014,
            [Description("GBK")]
            GBK = 2015,
        }

        public enum EnumThreadState
        {
            Normal = 0,//初始状态
            Run = 1,//正在运行
            Suspended = 2,//暂停中
            SpiderCompleted = 3,//采集已结束,执行下一任务,发布数据
            Completed = 10,//已结束
        }
        public enum EnumUrlGaterherState
        {
            First = 0,//主URL,如果主URL是最终状态,则为Run状态
            Run = 2,//最终页面,可获取到相关字段
            Error = 3,//出错页面
            Completed = 4,//完成读取页面.
            FirstError = 5,
            FileCompleted = 6,//这个页面是不用运行的,直接下载的,
            FileError = 7,//这个页面是不用运行的,直接下载的,
        }
    }
    public class ItemValue
    {
        public string name { get; set; }
        public string text { get; set; }
    }
    public class EnumHelper
    {
        public static String GetEnumDesc(Type type, string name)
        {
            FieldInfo EnumInfo = type.GetField(name);
            DescriptionAttribute[] EnumAttributes = (DescriptionAttribute[])EnumInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (EnumAttributes.Length > 0)
            {
                return EnumAttributes[0].Description;
            }
            return name.ToString();
        }

        public static void InitItemList(string name, WinFormLib.Controls.ComboBoxExt control)
        {
            control.Items.Clear();
            List<ItemValue> itemList = new List<ItemValue>();
            ItemValue entity = new ItemValue()
            {
                name = name,
                text = name,
            };
            itemList.Add(entity);
            control.DisplayMember = "name";
            control.ValueMember = "text";
            control.DataSource = itemList;
        }
        public static void InitItemList(Type type, WinFormLib.Controls.ComboBoxExt control)
        {
            control.Items.Clear();
            FieldInfo[] EnumInfo = type.GetFields();
            List<ItemValue> itemList = new List<ItemValue>();
            foreach (var item in EnumInfo)
            {
                DescriptionAttribute[] EnumAttributes = (DescriptionAttribute[])item.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (EnumAttributes.Length > 0)
                {
                    ItemValue entity = new ItemValue()
                    {
                        name = EnumAttributes[0].Description,
                        text = item.Name.ToString(),
                    };
                    itemList.Add(entity);
                }
            }

            control.DisplayMember = "name";
            control.ValueMember = "text";
            control.DataSource = itemList;
        }
    }

}
