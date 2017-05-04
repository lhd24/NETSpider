using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NETSpider.Gather
{
    public class cControlInvoked
    {

        public static void AppendText(RichTextBox richTextBox, cGatherEventArgs evt)
        {
            richTextBox.Invoke(new OnGatherLog(delegate(cGatherEventArgs e)
            {
                richTextBox.AppendText(e.MessageType + e.ThreadName + ":" + e.Message + "\r\n");
                richTextBox.ScrollToCaret();
            }), evt);
        }
        public static void UpdateLableText(WinFormLib.Controls.LableTextBox control, string text)
        {
            control.Invoke(new MethodInvoker(delegate()
            {
                control.DataViewValue = text;
            }));
        }
        public static void UpdateText(Control control, string text)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new MethodInvoker(delegate()
                {
                    control.Text = text;
                }));
            }
            else
            {
                control.Text = text;
            }
        }
    }
}
