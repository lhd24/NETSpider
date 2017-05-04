using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinFormLib.Core
{
    public class KeyValue
    {
        public string DisplayMember { get; set; }
        public string ValueMember { get; set; }
        public object OtherMember { get; set; }
    }
    public class KeyValueCollection : List<KeyValue>
    {

    }
}
