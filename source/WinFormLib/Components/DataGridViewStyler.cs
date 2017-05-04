using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinFormLib.Core;
using WinFormLib.Components.Cell;
using System.Drawing;
using System.ComponentModel;

namespace WinFormLib.Components
{
    [Serializable]
    public class DataGridViewStyler : DataGridView, ISerializeStyle
    {
        protected ContextMenuStrip RightMenu = new ContextMenuStrip();
        private DataGridViewHeaderCotrol pnlMoveed = new DataGridViewHeaderCotrol();

        public DataGridViewStyler()
        {
            #region 样式及移动
            EnableHeadersVisualStyles = false;
            ApplyStyle();
            ExtDoubleBuffered(true);
            if (!this.AllowUserToOrderColumns)
            {
                pnlMoveed.Width = pnlMoveed.Height = 0;
                pnlMoveed.Visible = false;
                this.Controls.Add(pnlMoveed);
            }
            #endregion
            #region 复制粘贴
            RightMenu.Items.Add("复制");
            RightMenu.Items.Add("粘贴");
            RightMenu.ItemClicked += new ToolStripItemClickedEventHandler(ctCopy_ItemClicked);
            #endregion
        }
    
        public event RapidHandler<object, EventArgs> OnDataGridViewRowDeleted;

        #region 列移动
        bool bStartMove = false;
        int ToColumnIndex = -1;
        int MoveColumnIndex = -1;
        private bool _MenuStripShowFlag = true;
        [Browsable(true), DefaultValue(true)]
        public bool MenuStripShowFlag
        {
            get { return _MenuStripShowFlag; }
            set { _MenuStripShowFlag = value; }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            pMouseDwn = new Point(e.X, e.Y);
        }
        protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
        {
            base.OnCellMouseDown(e);
            if (!this.AllowUserToOrderColumns)
            {
                #region OnCellMouseDown
                if (e.RowIndex == -1)
                {
                    //改变列大小
                    if (Cursor.Current == System.Windows.Forms.Cursors.SizeWE)
                    {
                        return;
                    }
                    if (e.Location.X > this.Columns[e.ColumnIndex].Width - 20)
                    {

                        return;
                    }
                    if (e.ColumnIndex == 0)
                    {
                        return;
                    }
                    if (this.Columns[e.ColumnIndex] is DataGridViewForzenColumn ||
                        this.Columns[e.ColumnIndex] is DataGridViewAutoForzenColumn)
                        return;
                    if (this.Columns[e.ColumnIndex].Frozen)
                        return;
                    pnlMoveed.HeaderText = this.Columns[e.ColumnIndex].HeaderText;
                    pnlMoveed.Visible = true;
                    pnlMoveed.Controls.Clear();
                    pnlMoveed.Width = this.Columns[e.ColumnIndex].Width;
                    pnlMoveed.Height = 18;
                    MoveColumnIndex = this.Columns[e.ColumnIndex].DisplayIndex;
                    pnlMoveed.Location = this.GetCellDisplayRectangle(e.ColumnIndex, -1, true).Location;
                    bStartMove = true;
                }
                else
                {
                    bStartMove = false;
                    if (this._MenuStripShowFlag)
                    {
                        if (e.Button == MouseButtons.Right && pMouseDwn != null && !pMouseDwn.IsEmpty && Cursor.Current == System.Windows.Forms.Cursors.Default)
                        {
                            RightMenu.Show(this, pMouseDwn);
                            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
                            {
                                RightMenu.Items[1].Enabled = true;
                            }
                            else
                            {
                                RightMenu.Items[1].Enabled = false;
                            }
                        }
                    }
                }
                #endregion
            }

        }
        protected override void OnCellMouseUp(DataGridViewCellMouseEventArgs e)
        {
            base.OnCellMouseUp(e);
            if (!this.AllowUserToOrderColumns)
            {
                #region OnCellMouseUp
                if (bStartMove && ToColumnIndex >= 0 && MoveColumnIndex >= 0)
                {
                    if (this.Columns[ToColumnIndex].Frozen ||
                        this.Columns[ToColumnIndex] is DataGridViewForzenColumn ||
                        this.Columns[ToColumnIndex] is DataGridViewAutoForzenColumn)
                    {

                    }
                    else
                    {
                        foreach (DataGridViewColumn item in this.Columns)
                        {
                            if (item.DisplayIndex == MoveColumnIndex)
                            {
                                item.DisplayIndex = ToColumnIndex;
                                break;
                            }
                        }
                    }

                }
                pnlMoveed.Controls.Clear();
                pnlMoveed.Visible = false;
                this.Refresh();
                bStartMove = false;
                MoveColumnIndex = -1;
                ToColumnIndex = -1;
                #endregion
            }
        }
        protected override void OnCellMouseMove(DataGridViewCellMouseEventArgs e)
        {
            base.OnCellMouseMove(e);
            if (!this.AllowUserToOrderColumns)
            {
                if (bStartMove)
                {
                    if (!pnlMoveed.Visible)
                    {
                        pnlMoveed.Visible = true;
                    }
                    pnlMoveed.Location = this.GetCellDisplayRectangle(e.ColumnIndex, -1, true).Location;
                    pnlMoveed.Width = this.Columns[e.ColumnIndex].Width;
                    ToColumnIndex = this.Columns[e.ColumnIndex].DisplayIndex;
                }
            }
        }
        #endregion
        #region 复制粘贴

