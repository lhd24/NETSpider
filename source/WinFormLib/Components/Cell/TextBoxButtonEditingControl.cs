using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinFormLib.Core;

namespace WinFormLib.Components.Cell
{
    public class TextBoxButtonEditingControl : DataGridViewTextBoxButton, IDataGridViewEditingControl
    {
        DataGridView dataGridView;
        private bool valueChanged = false;
        int rowIndex;
        private object _oldValue;

        public object OldValue
        {
            get { return _oldValue; }
            set { _oldValue = value; }
        }

        #region IDataGridViewEditingControl 成员
        public TextBoxButtonEditingControl()
        {
            this.OnTextBoxButtonChanged += new RapidHandler<object, EventArgs>(TextBoxButtonEditingControl_OnTextBoxButtonChanged);
            _oldValue = this.Value;
            txtEditor.SelectAll();
        }

        void TextBoxButtonEditingControl_OnTextBoxButtonChanged(object sender, EventArgs e)
        {
            if (this.OldValue != this.Value && !this.Value.Equals(this.OldValue))
            {
                this.OldValue = this.Value;
                valueChanged = true;
                this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            }
        }
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            txtEditor.BackColor = this.BackColor = dataGridViewCellStyle.BackColor;
        }

        public DataGridView EditingControlDataGridView
        {
            get
            {
                return dataGridView;
            }
            set
            {
                dataGridView = value;
            }
        }

        public object EditingControlFormattedValue
        {
            get
            {
                return this.Value;
            }
            set
            {
                this.Value = value as string;
            }
        }

        public int EditingControlRowIndex
        {
            get
            {
                return rowIndex;

            }
            set
            {
                rowIndex = value;
            }
        }
        private int _ColumnIndex;

        public int ColumnIndex
        {
            get { return _ColumnIndex; }
            set { _ColumnIndex = value; }
        }

        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }
            set
            {
                //valueChanged = false;
            }
        }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            if (keyData == Keys.F2)
            {
                OnButtonClicked(dataGridView, TextBoxButtonCellEventArgs.Empty);
                return false;
            }
            if (!txtEditor.Focused)
            {
                txtEditor.Focus();
            }
            return true;
        }

        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
            if (this.dataGridView.CurrentCell.Value == null)
                this.Value = "";
            else
                this.Value = this.dataGridView.CurrentCell.Value.ToString();
            if (selectAll)
                this.txtEditor.SelectAll();
        }


        public bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }

        #endregion
        protected override void OnButtonClicked(object sender, TextBoxButtonCellEventArgs e)
        {
            base.OnButtonClicked(sender, e);
            DataGridViewTextBoxButtonColumn dgvButton = (DataGridViewTextBoxButtonColumn)this.dataGridView.Columns[this.ColumnIndex];
            e.RowIndex = rowIndex;
            e.Value = Value;
            e.DataGridViewTextBoxButton = this;
            e.ColumnIndex = this.ColumnIndex;
            dgvButton.CellButtonClick(sender, e);
        }

    }
}
