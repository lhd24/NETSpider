using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NETSpider.Gather;

namespace NETSpider
{
    public partial class frmAddNavRules : Form
    {
        public frmAddNavRules(string url)
        {
            InitializeComponent();
            lbMainUrl.DataViewValue = url;
        }

        private void frmAddNavRules_Load(object sender, EventArgs e)
        {
            EnumHelper.InitItemList(typeof(EnumGloabParas.EnumCmdType), this.cbCmdType);
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lbInfo.Value))
            {
                WinFormLib.Core.MessageBoxHelper.ShowError("信息不能为空！");
                return;
            }
            switch (this.cbCmdType.SelectedIndex)
            {
                case 0:
                    txtNRule.Text += lbInfo.Value;
                    break;
                case 1:
                    txtNRule.Text += "<Regex:" + lbInfo.Value + ">";
                    break;
                case 2:
                    txtNRule.Text += "<Common:" + lbInfo.Value + ">";
                    break;
                case 3:
                    txtNRule.Text += "<End:" + lbInfo.Value + ">";
                    break;
            }
        }
        public string ResultValue { get; set; }

        private void frmAddNavRules_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNRule.Text))
            {
                this.ResultValue = txtNRule.Text;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtNRule.Text = "";
            lbResult.DataViewValue = "";
        }

        private void cmdTest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNRule.Text.Trim()) || string.IsNullOrEmpty(lbMainUrl.Value))
            {
                WinFormLib.Core.MessageBoxHelper.ShowError("规则不能为空！");
                return;
            }
            cGatherTaskThreadBase taskBase = new cGatherTaskThreadBase();
            string html = string.Empty;
            int count = 3;
            while (true)
            {
                try
                {
                    html = taskBase.GetHtml(lbMainUrl.Value, "", EnumGloabParas.EnumEncodeType.AUTO, "", "", false);
                }
                catch (Exception)
                {
                    count--;
                    if (count < 0)
                    {
                        html = string.Empty;
                        break;
                    }
                }
                break;
            }
            List<string> urls = taskBase.GetNextLevelUrl(lbMainUrl.Value, html, txtNRule.Text);
            if (urls.Count > 0)
            {
                lbResult.DataViewValue = "";
                foreach (var item in urls)
                {
                    lbResult.DataViewValue += item + "\r\n";
                }
            }
            else
            {
                lbResult.DataViewValue = "测试结果失败";
            }

        }
    }
}
