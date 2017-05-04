using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NETSpider.Entity;

namespace NETSpider.Controls
{
    public class RichTextBoxLog : RichTextBox
    {
        public RichTextBoxLog()
        {
            this.Text = "";
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                string strT = value;
                if (string.IsNullOrEmpty(strT))
                {
                    base.Text = "";
                    return;
                }
                if (strT.Length > 0)
                {
                    try
                    {
                        int infoType = int.Parse(strT.Substring(0, 4));
                        strT = strT.Substring(4, strT.Length - 4);
                        switch (infoType)
                        {
                            case (int)EnumGloabParas.EnumMessageType.ERROR:
                                base.SelectionFont = new System.Drawing.Font(DefaultFont, System.Drawing.FontStyle.Bold);
                                base.SelectionColor = System.Drawing.Color.Red;
                                break;
                            case (int)EnumGloabParas.EnumMessageType.INFO:
                                base.SelectionFont = new System.Drawing.Font(DefaultFont, System.Drawing.FontStyle.Regular);
                                base.SelectionColor = System.Drawing.Color.Black;
                                break;
                            case (int)EnumGloabParas.EnumMessageType.NOTIFY:
                                base.SelectionFont = new System.Drawing.Font(DefaultFont, System.Drawing.FontStyle.Bold);
                                base.SelectionColor = System.Drawing.Color.Magenta;
                                break;
                            case (int)EnumGloabParas.EnumMessageType.WARNING:
                                base.SelectionFont = new System.Drawing.Font(DefaultFont, System.Drawing.FontStyle.Bold);
                                base.SelectionColor = System.Drawing.Color.Orange;
                                break;
                            default:
                                base.SelectionFont = new System.Drawing.Font(DefaultFont, System.Drawing.FontStyle.Regular);
                                base.SelectionColor = System.Drawing.Color.Black;
                                break;
                        }

                        base.AppendText(strT);
                        base.SelectionStart = int.MaxValue;
                        base.ScrollToCaret();
                    }
                    catch (System.Exception)
                    {
                        base.Text = value + base.Text;
                    }
                }

            }
        }


    }
}
