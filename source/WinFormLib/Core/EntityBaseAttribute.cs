using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinFormLib.Core
{
    public class EntityBaseAttribute : Attribute
    {
        public EntityBaseAttribute()
        {
        }
        public EntityBaseAttribute(int maxLength)
        {
            this.MaxLength = maxLength;
        }
        public int MaxLength { get; set; }
    }
}
