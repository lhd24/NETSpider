using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NETSpider.Entity;
using System.Collections;

namespace NETSpider.Gather
{
    public class cGatherUrlBaseItem
    {
        public string Url { get; set; }
        public EnumGloabParas.EnumUrlGaterherState GaterherFlag { get; set; }
    }
    public class cGatherUrlItem : cGatherUrlBaseItem
    {

        public int Level { get; set; }
        public List<string> LevelUrlList { get; set; }
        public string NextPageText { get; set; }
        public string StartPos { get; set; }
        public string EndPos { get; set; }

    }
    public class cGatherUrlItemComparer : IEqualityComparer<cGatherUrlItem>
    {
        public bool Equals(cGatherUrlItem x, cGatherUrlItem y)
        {
            return x.Url.ToUpper() == y.Url.ToUpper();
        }
        public int GetHashCode(cGatherUrlItem obj)
        {
            return obj.Url.ToUpper().GetHashCode();
        }
    }
    
}
