using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace WinFormLib.Components.Cell
{
    [ToolboxBitmap(typeof(TextBox))]
    public class DataGridViewAutoForzenColumn : DataGridViewColumn
    {
        public DataGridViewAutoForzenColumn()
            : base(new DataGridViewForzenCell())
        {
            base.DefaultHeaderCellType = typeof(DataGridViewForzenColumnHeaderCell);
            this.Width = 60;
            this.Frozen = true;
            this.ReadOnly = true;
        }

        private Boolean _ShowRowDetails = true;
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Boolean ShowRowDetails
        {
            get { return _ShowRowDetails; }
            set { _ShowRowDetails = value; }
        }
        /// <summary>
        /// Returns the AutoFilter header cell type. This property hides the 
        /// non-virtual DefaultHeaderCellType property inherited from the 
        /// DataGridViewBand class. The inherited property is set in the 
        /// DataGridViewAutoFilterTextBoxColumn constructor. 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Type DefaultHeaderCellType
        {
            get
            {
                return typeof(DataGridViewForzenColumnHeaderCell);
            }
        }
        protected override void OnDataGridViewChanged()
        {
            base.OnDataGridViewChanged();
            if (this.DataGridView != null)
            {
                this.DataGridView.RowPostPaint += new DataGridViewRowPostPaintEventHandler(DataGridView_RowPostPaint);
            }
        }

        void DataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (this.DataGridView == null) return;
            //添加行号
            using (SolidBrush b = new SolidBrush(this.DataGridView.RowHeadersDefaultCellStyle.ForeColor))
            {
                string linenum = e.RowIndex.ToString();
                int linen = Convert.ToInt32(linenum) + 1;
                string line = linen.ToString();
                this.DataGridView[this.Index, e.RowIndex].Value = line;
                //e.Graphics.DrawString(line, e.InheritedRowStyle.Font, b, e.RowBounds.Location.X, e.RowBounds.Location.Y + 5);
            }

        }
        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                // Ensure that the cell used for the template is a CalendarCell.
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(DataGridViewForzenCell)))
                {
                    throw new InvalidCastException("Must be a DataGridViewForzenCell");
                }

                base.CellTemplate = value;
            }
        }
    }
}
