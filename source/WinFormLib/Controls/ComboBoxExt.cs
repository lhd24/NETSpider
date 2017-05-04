using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using WinFormLib.Core;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace WinFormLib.Controls
{
    [ToolboxBitmap(typeof(ComboBox))]
    public class ComboBoxExt : ComboBox, IDataControl<object>
    {
        Label _label = new Mylabel();
        private System.ComponentModel.Container components = null;
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
        public ComboBoxExt()
        {
            InitializeComponent();
            _label.Visible = false;
            _label.BorderStyle = BorderStyle.None;
            _label.AutoSize = false;
            _label.ForeColor = Color.Green;
            _label.TextAlign = ContentAlignment.MiddleLeft;
            _label.EnabledChanged += new EventHandler(_label_EnabledChanged);
            this.ForeColor = Color.Black;
            this.Width = 150;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

        }

        private Color _linecolor = Color.DodgerBlue;
        /// <summary>
        /// 线条颜色
        /// </summary>
        public Color LineColor
        {
            get
            {
                return this._linecolor;
            }
            set
            {
                this._linecolor = value;
                this.Invalidate();
            }
        }
        private void DrawLine()
        {
            Graphics g = this.CreateGraphics();
            using (Pen p = new Pen(this._linecolor))
            {
                g.DrawLine(p, 0, this.Height - 1, this.Width, this.Height - 1);
            }
        }
        private const int WM_PAINT = 0xF;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_PAINT)
            {
                DrawLine();
            }
        }

        /// <summary>
        /// 当Enable = false 时字体颜色
        /// </summary>
        [Description("当Enable = false 时字体颜色")]
        public Color DisenableForeColor
        {
            get { return _label.ForeColor; }
            set { _label.ForeColor = value; }
        }


        /// <summary>
        ///当Enable = false 时背景颜色 
        /// </summary>
        [Description("当Enable = false 时背景颜色")]
        public Color DisenableBackColor
        {
            get { return _label.BackColor; }
            set { _label.BackColor = value; }
        }

        void _label_EnabledChanged(object sender, EventArgs e)
        {
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            _label.Text = this.Text;
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            _label.Font = this.Font;
        }

        protected override void OnDockChanged(EventArgs e)
        {
            base.OnDockChanged(e);
            _label.Dock = this.Dock;
        }

        protected override void OnRightToLeftChanged(EventArgs e)
        {
            base.OnRightToLeftChanged(e);
            _label.RightToLeft = this.RightToLeft;
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            _label.Left = this.Left;
            _label.Top = this.Top;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            _label.Width = this.Width;
            _label.Height = this.Height;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            _label.Parent = this.Parent;
        }


        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            if (this.SelectedItem == null)
                _label.Text = "";
            else
                _label.Text = this.SelectedItem.ToString();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);            
        }

        private class Mylabel : Label
        {
            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);
                if (m.Msg == 0xF)
                {
                    Graphics g = this.CreateGraphics();
                    using (Pen p = new Pen(Color.Red))
                    {
                        g.DrawLine(p, 0, this.Height - 1, this.Width, this.Height - 1);
                    }
                }
            }
        }



        #region IDataControl<string> 成员

        public object Value
        {
            get { return ValueFomatted(DataViewValue, this.DataFormater); }
        }
        private string dataFiled = string.Empty;
        public string DataFiled
        {
            get
            {
                return dataFiled;
            }
            set
            {
                dataFiled = value;
            }
        }

        public object DataViewValue
        {
            get
            {
                if (this.SelectedValue == null)
                    return string.Empty;
                return this.SelectedValue;
            }
            set
            {
                this.SelectedValue = value;
            }
        }
        private DataCellType dataFormater;
        public DataCellType DataFormater
        {
            get
            {
                return dataFormater;
            }
            set
            {
                dataFormater = value;
            }
        }
        public object ValueFomatted(object source, DataCellType dataFormater)
        {
            return source;
        }

        public bool ReadOnlyValue
        {
            get { return this.Enabled; }
            set { this.Enabled = value; }
        }

        private object _defaultValue;
        public object DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

        public void ClearData()
        {
            if (_defaultValue != null)
                this.SelectedItem = _defaultValue;
        }
        public string IsValid()
        {
            return string.Empty;
        }
        public string DataControlName
        {
            get { return this.Name; }
            set { this.Name = value; }
        }
        #endregion
        public bool FillEntity(ref object Entity)
        {
            if (Entity == null)
                return true;
            if (string.IsNullOrEmpty(this.DataFiled))
                return true;
            if (this.Value == null || string.IsNullOrEmpty(this.Value.ToString()))
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
        }
    }
}
