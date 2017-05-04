using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using WinFormLib.Core;

namespace NETSpider.Controls
{
    public partial class MySqlPanel : UserControl, IConectionPanel
    {
        public MySqlPanel()
        {
            InitializeComponent();
            this.comMySqlCode.Items.Add("utf8");
            this.comMySqlCode.Items.Add("big5");
            this.comMySqlCode.Items.Add("gb2312");
            this.comMySqlCode.Items.Add("gbk");
            this.comMySqlCode.Items.Add("latin1");
            this.comMySqlCode.Items.Add("latin2");
            this.comMySqlCode.Items.Add("ascii");
            this.comMySqlCode.SelectedIndex = 0;
        }

        public bool TestCon()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = GetConectionString("mysql");
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

        private string GetConectionString(string database)
        {
            string connectionstring = "";
            connectionstring = "Server=" + this.lbMySql.Value + ";";
            connectionstring += "Port=" + this.lbMySqlPort.Value + ";";
            connectionstring += " Database=" + database + ";User Id=" + this.lbMySqlUser.Value + ";password=" + this.lbMySqlPwd.Value + ";";
            connectionstring += " character set=" + this.comMySqlCode.SelectedItem.ToString() + ";";
            return connectionstring;
        }

        public string GetConectionString()
        {
            return GetConectionString(this.cbDatabase.SelectedItem.ToString());
        }

        private void cbDatabase_DropDown(object sender, EventArgs e)
        {
            if (this.cbDatabase.Items.Count != 0)
                return;

            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = GetConectionString("mysql");
            try
            {
                conn.Open();
            }
            catch (System.Exception ex)
            {
                MessageBoxHelper.ShowError(ex.Message);
                return;
            }
            DataTable tb = conn.GetSchema("Databases");
            foreach (DataRow r in tb.Rows)
            {
                this.cbDatabase.Items.Add(r[1].ToString());
            }
        }
    }
}
