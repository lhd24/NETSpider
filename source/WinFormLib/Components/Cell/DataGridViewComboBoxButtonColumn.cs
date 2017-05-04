using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using WinFormLib.Core;

namespace WinFormLib.Components.Cell
{
    public class ComboBoxButtonCellEventArgs : EventArgs
    {

    }
    [ToolboxBitmap(typeof(ComboBox))]
    public class DataGridViewComboBoxButtonColumn : DataGridViewColumn
    {
        public DataGridViewComboBoxButtonColumn()
            : base(new DataGridViewComboBoxButtonCell())
        {
            _DataKeyValueList = new KeyValueCollection();
        }
        private KeyValueCollection _DataKeyValueList;
        [Browsable(false), RefreshProperties(RefreshProperties.None)]
        public KeyValueCollection DataSource
        {
            get { return _DataKeyValueList; }
            set { if (value != null) _DataKeyValueList = value; }
        }
        public event RapidHandler<object, ComboBoxButtonCellEventArgs> OnCellButtonClick;

        internal void CellButtonClick(object sender, ComboBoxButtonCellEventArgs e)
        {
            if (OnCellButtonClick != null)
            {
                OnCellButtonClick(sender, e);
            }
        }

        public override object Clone()
        {
            DataGridViewComboBoxButtonColumn column1 = (DataGridViewComboBoxButtonColumn)base.Clone();
            column1.DataSource = this.DataSource;
            column1.OnCellButtonClick = this.OnCellButtonClick;
            column1.CellTemplate = new DataGridViewComboBoxButtonCell();
            return column1;
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
                    !value.GetType().IsAssignableFrom(typeof(DataGridViewComboBoxButtonCell)))
                {
                    throw new InvalidCastException("Must be a DataGridViewComboBoxButtonCell");
                }

                base.CellTemplate = value;
            }
        }
    }
}
