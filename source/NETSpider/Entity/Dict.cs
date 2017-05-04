using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NETSpider.Entity
{
    public class DictList : List<Dict>
    {

    }
    public class Dict
    {
        public Dict()
        {
            this.CategoryName = CDataItem.Instance("");
            this.DictItemList = new List<DictItem>();
        }
        public CDataItem CategoryName { get; set; }

        public List<DictItem> DictItemList { get; set; }
    }
    public class DictItem
    {
        public DictItem()
        {
            this.DictName = CDataItem.Instance("");
        }
        public CDataItem DictName { get; set; }
    }
}
