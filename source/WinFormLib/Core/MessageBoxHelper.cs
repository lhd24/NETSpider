using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormLib.Core
{
    public class MessageBoxHelper
    {
        /// <summary>
        /// 弹出提示信息
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static System.Windows.Forms.DialogResult Show(string text)
        {
            if (!string.IsNullOrEmpty(text) && text.Length < 15)
            {
                text = text + "                         ";
            }
            Form topmostForm = new Form();
            // We do not want anyone to see this window so position it off the visible screen and make it as small as possible
            topmostForm.Size = new System.Drawing.Size(1, 1);
            topmostForm.StartPosition = FormStartPosition.Manual;
            System.Drawing.Rectangle rect = SystemInformation.VirtualScreen;
            topmostForm.Location = new System.Drawing.Point(rect.Bottom + 10, rect.Right + 10);
            topmostForm.Show();
            // Make this form the active form and make it TopMost
            topmostForm.Focus();
            topmostForm.BringToFront();
            topmostForm.ShowInTaskbar = false;
            topmostForm.TopMost = true;
            DialogResult result = System.Windows.Forms.MessageBox.Show(topmostForm, text, "系统提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            topmostForm.Dispose();
            return result;
        }
        /// <summary>
        /// 返回Confirm的提示信息
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static System.Windows.Forms.DialogResult ShowQuestion(string text)
        {
            if (!string.IsNullOrEmpty(text) && text.Length < 15)
            {
                text = text + "                         ";
            }
            Form topmostForm = new Form();
            // We do not want anyone to see this window so position it off the visible screen and make it as small as possible
            topmostForm.Size = new System.Drawing.Size(1, 1);
            topmostForm.StartPosition = FormStartPosition.Manual;
            System.Drawing.Rectangle rect = SystemInformation.VirtualScreen;
            topmostForm.Location = new System.Drawing.Point(rect.Bottom + 10, rect.Right + 10);
            topmostForm.Show();
            // Make this form the active form and make it TopMost
            topmostForm.Focus();
            topmostForm.BringToFront();
            topmostForm.TopMost = true;
            topmostForm.ShowInTaskbar = false;
            DialogResult result = MessageBox.Show(text, "系统提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            topmostForm.Dispose();
            return result;
        }
        /// <summary>
        /// 返回错误提示
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static System.Windows.Forms.DialogResult ShowError(string text)
        {
            if (!string.IsNullOrEmpty(text) && text.Length < 15)
            {
                text = text + "                         ";
            }
            Form topmostForm = new Form();
            // We do not want anyone to see this window so position it off the visible screen and make it as small as possible
            topmostForm.Size = new System.Drawing.Size(1, 1);
            topmostForm.StartPosition = FormStartPosition.Manual;
            System.Drawing.Rectangle rect = SystemInformation.VirtualScreen;
            topmostForm.Location = new System.Drawing.Point(rect.Bottom + 10, rect.Right + 10);
            topmostForm.Show();
            // Make this form the active form and make it TopMost
            topmostForm.Focus();
            topmostForm.BringToFront();
            topmostForm.TopMost = true;
            topmostForm.ShowInTaskbar = false;
            DialogResult result = MessageBox.Show(topmostForm, text, "系统提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            topmostForm.Dispose();
            return result;
        }
        /// <summary>
        /// 返回警告提示
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static System.Windows.Forms.DialogResult ShowWarning(string text)
        {
            if (!string.IsNullOrEmpty(text) && text.Length < 15)
            {
                text = text + "                         ";
            }
            Form topmostForm = new Form();
            // We do not want anyone to see this window so position it off the visible screen and make it as small as possible
            topmostForm.Size = new System.Drawing.Size(1, 1);
            topmostForm.StartPosition = FormStartPosition.Manual;
            System.Drawing.Rectangle rect = SystemInformation.VirtualScreen;
            topmostForm.Location = new System.Drawing.Point(rect.Bottom + 10, rect.Right + 10);
            topmostForm.Show();
            // Make this form the active form and make it TopMost
            topmostForm.Focus();
            topmostForm.BringToFront();
            topmostForm.TopMost = true;
            topmostForm.ShowInTaskbar = false;
            DialogResult result = MessageBox.Show(text, "系统提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            topmostForm.Dispose();
            return result;
        }
    }
}
