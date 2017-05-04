using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinFormLib.Core;

namespace WinFormLib.Components.Cell
{
    partial class DataGridViewTextBoxButton
    {
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
            this.btnEditor = new System.Windows.Forms.Button();
            this.txtEditor = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnEditor
            // 
            this.btnEditor.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnEditor.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnEditor.Location = new System.Drawing.Point(109, 1);
            this.btnEditor.Name = "btnEditor";
            this.btnEditor.Size = new System.Drawing.Size(19, 19);
            this.btnEditor.TabIndex = 0;
            this.btnEditor.Text = "…";
            this.btnEditor.UseVisualStyleBackColor = false;
            this.btnEditor.Click += new System.EventHandler(this.btnEditor_Click);
            // 
            // txtEditor
            // 
            this.txtEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEditor.Location = new System.Drawing.Point(0, 0);
            this.txtEditor.Name = "txtEditor";
            this.txtEditor.Size = new System.Drawing.Size(129, 21);
            this.txtEditor.TabIndex = 1;
            this.txtEditor.TextChanged += new System.EventHandler(this.txtEditor_TextChanged);
            // 
            // DataGridViewTextBoxButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnEditor);
            this.Controls.Add(this.txtEditor);
            this.Name = "DataGridViewTextBoxButton";
            this.Size = new System.Drawing.Size(130, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Button btnEditor;
        protected System.Windows.Forms.TextBox txtEditor;
    }
    public partial class DataGridViewTextBoxButton : UserControl
    {
        public DataGridViewTextBoxButton()
        {
            InitializeComponent();
        }

        public event RapidHandler<object, EventArgs> OnTextBoxButtonClick;
        public event RapidHandler<object, EventArgs> OnTextBoxButtonChanged;
        private DataCellType _dataFormater = DataCellType.None;
        public DataCellType DataFormater
        {
            get { return _dataFormater; }
            set { _dataFormater = value; }
        }

        protected virtual void OnButtonClicked(object sender, TextBoxButtonCellEventArgs e)
        {

        }
        private void btnEditor_Click(object sender, EventArgs e)
        {
            if (OnTextBoxButtonClick != null)
            {
                OnTextBoxButtonClick(sender, e);
            }
            if (this.Parent.Parent is DataGridView)
            {
                OnButtonClicked(sender, TextBoxButtonCellEventArgs.Empty);
            }
        }
        public object Value
        {
            get
            {
                return txtEditor.Text;
            }
            set
            {
                txtEditor.Text = value as string;
            }
        }
        public void SelectAll()
        {
            txtEditor.SelectAll();
        }
        private void txtEditor_TextChanged(object sender, EventArgs e)
        {
            if (OnTextBoxButtonChanged != null)
            {
                OnTextBoxButtonChanged(sender, e);
            }
        }
    }
}
