using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using WinFormLib.Core;
using System.Reflection;
using System.Drawing;

namespace WinFormLib.Controls
{
    public class LableTextBox : UserControl, IDataControl<string>
    {
        public LableTextBox()
        {
            InitializeComponent();
        }
        public RichTextBox txtValue;
        private ContextMenuStrip contextMenuStrip1;
        private IContainer components;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private Label lbTextInfo;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lbTextInfo = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTextInfo
            // 
            this.lbTextInfo.BackColor = System.Drawing.SystemColors.Control;
            this.lbTextInfo.Location = new System.Drawing.Point(0, 0);
            this.lbTextInfo.Name = "lbTextInfo";
            this.lbTextInfo.Size = new System.Drawing.Size(73, 20);
            this.lbTextInfo.TabIndex = 0;
            this.lbTextInfo.Text = "文本信息";
            this.lbTextInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtValue
            // 
            this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValue.BackColor = System.Drawing.Color.Beige;
            this.txtValue.ContextMenuStrip = this.contextMenuStrip1;
            this.txtValue.DetectUrls = false;
            this.txtValue.Location = new System.Drawing.Point(73, 0);
            this.txtValue.Multiline = false;
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(136, 22);
            this.txtValue.TabIndex = 1;
            this.txtValue.Text = "";
            this.txtValue.TextChanged += new System.EventHandler(this.txtValue_TextChanged);
            this.txtValue.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtValue_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.contextMenuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.toolStripMenuItem4,
            this.toolStripSeparator1,
            this.toolStripMenuItem6,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripSeparator2,
            this.toolStripMenuItem3});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowItemToolTips = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(179, 148);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.toolStripMenuItem5.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem5.Text = "撤消";
            this.toolStripMenuItem5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.toolStripMenuItem5.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.Z)));
            this.toolStripMenuItem4.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem4.Text = "重做";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(175, 6);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.toolStripMenuItem6.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem6.Text = "剪切";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.toolStripMenuItem1.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem1.Text = "复制";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.toolStripMenuItem2.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem2.Text = "粘贴";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(175, 6);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.toolStripMenuItem3.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem3.Text = "全选";
            // 
            // LableTextBox
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.lbTextInfo);
            this.Name = "LableTextBox";
            this.Size = new System.Drawing.Size(209, 22);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        public event EventHandler RichTextChanged;
        [DefaultValue("文本信息")]
        public string DataControlName
        {
            set { lbTextInfo.Text = value; }
            get { return lbTextInfo.Text; }
        }

        #region IDataControl<string> 成员
        [Browsable(true)]
        public string Value
        {
            get { return ValueFomatted(txtValue.Text, this.DataFormater); }
        }
        private string _dataFiled = "";
        [DefaultValue("")]
        public string DataFiled
        {
            get { return _dataFiled; }
            set { _dataFiled = value; }
        }
        [Browsable(true)]
        public string DataViewValue
        {
            get
            {
                if (this.PasswordChar > 0)
                {
                    string result = "";
                    foreach (var ch in txtValue.Text)
                    {
                        result += this.PasswordChar;
                    }
                    return result;
                }
                return txtValue.Text;
            }
            set { txtValue.Text = value; }
        }
        [Browsable(true)]
        public char PasswordChar
        {
            get;
            set;
        }


        public string ValueFomatted(string source, DataCellType dataFormater)
        {
            return source;
        }

        public string IsValid()
        {
            if (this.Required)
            {
                if (string.IsNullOrEmpty(txtValue.Text))
                    return string.Format(lbTextInfo.Text + "不能为空");
            }

            return string.Empty;
        }

        private DataCellType _dataFormater = DataCellType.None;
        [DefaultValue(DataCellType.None)]
        public DataCellType DataFormater
        {
            get { return _dataFormater; }
            set { _dataFormater = value; }
        }
        private string _defaultValue;
        public string DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

        public bool ReadOnlyValue
        {
            get
            {
                return txtValue.ReadOnly;

            }
            set
            {
                txtValue.ReadOnly = value;
                if (!value)
                {
                    if (Required)
                        txtValue.BackColor = System.Drawing.Color.LightSkyBlue;
                    else
                        txtValue.BackColor = System.Drawing.Color.Beige;
                }
                else
                {
                    if (Required)
                        txtValue.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
                    else
                        txtValue.BackColor = System.Drawing.SystemColors.Control;
                }
            }
        }
        public void ClearData()
        {
            txtValue.Text = _defaultValue;
        }
        #endregion

        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            IFormChanged form = this.FindForm() as IFormChanged;
            if (form != null)
            {
                form.FormChanged = true;
            }
            if (RichTextChanged != null)
            {
                RichTextChanged(this, e);
            }

        }

        private bool _Required;
        [DefaultValue(false), Browsable(true), Description("获取或设置是否必填项")]
        public bool Required
        {
            get { return _Required; }
            set
            {
                _Required = value;
                if (Required)
                    txtValue.BackColor = System.Drawing.Color.LightSkyBlue;
                else
                    txtValue.BackColor = System.Drawing.Color.Beige;
            }
        }
        public RichTextBoxScrollBars ScrollBars
        {
            get { return txtValue.ScrollBars; }
            set { txtValue.ScrollBars = value; }
        }

        [DefaultValue(false)]
        public bool Multiline
        {
            get { return this.txtValue.Multiline; }
            set { txtValue.Multiline = value; }
        }
         [DefaultValue(BorderStyle.Fixed3D)]
        public BorderStyle RichBorderStyle
        {
            get { return this.txtValue.BorderStyle; }
            set { txtValue.BorderStyle = value; }
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
                return true;
            }
            return false;
        }

        [DefaultValue(false)]
        public bool TextInfoVisible
        {
            get { return lbTextInfo.Visible; }
            set
            {
                if (value)
                {
                    lbTextInfo.Visible = true;
                    txtValue.Dock = DockStyle.None;
                }
                else
                {
                    lbTextInfo.Visible = false;
                    txtValue.Dock = DockStyle.Fill;
                }
            }
        }


        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == toolStripMenuItem1)
            {
                txtValue.Copy();
            }
            else if (e.ClickedItem == toolStripMenuItem2)
            {
                txtValue.Paste();
            }
            else if (e.ClickedItem == toolStripMenuItem3)
            {
                txtValue.SelectAll();
            }
            else if (e.ClickedItem == toolStripMenuItem4)
            {
                txtValue.Redo();
            }
            else if (e.ClickedItem == toolStripMenuItem5)
            {
                txtValue.Undo();
            }
            else if (e.ClickedItem == toolStripMenuItem6)
            {
                txtValue.Cut();
            }
        }

        private void txtValue_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (txtValue.CanRedo)//redo
                    toolStripMenuItem4.Enabled = true;
                else
                    toolStripMenuItem4.Enabled = false;
                if (txtValue.CanUndo)//undo
                    toolStripMenuItem5.Enabled = true;
                else
                    toolStripMenuItem5.Enabled = false;
                if (txtValue.SelectionLength > 0)
                {
                    toolStripMenuItem1.Enabled = true;
                    toolStripMenuItem6.Enabled = true;
                }
                else
                {
                    toolStripMenuItem1.Enabled = false;
                    toolStripMenuItem6.Enabled = false;
                }
                if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
                    toolStripMenuItem2.Enabled = true;
                else
                    toolStripMenuItem2.Enabled = false;
                contextMenuStrip1.Show(txtValue, new Point(e.X, e.Y));
            }
        }
    }
}
