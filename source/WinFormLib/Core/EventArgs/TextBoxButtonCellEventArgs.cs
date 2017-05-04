using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormLib.Core
{
    public class TextBoxButtonCellEventArgs : EventArgs
    {
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public object Value { get; set; }
        public Control DataGridViewTextBoxButton { get; set; }
        public new static TextBoxButtonCellEventArgs Empty
        {
            get { return new TextBoxButtonCellEventArgs(); }
        }
        public TextBoxButtonCellEventArgs()
        {

        }
        public TextBoxButtonCellEventArgs(string s)
        {

        }
    }
}
