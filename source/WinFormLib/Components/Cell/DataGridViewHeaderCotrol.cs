using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace WinFormLib.Components.Cell
{
    public class DataGridViewHeaderCotrol : UserControl
    {
        public string HeaderText { get; set; }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawString(HeaderText, new Font("Tahoma", 8.25f), Brushes.Black, new PointF(0, 2));
        }
    }
}
