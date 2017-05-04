using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace WinFormLib.Core
{
    public class BaseEntityHelper
    {
        #region KeyValue


        static KeyValueCollection _TopCountKeyValue;
        public static KeyValueCollection TopCountKeyValue
        {
            get
            {
                if (_TopCountKeyValue == null)
                {
                    _TopCountKeyValue = new KeyValueCollection();
                    _TopCountKeyValue.Add(new KeyValue() { ValueMember = " 50", DisplayMember = "50" });
                    _TopCountKeyValue.Add(new KeyValue() { ValueMember = " 100", DisplayMember = "100" });
                    _TopCountKeyValue.Add(new KeyValue() { ValueMember = " 500", DisplayMember = "500" });
                    _TopCountKeyValue.Add(new KeyValue() { ValueMember = " 1000", DisplayMember = "1000" });
                    //_TopCountKeyValue.Add(new KeyValue() { ValueMember = "", DisplayMember = "*" });
                }
                return _TopCountKeyValue;
            }

        }

        static KeyValueCollection _OperatorKeyValue;
        public static KeyValueCollection OperatorKeyValue
        {
            get
            {
                if (_OperatorKeyValue == null)
                {
                    _OperatorKeyValue = new KeyValueCollection();
                    _OperatorKeyValue.Add(new KeyValue() { ValueMember = "= {0}", DisplayMember = "=" });
                    _OperatorKeyValue.Add(new KeyValue() { ValueMember = "> {0}", DisplayMember = ">" });
                    _OperatorKeyValue.Add(new KeyValue() { ValueMember = "< {0}", DisplayMember = "<" });
                    _OperatorKeyValue.Add(new KeyValue() { ValueMember = ">= {0}", DisplayMember = ">=" });
                    _OperatorKeyValue.Add(new KeyValue() { ValueMember = "<= {0}", DisplayMember = "<=" });
                    _OperatorKeyValue.Add(new KeyValue() { ValueMember = "<> {0}", DisplayMember = "<>" });
                    _OperatorKeyValue.Add(new KeyValue() { ValueMember = "LIKE '{0}%'", DisplayMember = "LIKE%" });
                    _OperatorKeyValue.Add(new KeyValue() { ValueMember = "LIKE '%{0}%'", DisplayMember = "LIKE" });
                }
                return _OperatorKeyValue;
            }

        }


        static KeyValueCollection _YesNoKeyValue;
        public static KeyValueCollection YesNoKeyValue
        {
            get
            {
                if (_YesNoKeyValue == null)
                {
                    _YesNoKeyValue = new KeyValueCollection();
                    _YesNoKeyValue.Add(new KeyValue() { ValueMember = "Y", DisplayMember = "是 : Y" });
                    _YesNoKeyValue.Add(new KeyValue() { ValueMember = "N", DisplayMember = "否 : N" });
                }
                return _YesNoKeyValue;
            }
        }
        static KeyValueCollection _AvailablyKeyValue;
        public static KeyValueCollection AvailablyKeyValue
        {
            get
            {
                if (_AvailablyKeyValue == null)
                {
                    _AvailablyKeyValue = new KeyValueCollection();
                    _AvailablyKeyValue.Add(new KeyValue() { ValueMember = "Y", DisplayMember = "有效 : Y" });
                    _AvailablyKeyValue.Add(new KeyValue() { ValueMember = "N", DisplayMember = "无效 : N" });
                }
                return _AvailablyKeyValue;
            }
        }
        static KeyValueCollection _AndOrKeyValue;
        public static KeyValueCollection AndOrKeyValue
        {
            get
            {
                if (_AndOrKeyValue == null)
                {
                    _AndOrKeyValue = new KeyValueCollection();
                    _AndOrKeyValue.Add(new KeyValue() { ValueMember = "AND", DisplayMember = " AND " });
                    _AndOrKeyValue.Add(new KeyValue() { ValueMember = "OR", DisplayMember = " OR " });
                }
                return _AndOrKeyValue;
            }

        }

        #endregion
        public static void BindComboBox(ComboBox cbQuery, KeyValueCollection KeyValueList)
        {
            cbQuery.DataSource = KeyValueList;
            cbQuery.DisplayMember = "DisplayMember";
            cbQuery.ValueMember = "ValueMember";
        }
        public static bool GetValue(ref object value, PropertyInfo propertyInfo, string DataControlName)
        {
            if (propertyInfo.PropertyType == typeof(int))
            {
                value = TryParse.StrToInt(value, 0);
            }
            else if (propertyInfo.PropertyType == typeof(decimal))
            {
                value = TryParse.StrToDecimal(value, 0);
            }
            else if (propertyInfo.PropertyType == typeof(float))
            {
                value = TryParse.StrToFloat(value, 0);
            }
            else if (propertyInfo.PropertyType == typeof(DateTime))
            {
                value = TryParse.StrToDate(value, DateTime.MinValue);
            }
            else if (propertyInfo.PropertyType == typeof(string))
            {
                value = TryParse.ToString(value, string.Empty);
                object[] EntityBaseAttr = propertyInfo.GetCustomAttributes(typeof(EntityBaseAttribute), false);
                if (EntityBaseAttr != null && EntityBaseAttr.Length > 0)
                {
                    int maxLength = ((EntityBaseAttribute)EntityBaseAttr[0]).MaxLength;
                    if (value.ToString().Length > maxLength)
                    {
                        MessageBoxHelper.ShowError(DataControlName + "超出最大长度" + maxLength);
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
