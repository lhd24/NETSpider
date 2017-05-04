using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinFormLib.Core;

namespace WinFormLib.Components.Cell
{
    public class DataGridViewAutoFilterTextBoxCell : DataGridViewTextBoxCell
    {
        public DataGridViewAutoFilterTextBoxCell()
            : base()
        {

        }

        public override object DefaultNewRowValue
        {
            get
            {
                return base.DefaultNewRowValue;
            }
        }
        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter valueTypeConverter, System.ComponentModel.TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            if (value != null)
            {
                DataGridViewAutoFilterTextBoxColumn column = this.OwningColumn as DataGridViewAutoFilterTextBoxColumn;
                if (column != null)
                {
                    if (column.DataCellType == DataCellType.CharToDate)
                    {
                        return TryParse.DateToStrByChar(value);
                    }
                    else if (column.DataCellType == DataCellType.DateToChar && value.GetType().GetUnderlyingType() == typeof(DateTime) && !string.IsNullOrEmpty(column.DataCellTypeFormater))
                    {
                        return TryParse.DateToStr(value, column.DataCellTypeFormater);
                    }
                }
            }
            return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
        }
        public override Type ValueType
        {
            get
            {
                // Return the type of the value that CalendarCell contains.
                return typeof(string);
            }
        }
    }
}
