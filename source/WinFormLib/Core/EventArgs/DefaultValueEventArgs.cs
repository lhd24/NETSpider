using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinFormLib.Core
{
    public class DefaultValueEventArgs : EventArgs
    {
        public int RowIndex { get; set; }
        public DefaultValueEventArgs()
        {

        }
        public DefaultValueEventArgs(int rowIndex)
        {
            this.RowIndex = rowIndex;
        }
    }
}
