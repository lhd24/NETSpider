using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using WinFormLib.Core;
using System.Drawing;

namespace WinFormLib.Components.Cell
{
    public class DataGridViewForzenCell : DataGridViewTextBoxCell
    {
        public DataGridViewForzenCell()
            : base()
        {
            this.ReadOnly = true;
        }

        protected override void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value,
            object formattedValue, string errorText, DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            
            cellStyle.BackColor = cellStyle.SelectionBackColor = System.Drawing.SystemColors.Control;
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
        }

        public override void PositionEditingControl(bool setLocation, bool setSize, Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
        {
            base.PositionEditingControl(setLocation, setSize, cellBounds, cellClip, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
        }
        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            if (this.DataGridView != null && this.ColumnIndex >= 0)
            {
                if (this.DataGridView.Columns[this.ColumnIndex] is DataGridViewForzenColumn)
                {
                    DataGridViewForzenColumn ForzenColumn = this.DataGridView.Columns[this.ColumnIndex] as DataGridViewForzenColumn;
                    if (ForzenColumn.IsAutoValue)
                    {
                        int NextValue = 1;
                        if (rowIndex > 0)
                        {
                            NextValue = TryParse.StrToInt(this.DataGridView[this.ColumnIndex, rowIndex - 1].FormattedValue) + 1;
                        }
                        string returnValue = ("000000000" + NextValue);
                        value = returnValue.Substring(returnValue.Length - ForzenColumn.ForzenColumnLength);
                        this.SetValue(rowIndex, value);
                    }
                }
            }
            return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
        }

        public override bool ReadOnly
        {
            get
            {
                return true;
            }
        }
        public override Type ValueType
        {
            get
            {
                return typeof(string);
            }
        }
    }
}
