using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinFormLib.Core;
using System.Windows.Forms;
using WinFormLib.Controls;

namespace WinFormLib.Components.Cell
{
    public class DataGridViewComboBoxButton : UserControl
    {

        public DataGridViewComboBoxButton()
        {
            InitializeComponent();
            cbDataEditor.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDataEditor.SelectedIndexChanged += new EventHandler(cbDataSource_SelectedIndexChanged);
            this.Init("");
        }

        public void Init(object Value)
        {
            if (!string.IsNullOrEmpty(TryParse.ToString(Value)))
            {
                KeyValue item = _DataKeyList.Find(q => TryParse.ToString(q.DisplayMember) == TryParse.ToString(Value));
                if (item != null)
                {
                    cbDataEditor.SelectedValue = item.ValueMember;
                }
            }
        }
        void cbDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetEntryValue();
            if (OnTextBoxButtonChanged != null)
            {
                OnTextBoxButtonChanged(sender, e);
            }
        }

        private void SetEntryValue()
        {
            if (cbDataEditor.SelectedValue == null)
                return;
            this.TextValue = TryParse.ToString(cbDataEditor.SelectedValue);
        }

        public event RapidHandler<object, EventArgs> OnTextBoxButtonChanged;
        private List<KeyValue> _DataKeyList;
        public void InitDataSource(KeyValueCollection DataKeyList)
        {
            this._DataKeyList = DataKeyList;
            cbDataEditor.DataSource = DataKeyList;
            cbDataEditor.DisplayMember = "DisplayMember";
            cbDataEditor.ValueMember = "ValueMember";
        }

        public object Value
        {
            get
            {
                return cbDataEditor.SelectedValue;
            }
            set
            {
                cbDataEditor.SelectedValue = value;
            }
        }
        public object TextValue
        {
            get;
            set;
        }


        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.cbDataEditor = new ComboBoxExt();
            this.SuspendLayout();
            // 
            // cbDataEditor
            // 
            this.cbDataEditor.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.cbDataEditor.DataControlName = "cbDataEditor";
            this.cbDataEditor.DataFiled = "";
            this.cbDataEditor.DataFormater = DataCellType.Text;
            this.cbDataEditor.DataViewValue = "";
            this.cbDataEditor.DefaultValue = null;
            this.cbDataEditor.DisenableBackColor = System.Drawing.SystemColors.Control;
            this.cbDataEditor.DisenableForeColor = System.Drawing.Color.Green;
            this.cbDataEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbDataEditor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDataEditor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cbDataEditor.FormattingEnabled = true;
            this.cbDataEditor.LineColor = System.Drawing.Color.DodgerBlue;
            this.cbDataEditor.Location = new System.Drawing.Point(0, 0);
            this.cbDataEditor.Name = "cbDataEditor";
            this.cbDataEditor.ReadOnlyValue = true;
            this.cbDataEditor.Size = new System.Drawing.Size(130, 20);
            this.cbDataEditor.TabIndex = 0;
            // 
            // DataGridViewComboBoxButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbDataEditor);
            this.Name = "DataGridViewComboBoxButton";
            this.Size = new System.Drawing.Size(130, 21);
            this.ResumeLayout(false);

        }

        #endregion

        protected ComboBoxExt cbDataEditor;
    }
}
