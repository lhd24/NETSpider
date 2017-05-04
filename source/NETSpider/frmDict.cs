using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NETSpider.Entity;
using WinFormLib.Core;

namespace NETSpider
{
    public partial class frmDict : Form
    {
        private DictList dictList = new DictList();
        public frmDict()
        {
            InitializeComponent();
            string errMsg = string.Empty;
            dictList = XmlHelper.LoadFromXml<DictList>(Program.GetConfigPath(@"dict.xml"), ref errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                return;
            }
            foreach (var item in dictList)
            {
                TreeNode node = tvCategory.Nodes["tvDict"].Nodes.Add(item.CategoryName.Value);
                node.ImageIndex = 14;
                node.SelectedImageIndex = 15;
            }
            tvCategory.ExpandAll();
        }

        private void toolNewCategory_Click(object sender, EventArgs e)
        {
            string name = "newCategory0";
            TreeNode newNode = tvCategory.Nodes["tvDict"].Nodes.Add(name);
            newNode.Name = "category_" + name;
            newNode.ImageIndex = 14;
            newNode.SelectedImageIndex = 15;
            tvCategory.LabelEdit = true;
            tvCategory.ExpandAll();
            newNode.BeginEdit();
        }

        private void toolNewWord_Click(object sender, EventArgs e)
        {
            AddContent();
        }
        private string SelectNodeText { get; set; }
        private string SelectListViewText { get; set; }
        private string SelectNodeViewText { get; set; }
        private void AddContent()
        {
            SelectNodeText = string.Empty;
            TreeNode selectNode = tvCategory.SelectedNode;
            if (selectNode == null || selectNode.Name == "tvDict")
            {
                WinFormLib.Core.MessageBoxHelper.ShowError("请先选择字典分类!");
                return;
            }
            ListViewItem lviItem = lsvWord.Items.Add(new ListViewItem() { Text = "newWord0" });
            lsvWord.LabelEdit = true;
            lviItem.BeginEdit();
            lviItem = null;
            SelectNodeText = selectNode.Text;
        }

        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lsvWord_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                //表示没有进行修改
                this.lsvWord.Items.Remove(this.lsvWord.Items[e.Item]);
                this.lsvWord.LabelEdit = false;
                return;
            }

            if (e.Label.ToString().Trim() == "")
            {
                e.CancelEdit = true;
                this.lsvWord.Items.Remove(this.lsvWord.Items[e.Item]);
                this.lsvWord.LabelEdit = false;
                return;
            }
            Dict dictEntity = dictList.Where(q => q.CategoryName.Value == SelectNodeText).FirstOrDefault();
            if (dictEntity == null)
            {
                WinFormLib.Core.MessageBoxHelper.ShowError("请先选择字典分类!");
                return;
            }
            if (!string.IsNullOrEmpty(SelectListViewText))
            {
                DictItem dictItemEntity = dictEntity.DictItemList.Where(q => q.DictName.Value == SelectListViewText).FirstOrDefault();
                if (dictItemEntity != null)
                {
                    dictItemEntity.DictName = CDataItem.Instance(e.Label.Trim());
                }
            }
            else
            {
                dictEntity.DictItemList.Add(new DictItem() { DictName = CDataItem.Instance(e.Label.Trim()) });
            }
            SelectListViewText = string.Empty;
        }

        private void tvCategory_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                if (string.IsNullOrEmpty(SelectNodeViewText))
                {
                    //表示没有进行修改
                    e.Node.Remove();
                }
                else
                {
                    e.CancelEdit = true;
                }
                return;
            }
            if (e.Label.ToString().Trim() == "")
            {
                e.CancelEdit = true;
                return;
            }
            if (dictList.Where(q => q.CategoryName.Value == e.Label.Trim() && q.CategoryName.Value != SelectNodeViewText).FirstOrDefault() != null)
            {
                e.CancelEdit = true;
                WinFormLib.Core.MessageBoxHelper.ShowError("分类已存在!");
                return;
            }
            if (!string.IsNullOrEmpty(SelectNodeViewText))
            {
                Dict dicEntity = dictList.Where(q => q.CategoryName.Value == SelectNodeViewText).FirstOrDefault();
                if (dicEntity != null)
                {
                    dicEntity.CategoryName = e.Label.Trim();
                }
            }
            else
            {
                dictList.Add(new Dict()
                {
                    CategoryName = CDataItem.Instance(e.Label.Trim()),
                    DictItemList = new List<DictItem>()
                });
            }
            SelectNodeViewText = string.Empty;
        }

        private void frmDict_FormClosing(object sender, FormClosingEventArgs e)
        {

            string errMsg = string.Empty;
            XmlHelper.Save2File(dictList, Program.GetConfigPath(@"dict.xml"), ref errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                e.Cancel = true;
                MessageBoxHelper.ShowError(errMsg);
                return;
            }

        }

        private void tvCategory_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lsvWord.Items.Clear();
            if (e.Node.Name == "tvDict")
            {
                return;
            }

            Dict dictEntity = dictList.Where(q => q.CategoryName.Value == e.Node.Text).FirstOrDefault();
            if (dictEntity == null)
            {
                return;
            }
            SelectNodeText = e.Node.Text;
            foreach (var item in dictEntity.DictItemList)
            {
                lsvWord.Items.Add(new ListViewItem() { Text = item.DictName.Value });
            }
        }

        private void lsvWord_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditContent();
        }

        private void EditContent()
        {
            if (lsvWord.SelectedItems.Count > 0)
            {
                ListViewItem lviItem = lsvWord.SelectedItems[0];
                SelectListViewText = lviItem.Text;
                lsvWord.LabelEdit = true;
                lviItem.BeginEdit();
                lviItem = null;
            }
        }

        private void tvCategory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (tvCategory.SelectedNode != null && tvCategory.SelectedNode.Name != "tvDict")
            {
                tvCategory.LabelEdit = true;
                tvCategory.SelectedNode.BeginEdit();
                SelectNodeViewText = tvCategory.SelectedNode.Text;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddContent();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            EditContent();
        }
    }
}
