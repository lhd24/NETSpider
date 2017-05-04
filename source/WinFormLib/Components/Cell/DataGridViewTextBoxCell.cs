using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinFormLib.Core;

namespace WinFormLib.Components.Cell
{
    public class DataGridViewTextBoxButtonCell : DataGridViewTextBoxCell
    {
        public DataGridViewTextBoxButtonCell()
            : base()
        {
        }

        public override void InitializeEditingControl(int rowIndex, object
        initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            // Set the value of the editing control to the current cell value.
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);

            TextBoxButtonEditingControl ctl = DataGridView.EditingControl as TextBoxButtonEditingControl;
            DataGridViewTextBoxButtonColumn column = this.OwningColumn as DataGridViewTextBoxButtonColumn;
            if (column != null)
            {
                ctl.DataFormater = column.DataCellType;
            }
            ctl.BackColor = dataGridViewCellStyle.SelectionBackColor;
            ctl.ColumnIndex = this.ColumnIndex;
            ctl.SelectAll();
        }
        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter valueTypeConverter, System.ComponentModel.TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            if (value != null)
            {
                DataGridViewTextBoxButtonColumn column = this.OwningColumn as DataGridViewTextBoxButtonColumn;
                if (column != null)
                {
                    if (column.DataCellType == DataCellType.DateToChar &&!string.IsNullOrEmpty(column.DataCellTypeFormater))
                    {
                        return TryParse.DateToStrByChar(value,column.DataCellTypeFormater);
                    }
                }
            }
            return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
        }
        public override Type EditType
        {
            get
            {
                return typeof(TextBoxButtonEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                return typeof(string);
            }
        }
        protected override void OnKeyUp(KeyEventArgs e, int rowIndex)
        {
            if (e.KeyCode == Keys.F2)
            {

            }
            base.OnKeyUp(e, rowIndex);
        }
        public override object DefaultNewRowValue
        {
            get
            {
                return "";
            }
        }
    }

}
