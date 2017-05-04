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
    public class DataGridViewForzenColumn : DataGridViewColumn
    {
        public DataGridViewForzenColumn()
            : base(new DataGridViewForzenCell())
        {
            base.DefaultHeaderCellType = typeof(DataGridViewForzenColumnHeaderCell);
            this.Width = 60;
            this.Frozen = true;
            this.ReadOnly = true;          

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
        public override object Clone()
        {
            DataGridViewForzenColumn column1 = (DataGridViewForzenColumn)base.Clone();
            column1.IsAutoValue = this.IsAutoValue;
            column1.ForzenColumnLength = this.ForzenColumnLength;
            column1.CellTemplate = new DataGridViewForzenCell();
            return column1;
        }
        private int _ForzenColumnLength = 4;
        [Browsable(true), DefaultValue(4)]
        public int ForzenColumnLength
        {
            get { return _ForzenColumnLength; }
            set { _ForzenColumnLength = value; }
        }
        private bool _IsAutoValue = false;
        [Browsable(true), DefaultValue(false), Description("是否自动给值")]
        public bool IsAutoValue
        {
            get { return _IsAutoValue; }
            set { _IsAutoValue = value; }
        }
    }
}
