using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinFormLib.Components.Cell;
using System.ComponentModel;
using WinFormLib.Controls;
using WinFormLib.Core;
using System.Reflection;
using System.Collections;

namespace WinFormLib.Components
{
    [Serializable]
    public class DataGridViewEditor : DataGridViewStyler
    {
        public DataGridViewEditor()
            : base()
        {
            // this.RowCount = 50;
            this.ScrollBars = ScrollBars.Both;
            this.EnterIsTab = true;

            RightMenu.Items.Add("导入数据");

        }
        protected override void ctCopy_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            RightMenu.Hide();
            if (e.ClickedItem.Text == "导入数据")
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "*.xls|*.xls";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string FileName = ofd.FileName;
                    MessageBoxHelper.Show(FileName);
                }
            }
            else
                base.ctCopy_ItemClicked(sender, e);

        }
        private bool _EnabledEdit = true;
        [Browsable(false)]
        public bool EnabledEdit
        {
            get { return _EnabledEdit; }
            set { _EnabledEdit = value; }
        }
        DataGridViewForzenColumn ForzenColumn;
        private IContainer components;

        private Type DataSouceType;
        private bool _autoColumnWidth = true;
        public bool AutoColumnWidth
        {
            get { return _autoColumnWidth; }
            set { _autoColumnWidth = value; }
        }
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
        protected override void OnRowStateChanged(int rowIndex, DataGridViewRowStateChangedEventArgs e)
        {
            base.OnRowStateChanged(rowIndex, e);
        }
        [System.ComponentModel.DefaultValue(false)]
        public new bool MultiSelect
        {
            get { return base.MultiSelect; }
            set { base.MultiSelect = value; }
        }
        protected override void OnScroll(ScrollEventArgs e)
        {
            base.OnScroll(e);

            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (this.FirstDisplayedScrollingRowIndex == this.Rows.Count - (this.Height / 20))
                {
                    if (ForzenColumn != null && ForzenColumn is DataGridViewForzenColumn)
                    {
                        if (((DataGridViewForzenColumn)ForzenColumn).IsAutoValue)
                        {
                            return;
                        }
                    }
                    this.RowCount++;
                    if (OnRowDefaultValue != null)
                    {
                        OnRowDefaultValue(this, new DefaultValueEventArgs(this.RowCount - 1));
                    }
                }
            }
        }
        int ForzenColumnLength = 0;

        protected override void OnCellBeginEdit(DataGridViewCellCancelEventArgs e)
        {
            if (!_EnabledEdit)
            {
                e.Cancel = true;
                return;
            }
            if (e.RowIndex > 0)
            {
                if (ForzenColumn != null)
                {
                    if (string.IsNullOrEmpty(TryParse.ToString(this[ForzenColumn.Index, e.RowIndex - 1].Value)))
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
            base.OnCellBeginEdit(e);
        }
        private List<int> ValidRowIndex = new List<int>() { };
        protected override void OnCellEndEdit(DataGridViewCellEventArgs e)
        {
            base.OnCellEndEdit(e);
            if (ForzenColumn == null) return;
            if (this[e.ColumnIndex, e.RowIndex].Value == null)
                return;
            if (string.IsNullOrEmpty(TryParse.ToString(this[e.ColumnIndex, e.RowIndex].Value)))
                return;
            if (string.IsNullOrEmpty(TryParse.ToString(this[ForzenColumn.Index, e.RowIndex].Value)))
            {
                int NextValue = 1;
                if (e.RowIndex > 0)
                {
                    NextValue = TryParse.StrToInt(this[ForzenColumn.Index, e.RowIndex - 1].Value) + 1;
                }
                string Value = ("000000000" + NextValue);
                this[ForzenColumn.Index, e.RowIndex].Value = Value.Substring(Value.Length - ForzenColumnLength);
            }
            if (ValidRowIndex.Contains(e.RowIndex))
            {
                ValidRowIndex.Remove(e.RowIndex);
            }
            if (_IsNoNullEmptyColumnIndex != null && _IsNoNullEmptyColumnIndex.Count > 0 && ForzenColumn != null)
            {
                foreach (int columnIndex in _IsNoNullEmptyColumnIndex)
                {
                    object value = this[columnIndex, e.RowIndex].Value;
                    if (value != null && !ValidRowIndex.Contains(e.RowIndex))
                    {
                        ValidRowIndex.Add(e.RowIndex);
                    }
                }
            }

        }
        public event RapidHandler<object, DefaultValueEventArgs> OnRowDefaultValue;
        /// <summary>
        /// 始终空行显示在最后
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSortCompare(DataGridViewSortCompareEventArgs e)
        {
            if (ForzenColumn == null) return;
            //base.OnSortCompare(e);
            if (string.IsNullOrEmpty(TryParse.ToString(this[ForzenColumn.Index, e.RowIndex1].Value)))
            {
                if (e.Column.HeaderCell.SortGlyphDirection == SortOrder.Ascending)
                    e.SortResult = 1;
                else
                    e.SortResult = -1;

            }
            else if (string.IsNullOrEmpty(TryParse.ToString(this[ForzenColumn.Index, e.RowIndex2].Value)))
            {
                if (e.Column.HeaderCell.SortGlyphDirection == SortOrder.Ascending)
                    e.SortResult = -1;
                else
                    e.SortResult = 1;
            }

            else
            {
                e.SortResult = System.String.Compare(TryParse.ToString(e.CellValue1), TryParse.ToString(e.CellValue2));
            }
            e.Handled = true;
        }
        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                if (this.SelectedRows.Count > 0 && this.SelectedRows[0].Index == this.RowCount - 1)
                {
                    this.RowCount++;
                    if (OnRowDefaultValue != null)
                    {
                        OnRowDefaultValue(this, new DefaultValueEventArgs(this.RowCount - 1));
                    }
                }
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (ForzenColumn != null && this.SelectedRows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(TryParse.ToString(this[ForzenColumn.Index, SelectedRows[0].Index].Value)))
                    {
                        if (MessageBoxHelper.ShowQuestion("确定删除当前行吗?") == DialogResult.OK)
                        {
                            this.Rows.RemoveAt(this.SelectedRows[0].Index);
                            this.RowCount++;
                        }
                    }
                }

            }
            return base.ProcessDataGridViewKey(e);
        }

        private List<int> _IsNoNullEmptyColumnIndex = new List<int>();

        public List<int> IsNoNullEmptyColumnIndex
        {
            get { return _IsNoNullEmptyColumnIndex; }
            set { _IsNoNullEmptyColumnIndex = value; }
        }
        string RowMessage = string.Empty;
        protected override void OnRowValidating(DataGridViewCellCancelEventArgs e)
        {

            if (!string.IsNullOrEmpty(RowMessage))
            {
                if (this.Focused && this.EnabledEdit)
                {
                    MessageBoxHelper.ShowError("[" + RowMessage.Substring(1) + "]不能为空");
                    e.Cancel = true;
                }
            }
            base.OnRowValidating(e);
        }
        protected override void OnRowLeave(DataGridViewCellEventArgs e)
        {
            base.OnRowLeave(e);
            ValidRowValue(e);
        }

        private void ValidRowValue(DataGridViewCellEventArgs e)
        {
            RowMessage = string.Empty;
            if (_IsNoNullEmptyColumnIndex != null && _IsNoNullEmptyColumnIndex.Count > 0 && ForzenColumn != null)
            {
                if (!ValidRowIndex.Contains(e.RowIndex))
                    return;
                if (!string.IsNullOrEmpty(TryParse.ToString(this[ForzenColumn.Index, e.RowIndex].Value)) ||
                    !string.IsNullOrEmpty(TryParse.ToString(this[e.ColumnIndex, e.RowIndex].EditedFormattedValue)))
                {
                    foreach (int item in _IsNoNullEmptyColumnIndex)
                    {
                        if (string.IsNullOrEmpty(TryParse.ToString(this[item, e.RowIndex].Value)) &&
                            string.IsNullOrEmpty(TryParse.ToString(this[item, e.RowIndex].EditedFormattedValue)))
                        {
                            RowMessage += "," + this.Columns[item].HeaderText;
                        }
                    }
                }
            }
        }

        #region InitDataSource 成员
        public void InitDataSource(Type p_Type)
        {
            this.InitDataSource(50, p_Type);
        }
        public void InitDataSource(int count, Type p_Type)
        {
            if (count <= 0) return;
            DataSouceType = p_Type;
            this.EnabledEdit = false;
            // 在子线程中 解决滚动条
            //this.EnabledEdit = false;
            this.RowCount = 0;
            this.Invoke(new InvokeHandler(delegate()
            {

                while (true)
                {
                    this.RowCount++;
                    if (OnRowDefaultValue != null)
                    {
                        OnRowDefaultValue(this, new DefaultValueEventArgs(this.RowCount - 1));
                    }
                    if (this.RowCount == count)
                        break;
                }
            }));
            SetForzenColumn();
            SetAutoWidth();
        }

        private void SetForzenColumn()
        {
            if (ForzenColumn == null)
            {
                foreach (DataGridViewColumn dgvc in this.Columns)
                {
                    if (dgvc is DataGridViewForzenColumn)
                    {
                        ForzenColumn = (DataGridViewForzenColumn)dgvc;
                        ForzenColumnLength = ForzenColumn.ForzenColumnLength;
                        break;
                    }
                }
            }
            if (ForzenColumn == null)
            {
                MessageBoxHelper.Show("没有设置编号列,不能进行操作");
                this.Enabled = false;
            }
        }
        public void InitDataSource<T>(IEnumerable<T> DataSource)
        {
            InitDataSource<T>(50, DataSource);
        }
        public void InitDataSource<T>(int count, IEnumerable<T> DataSource)
        {

            DataSouceType = typeof(T);
            int dcount = DataSource.Count();
            int index = 0;
            this.RowCount = 0;
            this.Invoke(new InvokeHandler(delegate()
            {
                SetForzenColumn();
                while (true)
                {
                    this.RowCount++;
                    if (OnRowDefaultValue != null)
                    {
                        OnRowDefaultValue(this, new DefaultValueEventArgs(this.RowCount - 1));
                    }
                    if (dcount > index)
                    {
                        foreach (T entity in DataSource)
                        {
                            if (entity != null)
                            {
                                if (!ValidRowIndex.Contains(index))
                                {
                                    ValidRowIndex.Add(index);
                                }
                                foreach (DataGridViewColumn column in this.Columns)
                                {
                                    string DataPropertyName = column.DataPropertyName;
                                    if (!string.IsNullOrEmpty(DataPropertyName))
                                    {
                                        PropertyInfo propertyInfo = entity.GetType().GetProperty(DataPropertyName);
                                        if (propertyInfo != null)
                                        {
                                            this[column.Index, index].Value = propertyInfo.GetValue(entity, null);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    index++;

                    if (this.RowCount >= count)
                    {
                        if (count > dcount)
                            break;
                        else if (this.RowCount == dcount)
                            break;
                    }
                }
            }));
        }
        public object GetDataSource()
        {
            DataGridViewCellEventArgs evt = new DataGridViewCellEventArgs(this.CurrentCell.ColumnIndex, this.CurrentCell.RowIndex);
            ValidRowValue(evt);
            if (!string.IsNullOrEmpty(RowMessage))
            {
                if (this.Focused && this.EnabledEdit)
                {
                    MessageBoxHelper.ShowError("[" + RowMessage.Substring(1) + "]不能为空");
                    return null;
                }
            } if (this.DataSouceType != null)
            {
                IList resultList = GetList(this.DataSouceType) as IList;
                foreach (var item in this.ValidRowIndex)
                {
                    object entity = System.Activator.CreateInstance(this.DataSouceType);
                    foreach (DataGridViewColumn column in this.Columns)
                    {
                        string DataPropertyName = column.DataPropertyName;
                        if (!string.IsNullOrEmpty(DataPropertyName))
                        {
                            PropertyInfo propertyInfo = entity.GetType().GetProperty(DataPropertyName);
                            if (propertyInfo != null && propertyInfo.CanRead)
                            {
                                object value = this[column.Index, item].FormattedValue;
                                if (propertyInfo.PropertyType == typeof(WinFormLib.Core.CDATA))
                                {
                                    WinFormLib.Core.CDATA data = new WinFormLib.Core.CDATA(value.ToString());
                                    propertyInfo.SetValue(entity, data, null);
                                }
                                else
                                {
                                    value = Convert.ChangeType(value, propertyInfo.PropertyType);
                                    propertyInfo.SetValue(entity, value, null);
                                }
                            }
                        }
                    }
                    resultList.Add(entity);
                }
                return resultList;
            }
            return this.DataSource;
        }
        private object GetList(Type p_Type)
        {
            Assembly _Assembly = Assembly.Load("mscorlib");
            Type _TypeList = _Assembly.GetType("System.Collections.Generic.List`1[[" + p_Type.FullName + "," + p_Type.Assembly.FullName + "]]");
            object _List = System.Activator.CreateInstance(_TypeList);
            return _List;
        }

        #endregion
        public string DataControlName
        {
            get { return this.Name; }
            set { this.Name = value; }
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
        #region IDataControlFomatter<object> 成员

        public object ValueFomatted(object source, DataCellType dataFormater)
        {
            if (DataSouceType != null && ForzenColumn != null)
            {
                List<string> s = new List<string>();
                IList DataSourceValue = GetList(DataSouceType) as IList;
                foreach (DataGridViewRow row in this.Rows)
                {
                    if (this[ForzenColumn.Index, row.Index].Value != null)
                    {
                        object Entity = System.Activator.CreateInstance(DataSouceType);
                        foreach (DataGridViewColumn column in this.Columns)
                        {
                            string DataPropertyName = column.DataPropertyName;
                            if (!string.IsNullOrEmpty(DataPropertyName))
                            {
                                PropertyInfo propertyInfo = Entity.GetType().GetProperty(DataPropertyName);
                                if (propertyInfo != null)
                                {
                                    if (propertyInfo.PropertyType == typeof(int))
                                    {
                                        propertyInfo.SetValue(Entity, TryParse.StrToInt(this[column.Index, row.Index].Value), null);
                                    }
                                    else if (propertyInfo.PropertyType == typeof(decimal))
                                    {
                                        propertyInfo.SetValue(Entity, TryParse.StrToDecimal(this[column.Index, row.Index].Value), null);
                                    }
                                    else
                                        propertyInfo.SetValue(Entity, this[column.Index, row.Index].Value, null);
                                }
                            }
                        }
                        DataSourceValue.Add(Entity);
                    }
                    else
                        break;

                }
                return DataSourceValue;
            }
            return null;
        }

        public string IsValid()
        {
            if (this.CurrentCell != null)
            {
                ValidRowValue(new DataGridViewCellEventArgs(this.CurrentCell.ColumnIndex, CurrentCell.RowIndex));
            }
            if (!string.IsNullOrEmpty(RowMessage))
            {
                return "[" + RowMessage.Substring(1) + "]不能为空";
            }
            this.EndEdit();
            return string.Empty;
        }


        public void ClearData()
        {

        }

        #endregion
        public void UnFillEntity(object Entity)
        {

        }
        public bool FillEntity(ref object Entity)
        {
            return true;
            //if (Entity == null)
            //    return false;
            //if (string.IsNullOrEmpty(this.DataFiled))
            //    return false;
            //if (string.IsNullOrEmpty(this.Value))
            //    return false;
            //string[] dataFileds = this.DataFiled.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //string[] value = this.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //int index = -1;
            //foreach (string fileds in dataFileds)
            //{
            //    index++;
            //    object filedValue = value[index];
            //    PropertyInfo propertyInfo = Entity.GetType().GetProperty(fileds);
            //    if (propertyInfo == null)
            //    {
            //        continue;
            //    }
            //    if (BaseEntityHelper.GetValue(ref filedValue, propertyInfo, this.DataControlName))
            //    {
            //        propertyInfo.SetValue(Entity, filedValue, null);
            //    }
            //}
        }
    }
}
