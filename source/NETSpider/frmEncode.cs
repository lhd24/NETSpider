using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NETSpider
{
    public partial class frmEncode : Form
    {
        public frmEncode()
        {
            InitializeComponent();
        }

        private void frmEncode_Load(object sender, EventArgs e)
        {
            EnumHelper.InitItemList(typeof(EnumGloabParas.EnumEncodeType), this.comboBoxExt1);
            this.comboBoxExt1.SelectedIndex = 0;
        }
        private EnumGloabParas.EnumEncodeType GetEndcode()
        {
            EnumGloabParas.EnumEncodeType enocodeType = (EnumGloabParas.EnumEncodeType)Enum.Parse(typeof(EnumGloabParas.EnumEncodeType), comboBoxExt1.SelectedValue.ToString());
            return enocodeType;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            EnumGloabParas.EnumEncodeType enocodeType = GetEndcode();
            switch (enocodeType)
            {
                case EnumGloabParas.EnumEncodeType.AUTO:
                case EnumGloabParas.EnumEncodeType.UTF8:
                    this.textBox2.Text = System.Web.HttpUtility.UrlEncode(this.textBox1.Text, Encoding.UTF8);
                    break;
                case EnumGloabParas.EnumEncodeType.BIG5:
                    this.textBox2.Text = System.Web.HttpUtility.UrlEncode(this.textBox1.Text, Encoding.GetEncoding("big5"));
                    break;
                case EnumGloabParas.EnumEncodeType.GB2312:
                    this.textBox2.Text = System.Web.HttpUtility.UrlEncode(this.textBox1.Text, Encoding.GetEncoding("gb2312"));
                    break;
                case EnumGloabParas.EnumEncodeType.GBK:
                    this.textBox2.Text = System.Web.HttpUtility.UrlEncode(this.textBox1.Text, Encoding.GetEncoding("gbk"));
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EnumGloabParas.EnumEncodeType enocodeType = GetEndcode();
            switch (enocodeType)
            {
                case EnumGloabParas.EnumEncodeType.AUTO:
                case EnumGloabParas.EnumEncodeType.UTF8:
                    this.textBox2.Text = System.Web.HttpUtility.UrlDecode(this.textBox1.Text, Encoding.UTF8);
                    break;
                case EnumGloabParas.EnumEncodeType.BIG5:
                    this.textBox2.Text = System.Web.HttpUtility.UrlDecode(this.textBox1.Text, Encoding.GetEncoding("big5"));
                    break;
                case EnumGloabParas.EnumEncodeType.GB2312:
                    this.textBox2.Text = System.Web.HttpUtility.UrlDecode(this.textBox1.Text, Encoding.GetEncoding("gb2312"));
                    break;
                case EnumGloabParas.EnumEncodeType.GBK:
                    this.textBox2.Text = System.Web.HttpUtility.UrlDecode(this.textBox1.Text, Encoding.GetEncoding("gbk"));
                    break;
            }
        }
    }
}
