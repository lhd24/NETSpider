using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace WinFormLib.Components.Cell
{
    public class DataGridViewCheckboxHeaderCell : DataGridViewColumnHeaderCell
    {
        Point checkBoxLocation;
        Size checkBoxSize;
        bool _checked = false;
        Point _cellLocation = new Point();
        System.Windows.Forms.VisualStyles.CheckBoxState _cbState =
            System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
        public event DataGridViewCheckBoxHeaderEventHander OnCheckBoxClicked;
        //绘制列头checkbox 
        protected override void Paint(System.Drawing.Graphics graphics,
          System.Drawing.Rectangle clipBounds,
          System.Drawing.Rectangle cellBounds,
          int rowIndex,
          DataGridViewElementStates dataGridViewElementState,
          object value,
          object formattedValue,
          string errorText,
          DataGridViewCellStyle cellStyle,
          DataGridViewAdvancedBorderStyle advancedBorderStyle,
          DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                dataGridViewElementState, value,
                formattedValue, errorText, cellStyle,
                advancedBorderStyle, paintParts);
            Point p = new Point();
            Size s = CheckBoxRenderer.GetGlyphSize(graphics,
            System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
            p.X = cellBounds.Location.X +
                (cellBounds.Width / 2) - (s.Width / 2) - 1;//列头checkbox的X坐标 
            p.Y = cellBounds.Location.Y +
                (cellBounds.Height / 2) - (s.Height / 2);//列头checkbox的Y坐标 
            _cellLocation = cellBounds.Location;
            checkBoxLocation = p;
            checkBoxSize = s;
            if (_checked)
                _cbState = System.Windows.Forms.VisualStyles.
                    CheckBoxState.CheckedNormal;
            else
                _cbState = System.Windows.Forms.VisualStyles.
                    CheckBoxState.UncheckedNormal;
            CheckBoxRenderer.DrawCheckBox
            (graphics, checkBoxLocation, _cbState);
        }

        /// <summary> 
        /// 点击列头checkbox单击事件 
        /// </summary> 
        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            Point p = new Point(e.X + _cellLocation.X, e.Y + _cellLocation.Y);
            if (p.X >= checkBoxLocation.X && p.X <=
                checkBoxLocation.X + checkBoxSize.Width
            && p.Y >= checkBoxLocation.Y && p.Y <=
                checkBoxLocation.Y + checkBoxSize.Height)
            {
                _checked = !_checked;

                //获取列头checkbox的选择状态 
                DataGridViewCheckboxHeaderEventArgs ex = new DataGridViewCheckboxHeaderEventArgs();
                ex.CheckedState = _checked;

                object sender = new object();//此处不代表选择的列头checkbox，只是作为参数传递。应该列头checkbox是绘制出来的，无法获得它的实例

                if (OnCheckBoxClicked != null)
                {
                    OnCheckBoxClicked(sender, ex);//触发单击事件 
                    this.DataGridView.InvalidateCell(this);
                }
            }
            base.OnMouseClick(e);
        }
    }


}
