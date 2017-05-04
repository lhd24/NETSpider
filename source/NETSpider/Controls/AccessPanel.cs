using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using WinFormLib.Core;

namespace NETSpider.Controls
{
    public partial class AccessPanel : UserControl, IConectionPanel
    {
        public AccessPanel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Title = "选择Access文件";
            openFileDialog1.InitialDirectory = Program.GetConfigPath();
            openFileDialog1.Filter = "Access Files(*.mdb)|*.mdb|All Files(*.*)|*.*";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                lbAccessName.DataViewValue = this.openFileDialog1.FileName;
            }
        }

        public bool TestCon()
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = GetConectionString();
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowError(ex.Message);
                return false;
            }
            conn.Close();
            MessageBoxHelper.Show("测试连接成功！");
            return true;
        }

        public string GetConectionString()
        {
            string connectionstring = "provider=microsoft.jet.oledb.4.0;data source=";
            connectionstring += this.lbAccessName.Value + ";";
            if (chkValidate.Checked && !string.IsNullOrEmpty(this.lbAccessUser.Value))
            {
                connectionstring += "User ID=" + this.lbAccessUser.Value + ";Jet OLEDB:Database Password=" + this.lbAccessPwd.Value + ";Persist Security Info=true;";
            }
            return connectionstring;
        }
    }
}
