using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinFormLib.Core;
using System.Drawing;
using System.ComponentModel;

namespace WinFormLib.Components.Cell
{
    [ToolboxBitmap(typeof(Button))]
    public class DataGridViewTextBoxButtonColumn : DataGridViewColumn
    {
        public DataGridViewTextBoxButtonColumn()
            : base(new DataGridViewTextBoxButtonCell())
        {
        }
        public event RapidHandler<object, TextBoxButtonCellEventArgs> OnCellButtonClick;
        internal void CellButtonClick(object sender, TextBoxButtonCellEventArgs e)
        {
            if (OnCellButtonClick != null)
            {
                OnCellButtonClick(sender, e);
            }
        }
        private DataCellType _dataCellType = DataCellType.None;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DataCellType DataCellType
        {
            get { return _dataCellType; }
            set
            {
                _dataCellType = value;
            }
        }
        private string _dataCellTypeFormater = string.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string DataCellTypeFormater
        {
            get { return _dataCellTypeFormater; }
            set { _dataCellTypeFormater = value; }
        }
        public override object Clone()
        {
            DataGridViewTextBoxButtonColumn column1 = (DataGridViewTextBoxButtonColumn)base.Clone();
            column1.ColumnKey = this.ColumnKey;
            column1.ColumnWhere = this.ColumnWhere;
            column1.ControlWhere = this.ControlWhere;
            column1.DataCellTypeFormater = this.DataCellTypeFormater;
            column1.OnCellButtonClick = this.OnCellButtonClick;
            column1.DataCellType = this.DataCellType;
            column1.CellTemplate = new DataGridViewTextBoxButtonCell();
            return column1;
        }
        [Description("弹出窗Key")]
        public string ColumnKey { get; set; }
        [Description("条件列名")]
        public string ColumnWhere { get; set; }
        [Description("单头控件名")]
        public string ControlWhere { get; set; }

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
                    !value.GetType().IsAssignableFrom(typeof(DataGridViewTextBoxButtonCell)))
                {
                    throw new InvalidCastException("Must be a DataGridViewTextBoxButtonCell");
                }

                base.CellTemplate = value;
            }
        }

        #region IDataGridViewColumn 成员



        #endregion
    }
}