        Point pMouseDwn;

        protected virtual void ctCopy_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            RightMenu.Hide();
            if (e.ClickedItem.Text == "复制")
            {
                if (this.CurrentCell != null)
                {
                    Clipboard.SetDataObject(TryParse.ToString(this.CurrentCell.Value).Trim());
                }

            }
            else if (e.ClickedItem.Text == "粘贴")
            {
                if (this.CurrentCell != null && !this.CurrentCell.ReadOnly && !this.ReadOnly)
                {
                    IDataObject iData = Clipboard.GetDataObject();

                    if (iData.GetDataPresent(DataFormats.Text))
                    {
                        this.CurrentCell.Value = iData.GetData(DataFormats.Text);
                    }

                }
            }
        }
        #endregion
        #region 样式及属性
        protected delegate void InvokeHandler();
        private void ApplyStyle()
        {
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            RowsDefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            RowHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Control;
            RowHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.Control;
            AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#CEF9E7");
            AlternatingRowsDefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#B7DCF2");
            AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.Black;
            RowsDefaultCellStyle.BackColor = Color.White;
            RowsDefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#B7DCF2");
            RowsDefaultCellStyle.SelectionForeColor = Color.Black;
            this.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#B7DCF2");
            this.DefaultCellStyle.SelectionForeColor = ColorTranslator.FromHtml("#FFF");
            this.RowsDefaultCellStyle.Padding = System.Windows.Forms.Padding.Empty;
            this.RowTemplate.Height = 20;
            this.Margin = System.Windows.Forms.Padding.Empty;
            this.RowHeadersVisible = false;
            base.AutoGenerateColumns = false;
            this.AllowUserToOrderColumns = false;
            this.BackgroundColor = System.Drawing.Color.DarkGray;
            this.MultiSelect = false;
            this.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.ColumnHeadersHeight = 23;
            this.ClearSelection();
        }
        [System.ComponentModel.DefaultValue(23)]
        public new int ColumnHeadersHeight
        {
            get { return base.ColumnHeadersHeight; }
            set { base.ColumnHeadersHeight = value; }
        }
        [System.ComponentModel.DefaultValue(DataGridViewColumnHeadersHeightSizeMode.EnableResizing)]
        public new DataGridViewColumnHeadersHeightSizeMode ColumnHeadersHeightSizeMode
        {
            get { return base.ColumnHeadersHeightSizeMode; }
            set { base.ColumnHeadersHeightSizeMode = value; }
        }
        private bool _MultiSelect = false;
        [System.ComponentModel.DefaultValue(false)]
        public new bool MultiSelect
        {
            get { return _MultiSelect; }
            set { base.MultiSelect = _MultiSelect = value; }
        }
        [System.ComponentModel.DefaultValue(false)]
        protected new bool AutoGenerateColumns
        {
            get { return base.AutoGenerateColumns; }
            set { base.AutoGenerateColumns = value; }
        }
        public void ExtDoubleBuffered(bool setting)
        {
            Type dgvType = this.GetType();
            System.Reflection.PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            pi.SetValue(this, setting, null);
        }


        private bool _AutoGenerateForzenCell = true;
        [System.ComponentModel.DefaultValue(true)]
        public bool AutoGenerateForzenCell
        {
            get { return _AutoGenerateForzenCell; }
            set { _AutoGenerateForzenCell = value; }
        }
        private bool lockKeyUp = false;

        private bool _EnterIsTab = false;

        public bool EnterIsTab
        {
            get { return _EnterIsTab; }
            set { _EnterIsTab = value; }
        }
        #endregion
        #region 重写相关事件处理

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (EnterIsTab && keyData == Keys.Enter)
            {
                return this.ProcessTabKey(keyData);
            }
            return base.ProcessDialogKey(keyData);
        }
        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            if (EnterIsTab && e.KeyCode == Keys.Enter)
            {
                return this.ProcessTabKey(e.KeyCode);
            }
            return base.ProcessDataGridViewKey(e);
        }

        protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception.Message == "类型“System.DBNull”的对象无法转换为类型“System.Decimal”。")
            {
                this[e.ColumnIndex, e.RowIndex].Value = 0m;
                return;
            }
            else if (e.Exception.Message == "类型“System.DBNull”的对象无法转换为类型“System.Int32”。")
            {
                this[e.ColumnIndex, e.RowIndex].Value = 0;
                return;
            }
            else if (e.Exception.Message == "类型“System.DBNull”的对象无法转换为类型“System.Float”。")
            {
                this[e.ColumnIndex, e.RowIndex].Value = 0f;
                return;
            }
            base.OnDataError(displayErrorDialogIfNoHandler, e);
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (this.CurrentCell != null && this.CurrentCell.Value != null && !string.IsNullOrEmpty(this.CurrentCell.Value.ToString().Trim()))
                {
                    Clipboard.SetText(this.CurrentCell.Value.ToString().Trim());
                }
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (OnDataGridViewRowDeleted != null)
                {
                    OnDataGridViewRowDeleted(this, EventArgs.Empty);
                }
            }
            if (lockKeyUp)
            {
                return;
            }
            base.OnKeyUp(e);
        }
        #endregion
        #region 序列化视图
        public string ToSerializeStyle()
        {
            return string.Empty;
        }
        public void FromSerializeStyle(string Value)
        {

        }
        #endregion
    }
}
