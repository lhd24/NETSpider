using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinFormLib.Core
{

    public enum EnumDataType
    {
        String = 1,
        Int = 2,
    }

    public enum EnumDataSearchType
    {
        Normal = 1,
        High = 2,
        Inherited = 3,
    }
    public class SearchToolTF
    {
        private string _tf001;
        private string _tf002;
        private string _tf003;
        private EnumDataSearchType _tf004;
        private string _tf005;
        private string _tf006;
        private EnumDataType _tf007;
        private string _tf008;

        /// <summary>
        /// 序号
        /// </summary>
        public string TF001
        {
            set { _tf001 = value; }
            get { return _tf001; }
        }
        /// <summary>
        /// 查询字段
        /// </summary>
        public string TF002
        {
            set { _tf002 = value; }
            get { return _tf002; }
        }
        /// <summary>
        /// 查询显示
        /// </summary>
        public string TF003
        {
            set { _tf003 = value; }
            get { return _tf003; }
        }
        /// <summary>
        /// 一般查询/高级查询
        /// </summary>
        public EnumDataSearchType TF004
        {
            set { _tf004 = value; }
            get { return _tf004; }
        }
        /// <summary>
        /// 操作符
        /// </summary>
        public string TF005
        {
            set { _tf005 = value; }
            get { return _tf005; }
        }
        /// <summary>
        /// 条件
        /// </summary>
        public string TF006
        {
            set { _tf006 = value; }
            get { return _tf006; }
        }
        /// <summary>
        /// 数字/字符串类型,EnumDataType
        /// </summary>
        public EnumDataType TF007
        {
            set { _tf007 = value; }
            get { return _tf007; }
        }
        /// <summary>
        /// 与或符
        /// </summary>
        public string TF008
        {
            set { _tf008 = value; }
            get { return _tf008; }
        }
    }
}
