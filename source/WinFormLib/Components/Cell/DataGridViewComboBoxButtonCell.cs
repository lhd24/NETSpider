using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinFormLib.Core;

namespace WinFormLib.Components.Cell
{
    public class DataGridViewComboBoxButtonCell : DataGridViewTextBoxCell
    {
        public DataGridViewComboBoxButtonCell()
            : base()
        {

        }

        public override Type FormattedValueType
        {
            get { return typeof(string); }
        }
        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter valueTypeConverter, System.ComponentModel.TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            if (value != null)
            {
                DataGridViewComboBoxButtonColumn dgcb = this.DataGridView.Columns[this.ColumnIndex] as DataGridViewComboBoxButtonColumn;
                if (dgcb != null)
                {
                    KeyValue item = dgcb.DataSource.Find(q => TryParse.ToString(q.ValueMember) == TryParse.ToString(value));
                    if (item != null)
                    {
                        this.Value = item.ValueMember;
                        return item.DisplayMember.ToString();
                    }
                }
            }
            return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
        }
        public override object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter formattedValueTypeConverter, System.ComponentModel.TypeConverter valueTypeConverter)
        {
            if (formattedValue == null)
            {
                return string.Empty;
            }
            return base.ParseFormattedValue(formattedValue, cellStyle, formattedValueTypeConverter, valueTypeConverter);
        }
        public override void InitializeEditingControl(int rowIndex, object
        initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            // Set the value of the editing control to the current cell value.
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);

            DataGridViewComboBoxButtonColumn dgcb = this.DataGridView.Columns[this.ColumnIndex] as DataGridViewComboBoxButtonColumn;
            ComboBoxButtonEditingControl ctl = DataGridView.EditingControl as ComboBoxButtonEditingControl;

            ctl.InitDataSource(dgcb.DataSource);
            ctl.Init(initialFormattedValue);
            ctl.BackColor = dataGridViewCellStyle.SelectionBackColor;
            ctl.ColumnIndex = this.ColumnIndex;
        }

        public override Type EditType
        {
            get
            {
                // Return the type of the editing contol that CalendarCell uses.
                return typeof(ComboBoxButtonEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                // Return the type of the value that CalendarCell contains.
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
