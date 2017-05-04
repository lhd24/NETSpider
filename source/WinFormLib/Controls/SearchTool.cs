using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinFormLib.Core;

namespace WinFormLib.Controls
{
    [DefaultEvent("OnSearchToolSelected")]
    public partial class SearchTool : UserControl
    {
        public event RapidHandler<string, string> OnSearchToolSelected;
        public SearchTool()
        {
            InitializeComponent();
            dgvList.AutoColumnWidth = false;
            pnlCenter.Visible = false;
            btnSelect.ContextMenuStrip = cmsMenu;
            cbTF005.DataSource = BaseEntityHelper.OperatorKeyValue;
            cbTF005.DisplayMember = "DisplayMember";
            cbTF005.ValueMember = "ValueMember";
            cbNum.DataSource = BaseEntityHelper.TopCountKeyValue;
        }

        List<SearchToolTF> DataKeyValueList = new List<SearchToolTF>();
        KeyValueCollection HighList = new KeyValueCollection();
        public void InitSearchTool(List<SearchToolTF> DataKeyValueList)
        {
            this.DataKeyValueList = DataKeyValueList;
            KeyValueCollection NormalList = new KeyValueCollection();
            foreach (SearchToolTF item in DataKeyValueList)
            {
                if (item.TF004 == EnumDataSearchType.Inherited || item.TF004 == EnumDataSearchType.Normal)
                {
                    NormalList.Add(new KeyValue()
                    {
                        DisplayMember = item.TF002.Trim() + " " + item.TF003,
                        ValueMember = item.TF001.Trim(),
                        OtherMember = ""
                    });
                }
                if (item.TF004 == EnumDataSearchType.Inherited || item.TF004 == EnumDataSearchType.High)
                {
                    HighList.Add(new KeyValue()
                    {
                        DisplayMember = item.TF002.Trim() + " " + item.TF003,
                        ValueMember = item.TF001.Trim(),
                        OtherMember = ""
                    });
                }
            }

            cbTF001.DataSource = NormalList;
            cbTF001.DisplayMember = "DisplayMember";
            cbTF001.ValueMember = "ValueMember";
            CTF005.DataSource = BaseEntityHelper.OperatorKeyValue;
            CTF008.DataSource = BaseEntityHelper.AndOrKeyValue;
            CTF000.IsAutoValue = true;
            CTF001.DataSource = HighList;

            dgvList.OnRowDefaultValue += new RapidHandler<object, DefaultValueEventArgs>(dgvList_OnRowDefaultValue);
            dgvList.InitDataSource(2, typeof(SearchToolTF));
        }

        void dgvList_OnRowDefaultValue(object sender, DefaultValueEventArgs e)
        {
            if (HighList.Count > e.RowIndex)
                dgvList[CTF001.Index, e.RowIndex].Value = HighList[e.RowIndex].ValueMember;
            else
                dgvList[CTF001.Index, e.RowIndex].Value = HighList[0].ValueMember;
            dgvList[CTF005.Index, e.RowIndex].Value = "= {0}";
            dgvList[CTF008.Index, e.RowIndex].Value = "AND";
        }
        private void tsMnuItemNormal_Click(object sender, EventArgs e)
        {
            if (pnlCenter.Visible)
            {
                pnlCenter.Visible = false;
                if (this.Parent is SplitterPanel)
                {
                    SplitContainer container = this.Parent.Parent as SplitContainer;
                    container.SplitterDistance = 30;
                    container.Refresh();
                }
                else
                {
                    this.Parent.Height = 30;
                }
                tsMnuItemNormal.Checked = true;
                tsMnuItemHigh.Checked = false;
            }
        }

        private void tsMnuItemHigh_Click(object sender, EventArgs e)
        {
            if (!pnlCenter.Visible)
            {
                pnlCenter.Visible = true;
                if (this.Parent is SplitterPanel)
                {
                    SplitContainer container = this.Parent.Parent as SplitContainer;
                    container.SplitterDistance = 112;
                    container.Refresh();
                }
                else
                {
                    this.Parent.Height = 112;
                }
                tsMnuItemNormal.Checked = false;
                tsMnuItemHigh.Checked = true;
            }
        }
        private void btnSelect_ButtonClick(object sender, EventArgs e)
        {
            StringBuilder strSql = new StringBuilder();
            if (tsMnuItemHigh.Checked)
            {
                List<SearchToolTF> SearchTFList = dgvList.DataSource as List<SearchToolTF>;
                SearchTFList = SearchTFList.FindAll(q => !string.IsNullOrEmpty(q.TF006));
                int index = 0;
                bool Brackets = false;
                foreach (SearchToolTF item in SearchTFList)
                {
                    SearchToolTF entity = DataKeyValueList.Find(q => q.TF001 == item.TF001);

                    if (!Brackets && item.TF008.Trim() == "OR" && SearchTFList.Count > index + 1)
                    {
                        Brackets = true;
                        strSql.Append("(");
                    }
                    string TF006 = item.TF006.Trim();
                    if (entity.TF007 == EnumDataType.String)//string类型
                    {
                        if (item.TF005.IndexOf("'") == -1)
                            TF006 = "'" + TF006 + "'";
                    }
                    strSql.Append(" " + entity.TF002.Trim() + " " + string.Format(item.TF005, TF006));

                    if (Brackets && item.TF008.Trim() != "OR")
                    {
                        strSql.Append(")");
                        Brackets = false;
                    }
                    if (SearchTFList.Count > index + 1)
                        strSql.Append(" " + item.TF008.Trim());
                    index++;
                }
            }
            else
            {
                if (cbTF001.SelectedValue == null) return;
                SearchToolTF entity = DataKeyValueList.Find(q => q.TF001 == cbTF001.SelectedValue.ToString().Trim());
                if (entity == null) return;

                string TF006 = txtTF006.Text.Trim();
                if (!string.IsNullOrEmpty(TF006))
                {
                    string TF005 = cbTF005.SelectedValue.ToString();
                    if (entity.TF007 == EnumDataType.String)//string类型
                    {
                        if (TF005.IndexOf("'") == -1)
                            TF006 = "'" + TF006 + "'";
                    }
                    strSql.Append(" " + entity.TF002.Trim() + " " + string.Format(TF005, TF006));
                }
            }
            if (OnSearchToolSelected != null)
            {
                OnSearchToolSelected(strSql.ToString(), cbNum.Value);
            }
        }
        public string TopCount
        {
            get { return cbNum.Value; }
        }
    }
}
