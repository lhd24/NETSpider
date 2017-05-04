using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Data;
using WinFormLib.Core;

namespace WinFormLib.Components
{
    [Serializable]
    [ToolboxBitmap(typeof(DataGridView))]
    public class DataGridViewer : DataGridViewStyler
    {
        ToolStripButton btnFirst;
        ToolStripButton btnPrev;
        ToolStripButton btnNext;
        ToolStripButton btnLast;

        public DataGridPager DataGridPager
        {
            get;
            set;
        }
        private DataGridViewCell _CurrentCell;
        public DataGridViewer()
            : base()
        {
            _CurrentCell = null;
        }
        private bool _autoColumnWidth = true;
        public bool AutoColumnWidth
        {
            get { return _autoColumnWidth; }
            set { _autoColumnWidth = value; }
        }
        protected override void OnRowStateChanged(int rowIndex, DataGridViewRowStateChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate()
                {
                    OnRowStateChanged(rowIndex, e);                   
                }));
                return;
            }
            base.OnRowStateChanged(rowIndex, e);
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                if (this.SelectedRows.Count > 0)
                {
                    if (btnFirst != null) btnFirst.Enabled = true;
                    if (btnPrev != null) btnPrev.Enabled = true;
                    if (btnNext != null) btnNext.Enabled = true;
                    if (btnLast != null) btnLast.Enabled = true;
                    if (this.SelectedRows[0].Index == 0)
                    {
                        if (btnFirst != null) btnFirst.Enabled = false;
                        if (btnPrev != null) btnPrev.Enabled = false;
                    }
                    if (this.SelectedRows[0].Index == this.Rows.Count - 1)
                    {
                        if (btnNext != null) btnNext.Enabled = false;
                        if (btnLast != null) btnLast.Enabled = false;
                    }
                }
                this.Refresh();
            }
        }
        protected override void OnDataBindingComplete(DataGridViewBindingCompleteEventArgs e)
        {
            base.OnDataBindingComplete(e);
            if (this.RowCount > 0)
            {
                if (btnNext != null) btnNext.Enabled = true;
                if (btnLast != null) btnLast.Enabled = true;
            }
            if (this.RowCount > 0 && this.SelectedRows.Count > 0 && this.SelectedRows[0].Index == this.Rows.Count - 1)
            {
                if (btnNext != null) btnNext.Enabled = false;
                if (btnLast != null) btnLast.Enabled = false;
            }
            if (_CurrentCell != null && _CurrentCell.OwningRow.DataGridView == this)//btnLast点击时会重新绑定数据源
            {
                CurrentCell = _CurrentCell;
                _CurrentCell = null;
            }
        }


        [Obsolete("DataGridViewer的DataSource不能访问,请使用GetDataSource|ReBind|RemoveBind", true)]
        public new object DataSource
        {
            get { return base.DataSource; }
            set { throw new UnauthorizedAccessException("DataGridViewer的DataSource不能访问"); }
        }
        public int CurrentPageIndex { get; set; }
        private void SetAutoWidth()
        {
            if (_autoColumnWidth)
            {
                if (this.ColumnCount > 0)
                {
                    this.Columns[this.ColumnCount - 1].Width = this.Parent.Width;
                }
            }
        }
        public bool ReBind<T>(List<T> EntityList)
        {
            SetAutoWidth();
            return ReBind<T>(EntityList, false);
        }
        public bool ReBind<T>(T Entity, int RowIndex)
        {
            bool ReturnValue = false;
            this.Invoke(new InvokeHandler(delegate()
            {
                if (RowIndex < 0) return;
                BindingGridList<T> tmpData = (BindingGridList<T>)base.DataSource;
                if (tmpData != null)
                {
                    tmpData[RowIndex] = Entity;
                    base.DataSource = new BindingGridList<T>();
                    base.DataSource = tmpData;
                    this.Rows[RowIndex].Selected = true;
                    ReturnValue = true;
                }

            }));
            return ReturnValue;
        }
        public bool ReBind(object Entity, Type ElementType, int RowIndex)
        {
            bool ReturnValue = false;
            this.Invoke(new InvokeHandler(delegate()
            {
                if (RowIndex < 0) return;
                object tmpData = base.DataSource;
                if (tmpData != null)
                {
                    ((IList)tmpData)[RowIndex] = Entity;
                    base.DataSource = BindingGridList.GetTypeList(ElementType);
                    base.DataSource = tmpData;
                    this.Rows[RowIndex].Selected = true;
                    ReturnValue = true;
                }
                SetAutoWidth();
            }));
            return ReturnValue;
        }
        public bool RemoveBind<T>(T Entity)
        {

            this.Invoke(new InvokeHandler(delegate()
            {
                BindingGridList<T> tmpData;
                if (base.DataSource != null)
                {
                    tmpData = (BindingGridList<T>)base.DataSource;
                    tmpData.Remove(Entity);
                    base.DataSource = new BindingGridList<T>();
                    base.DataSource = tmpData;
                    this.Refresh();
                }
                SetAutoWidth();
            }));

            return true;
        }
        public bool RemoveBind()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                    RemoveBind();
                }));
            }
            else
            {
                if (base.DataSource != null)
                {
                    base.DataSource = null;
                    this.Refresh();
                }
                SetAutoWidth();
            }
            return true;
        }
        public bool RemoveBind(Type EntityType, object Entity)
        {

            this.Invoke(new InvokeHandler(delegate()
            {

                if (base.DataSource != null)
                {
                    object tmpData = base.DataSource;
                    ((IList)tmpData).Remove(Entity);
                    base.DataSource = BindingGridList.GetTypeList(EntityType);
                    base.DataSource = tmpData;
                    this.Refresh();
                }
                SetAutoWidth();
            }));

            return true;
        }
        public bool ReBindSingle(object element, Type ElementType, bool Append)
        {
            IList list = (IList)BindingGridList.GetTypeList(ElementType);
            list.Add(element);
            return ReBind(list, ElementType, Append);
        }
        public bool ReBind(IList ElementTypeList, Type ElementType, bool Append)
        {
            if (ElementTypeList == null)
                return false;
            this.Invoke(new InvokeHandler(delegate()
            {

                object tmpData = BindingGridList.GetTypeList(ElementType);
                if (base.DataSource != null)
                {
                    if (Append)
                    {
                        tmpData = base.DataSource;
                        foreach (object item in ElementTypeList)
                        {
                            ((IList)tmpData).Add(item);
                        }
                    }
                    else
                    {
                        tmpData = BindingGridList.GetTypeList(ElementType, ElementTypeList);
                        CurrentPageIndex = 1;
                    }
                    base.DataSource = BindingGridList.GetTypeList(ElementType);
                    base.DataSource = tmpData;
                    this.Refresh();
                    if (Append && ElementTypeList.Count == 1)
                    {
                        if (this.RowCount > 0 && this.CurrentCell != null)
                            this.CurrentCell = this[CurrentCell.ColumnIndex, this.RowCount - 1];
                    }
                }
                else
                {
                    base.DataSource = BindingGridList.GetTypeList(ElementType, ElementTypeList);
                    this.Refresh();
                    CurrentPageIndex = 1;
                }
                SetAutoWidth();

            }));
            return true;
        }
        public bool ReBind<T>(IList<T> EntityList, bool Append)
        {
            if (EntityList == null)
                return false;
            this.Invoke(new InvokeHandler(delegate()
            {

                BindingGridList<T> tmpData = new BindingGridList<T>();
                if (base.DataSource != null)
                {
                    if (Append)
                    {
                        tmpData = (BindingGridList<T>)base.DataSource;
                        foreach (T item in EntityList)
                        {
                            tmpData.Add(item);
                        }
                    }
                    else
                    {
                        tmpData = new BindingGridList<T>(EntityList);
                        CurrentPageIndex = 1;
                    }
                    base.DataSource = new BindingGridList<T>();
                    base.DataSource = tmpData;
                    this.Refresh();
                    if (Append && EntityList.Count == 1)
                    {
                        if (this.RowCount > 0 && this.CurrentCell != null)
                            this.CurrentCell = this[CurrentCell.ColumnIndex, this.RowCount - 1];
                    }
                }
                else
                {
                    base.DataSource = new BindingGridList<T>(EntityList);
                    this.Refresh();
                    CurrentPageIndex = 1;
                }
                SetAutoWidth();

            }));
            return true;
        }

        public List<T> GetDataSource<T>()
        {
            if (base.DataSource == null)
                return new List<T>();
            BindingGridList<T> BindingData = (BindingGridList<T>)base.DataSource;
            return new List<T>(BindingData);
        }
        public object GetDataSource()
        {
            return base.DataSource;
        }
        public void ReBind(DataTable data)
        {
            if (base.InvokeRequired)
            {
                base.Invoke(new MethodInvoker(delegate()
                {
                    ReBind(data);
                }));
                return;
            }
            base.DataSource = data;
            SetAutoWidth();
        }
        public void ReBind(DataTable data, bool Append)
        {
            if (base.InvokeRequired)
            {
                base.Invoke(new MethodInvoker(delegate()
                {
                    ReBind(data, Append);
                }));
                return;
            }
            DataTable BaseDataSource = base.DataSource as DataTable;
            if (BaseDataSource == null)
            {
                base.DataSource = data;
            }
            else
            {
                if (Append)
                {
                    foreach (DataRow dr in data.Rows)
                    {
                        BaseDataSource.Rows.Add(dr.ItemArray);
                    }
                    base.DataSource = BaseDataSource;
                }
                else
                {
                    base.DataSource = data;
                }
            }
            SetAutoWidth();
        }
        public void SetToolStripButton(ToolStripButton _btnFirst, ToolStripButton _btnPrev, ToolStripButton _btnNext, ToolStripButton _btnLast)
        {

            this.btnFirst = _btnFirst;
            this.btnLast = _btnLast;
            this.btnNext = _btnNext;
            this.btnPrev = _btnPrev;
            if (_btnFirst != null)
            {
                _btnFirst.Click += new EventHandler(_btnFirst_Click);
            }
            if (_btnLast != null)
            {
                _btnLast.Click += new EventHandler(_btnLast_Click);
            }
            if (_btnNext != null)
            {
                _btnNext.Click += new EventHandler(_btnNext_Click);
            }
            if (_btnPrev != null)
            {
                _btnPrev.Click += new EventHandler(_btnPrev_Click);
            }
            if (this.RowCount > 0 && this.SelectedRows[0].Index == 0)
            {
                if (btnFirst != null) btnFirst.Enabled = false;
                if (btnPrev != null) btnPrev.Enabled = false;
            }
            if (this.RowCount > 0 && this.SelectedRows[0].Index == this.Rows.Count - 1)
            {
                if (btnNext != null) btnNext.Enabled = false;
                if (btnLast != null) btnLast.Enabled = false;
            }
            if (this.RowCount == 0)
            {
                if (btnFirst != null) btnFirst.Enabled = false;
                if (btnPrev != null) btnPrev.Enabled = false;
                if (btnNext != null) btnNext.Enabled = false;
                if (btnLast != null) btnLast.Enabled = false;
            }
        }

        void _btnPrev_Click(object sender, EventArgs e)
        {
            if (CurrentCell != null && CurrentCell.RowIndex - 1 != -1)
                this.CurrentCell = this[CurrentCell.ColumnIndex, CurrentCell.RowIndex - 1];
        }

        void _btnLast_Click(object sender, EventArgs e)
        {
            if (CurrentCell != null && this.RowCount > 0)
            {
                _CurrentCell = this[CurrentCell.ColumnIndex, this.RowCount - 1];
                CurrentCell = _CurrentCell;
            }
        }

        void _btnNext_Click(object sender, EventArgs e)
        {
            if (CurrentCell != null && this.RowCount > CurrentCell.RowIndex + 1)
            {
                _CurrentCell = this[CurrentCell.ColumnIndex, CurrentCell.RowIndex + 1];
                CurrentCell = _CurrentCell;
            }
        }

        void _btnFirst_Click(object sender, EventArgs e)
        {
            if (CurrentCell != null && this.RowCount > 0)
                this.CurrentCell = this[CurrentCell.ColumnIndex, 0];
        }
        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (this.btnFirst != null)
                this.btnFirst.Enabled = this.Enabled;
            if (this.btnPrev != null)
                this.btnPrev.Enabled = this.Enabled;
            if (this.btnNext != null)
                this.btnNext.Enabled = this.Enabled;
            if (this.btnLast != null)
                this.btnLast.Enabled = this.Enabled;

            if (Enabled)
            {
                if (this.RowCount > 0 && this.SelectedRows[0].Index == 0)
                {
                    if (btnFirst != null) btnFirst.Enabled = false;
                    if (btnPrev != null) btnPrev.Enabled = false;
                }
                if (this.RowCount > 0 && this.SelectedRows[0].Index == this.Rows.Count - 1)
                {
                    if (btnNext != null) btnNext.Enabled = false;
                    if (btnLast != null) btnLast.Enabled = false;
                }
                if (this.RowCount == 0)
                {
                    if (btnFirst != null) btnFirst.Enabled = false;
                    if (btnPrev != null) btnPrev.Enabled = false;
                    if (btnNext != null) btnNext.Enabled = false;
                    if (btnLast != null) btnLast.Enabled = false;
                }
            }
        }
    }
}
