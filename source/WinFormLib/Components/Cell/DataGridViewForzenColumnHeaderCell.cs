using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinFormLib.Core;

namespace WinFormLib.Components.Cell
{
    public class DataGridViewForzenColumnHeaderCell : DataGridViewColumnHeaderCell
    {
        public DataGridViewForzenColumnHeaderCell()
        {

        }
        public DataGridViewForzenColumnHeaderCell(DataGridViewColumnHeaderCell oldHeaderCell)
        {
            this.ContextMenuStrip = oldHeaderCell.ContextMenuStrip;
            this.ErrorText = oldHeaderCell.ErrorText;
            this.Tag = oldHeaderCell.Tag;
            this.ToolTipText = oldHeaderCell.ToolTipText;
            this.Value = oldHeaderCell.Value;
            this.ValueType = oldHeaderCell.ValueType;
        }
        /// <summary>
        /// Creates an exact copy of this cell.
        /// </summary>
        /// <returns>An object that represents the cloned DataGridViewAutoFilterColumnHeaderCell.</returns>
        public override object Clone()
        {
            return new DataGridViewForzenColumnHeaderCell(this);
        }
        System.Windows.Forms.VisualStyles.PushButtonState buttonState = System.Windows.Forms.VisualStyles.PushButtonState.Default;

        protected override void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates dataGridViewElementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            DataGridViewAutoForzenColumn column = this.OwningColumn as DataGridViewAutoForzenColumn;

            if (column != null && column.ShowRowDetails == true)
            {
                var font = this.OwningColumn.DefaultCellStyle.Font;
                if (font == null)
                {
                    font = this.DataGridView.ColumnHeadersDefaultCellStyle.Font;
                }
                ButtonRenderer.DrawButton(graphics, cellBounds, this.OwningColumn.HeaderText, font, false, buttonState);
            }
        }

        protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.buttonState = System.Windows.Forms.VisualStyles.PushButtonState.Pressed;
                this.DataGridView.InvalidateCell(this);
                if (this.DataGridView.SelectedRows.Count > 0)
                {
                    FrmDataGridViewRow FDGVR = new FrmDataGridViewRow(this.DataGridView);
                    FDGVR.ShowDialog();
                    FDGVR.Dispose();
                    this.buttonState = System.Windows.Forms.VisualStyles.PushButtonState.Default;
                    this.DataGridView.InvalidateCell(this);
                }
            }
        }
        /// <summary>
        /// Displays the drop-down filter list. 
        /// </summary>
        public void ShowDropDownList()
        {

        }
    }
}
