using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NETSpider.Controls;

namespace NETSpider
{
    public partial class frmDataConfig : Form
    {
        private Controls.IConectionPanel conPanel;
        private EnumGloabParas.EnumConnectionType connectionType = EnumGloabParas.EnumConnectionType.ExportMSSQL;
        public frmDataConfig(EnumGloabParas.EnumConnectionType _connectionType)
        {
            InitializeComponent();
            this.connectionType = _connectionType;
            switch (_connectionType)
            {
                case EnumGloabParas.EnumConnectionType.ExportMSSQL:
                default:
                    NETSpider.Controls.MSPanel msPanel1 = new NETSpider.Controls.MSPanel();
                    msPanel1.Location = new System.Drawing.Point(3, 5);
                    msPanel1.Name = "msPanel1";
                    msPanel1.Size = new System.Drawing.Size(365, 134);
                    msPanel1.TabIndex = 43;
                    conPanel = msPanel1;
                    this.Controls.Add(msPanel1);
                    break;
                case EnumGloabParas.EnumConnectionType.ExportMySql:
                    NETSpider.Controls.MySqlPanel myPanel1 = new NETSpider.Controls.MySqlPanel();
                    myPanel1.Location = new System.Drawing.Point(3, 5);
                    myPanel1.Name = "myPanel1";
                    myPanel1.Size = new System.Drawing.Size(365, 134);
                    myPanel1.TabIndex = 43;
                    conPanel = myPanel1;
                    this.Controls.Add(myPanel1);
                    break;
                case EnumGloabParas.EnumConnectionType.ExportAccess:
                    NETSpider.Controls.AccessPanel accPanel1 = new NETSpider.Controls.AccessPanel();
                    accPanel1.Location = new System.Drawing.Point(3, 5);
                    accPanel1.Name = "accPanel1";
                    accPanel1.Size = new System.Drawing.Size(365, 134);
                    accPanel1.TabIndex = 43;
                    conPanel = accPanel1;
                    this.Controls.Add(accPanel1);
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (conPanel != null)
            {
                string ConnectionString = conPanel.GetConectionString();
                if (e_OnReturnDataSource != null)
                {
                    e_OnReturnDataSource(this.connectionType, ConnectionString);
                }
                this.Close();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (conPanel != null)
            {
                conPanel.TestCon();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public event OnReturnDataSource e_OnReturnDataSource;
    }
}
