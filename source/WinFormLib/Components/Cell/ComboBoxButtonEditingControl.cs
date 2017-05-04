using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinFormLib.Core;

namespace WinFormLib.Components.Cell
{

    public class ComboBoxButtonEditingControl : DataGridViewComboBoxButton, IDataGridViewEditingControl
    {

        public ComboBoxButtonEditingControl()
        {
            this.OnTextBoxButtonChanged += new RapidHandler<object, EventArgs>(TextBoxButtonEditingControl_OnTextBoxButtonChanged);

            _oldValue = this.Value;
        }
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


        void TextBoxButtonEditingControl_OnTextBoxButtonChanged(object sender, EventArgs e)
        {
            if (this.OldValue != this.Value || !this.Value.Equals(this.OldValue))
            {
                this.OldValue = this.Value;
                valueChanged = true;
                this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            }
        }
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            cbDataEditor.BackColor = this.BackColor = dataGridViewCellStyle.BackColor;
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
        }


        public bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }

        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ComboBoxButtonEditingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "ComboBoxButtonEditingControl";
            this.ResumeLayout(false);

        }

    }
}
