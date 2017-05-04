using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinFormLib.Core
{
    public interface ISerializeStyle
    {
        string ToSerializeStyle();
        void FromSerializeStyle(string Value);
    }
    [Serializable]
    public class SerializeStyle
    {
        public int ColumnIndex { get; set; }
        public int DisplayIndex { get; set; }
        public string ColumnName { get; set; }
        public int Width { get; set; }
    }
    [Serializable]
    public class SerializeStyleList : List<SerializeStyle>
    {

    }
}
