using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinFormLib.Components.Cell
{
    public class DataGridViewCheckboxHeaderEventArgs : EventArgs
    {
        private bool checkedState = false;

        public bool CheckedState
        {
            get { return checkedState; }
            set { checkedState = value; }
        }
    }

    public delegate void DataGridViewCheckBoxHeaderEventHander(object sender, DataGridViewCheckboxHeaderEventArgs e);
}
