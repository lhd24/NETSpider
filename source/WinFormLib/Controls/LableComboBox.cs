using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinFormLib.Core;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;

namespace WinFormLib.Controls
{
    [ToolboxBitmap(typeof(ComboBox))]
    [DefaultEvent("OnDataControlValueChanged")]
    public partial class LableComboBox : UserControl, IDataControl<string>
    {
        public LableComboBox()
        {
            InitializeComponent();
        }
        [Browsable(true)]
        public event EventHandler OnDataControlValueChanged;
        public object DataSource
        {
            get { return cbValueMember.DataSource; }
            set
            {
                cbValueMember.DataSource = value;
                if (value is List<KeyValue>)
                {
                    cbValueMember.DisplayMember = "DisplayMember";
                    cbValueMember.ValueMember = "ValueMember";

                }
                if (cbValueMember.Items.Count > 0)
                {
                    cbValueMember.SelectedIndex = 0;
                }
            }
        }
        [DefaultValue("文本信息"), Browsable(true)]
        public string DataControlName
        {
            get { return lbTextInfo.Text; }
            set { this.Text = this.lbTextInfo.Text = value; }
        }


        private bool _Required;
        [DefaultValue(false), Browsable(true)]
        public bool Required
        {
            get { return _Required; }
            set { _Required = value; }
        }

        private void cbValueMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            IFormChanged form = this.FindForm() as IFormChanged;
            if (form != null)
            {
                form.FormChanged = true;
            }
            if (OnDataControlValueChanged != null)
            {
                OnDataControlValueChanged(sender, e);
            }
        }



        #region IDataControl<string> 成员

        public string Value
        {
            get
            {
                if (cbValueMember.SelectedIndex != -1)
                {
                    return cbValueMember.SelectedValue.ToString();
                }
                return string.Empty;
            }
        }

        public string DataViewValue
        {
            get
            {
                return "";
            }
            set
            {

            }
        }

        public string DefaultValue
        {
            get
            {
                return "";
            }
            set
            {

            }
        }

        #endregion

        #region IDataControlFomatter<string> 成员

        public string ValueFomatted(string source, DataCellType dataFormater)
        {
            return source;
        }

        #endregion

        #region IDataControl 成员

        public bool ReadOnlyValue
        {
            get
            {
                return !cbValueMember.Enabled;
            }
            set
            {
                cbValueMember.Enabled = !value;
            }
        }
        public bool FillEntity(ref object Entity)
        {
            if (Entity == null)
                return true;
            if (string.IsNullOrEmpty(this.DataFiled))
                return true;
            if (string.IsNullOrEmpty(this.Value))
                return true;

            PropertyInfo propertyInfo = Entity.GetType().GetProperty(this.DataFiled);
            if (propertyInfo == null)
            {
                return true;
            }
            object filedValue = this.Value;
            if (BaseEntityHelper.GetValue(ref filedValue, propertyInfo, this.DataControlName))
            {
                propertyInfo.SetValue(Entity, filedValue, null);
            }
            return true;
        }
        public void UnFillEntity(object Entity)
        {
            if (Entity == null) return;
            if (string.IsNullOrEmpty(this.DataFiled)) return;
            PropertyInfo propertyInfo = Entity.GetType().GetProperty(this.DataFiled);
            if (propertyInfo == null) return;
            object EntityValue = propertyInfo.GetValue(Entity, null);
            if (EntityValue != null)
            {
                this.DataViewValue = EntityValue.ToString();
            }
            else
                this.DataViewValue = "";
        }
        public void ClearData()
        {

        }
        public string IsValid()
        {
            return string.Empty;
        }

        public DataCellType DataFormater
        {
            get;
            set;
        }

        public string DataFiled
        {
            get;
            set;
        }
        #endregion
        [DefaultValue(false)]
        public bool TextInfoVisible
        {
            get { return lbTextInfo.Visible; }
            set
            {
                if (value)
                {
                    lbTextInfo.Visible = true;
                    cbValueMember.Dock = DockStyle.None;
                }
                else
                {
                    lbTextInfo.Visible = false;
                    cbValueMember.Dock = DockStyle.Fill;
                }
            }
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
            this.lbTextInfo = new System.Windows.Forms.Label();
            this.cbValueMember = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lbTextInfo
            // 
            this.lbTextInfo.Location = new System.Drawing.Point(0, 0);
            this.lbTextInfo.Name = "lbTextInfo";
            this.lbTextInfo.Size = new System.Drawing.Size(73, 20);
            this.lbTextInfo.TabIndex = 0;
            this.lbTextInfo.Text = "文本信息";
            this.lbTextInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbValueMember
            // 
            this.cbValueMember.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbValueMember.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbValueMember.FormattingEnabled = true;
            this.cbValueMember.Location = new System.Drawing.Point(73, 0);
            this.cbValueMember.Name = "cbValueMember";
            this.cbValueMember.Size = new System.Drawing.Size(120, 20);
            this.cbValueMember.TabIndex = 1;
            this.cbValueMember.SelectedIndexChanged += new System.EventHandler(this.cbValueMember_SelectedIndexChanged);
            // 
            // LableComboBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.Controls.Add(this.cbValueMember);
            this.Controls.Add(this.lbTextInfo);
            this.Name = "LableComboBox";
            this.Size = new System.Drawing.Size(192, 20);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbTextInfo;
        private System.Windows.Forms.ComboBox cbValueMember;
    }
}
