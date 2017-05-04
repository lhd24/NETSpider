using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace NETSpider
{
    public class CDataItem : IXmlSerializable
    {
        public static CDataItem Instance(string value)
        {
            return new CDataItem(value);
        }
        private string _value;

        public CDataItem() { }

        public CDataItem(string value)
        {
            this._value = value;
        }

        public string Value
        {
            get { return _value; }
        }

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            this._value = reader.ReadElementContentAsString();
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteCData(this._value);
        }

        public override string ToString()
        {
            return this._value;
        }

        public static implicit operator CDataItem(string text)
        {
            return new CDataItem(text);
        }
    }
}
