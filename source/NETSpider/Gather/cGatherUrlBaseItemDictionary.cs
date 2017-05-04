using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace NETSpider.Gather
{
    public class cGatherUrlBaseItemDictionary : Dictionary<int, cGatherUrlBaseItemQueue>
    {
        public cGatherUrlBaseItemDictionary()
        {
            this.CompletedUrls = new List<cGatherUrlBaseItem>();
        }
        public bool ContainsUrl(string url)
        {
            bool resultFlag = false;
            lock (((ICollection)this).SyncRoot)
            {
                foreach (cGatherUrlBaseItemQueue item in this.Values)
                {
                    resultFlag = item.ContainsUrl(url);
                    if (resultFlag)
                    {
                        return true;
                    }
                }
                resultFlag = CompletedUrls.Where(q => q.Url == url).FirstOrDefault() != null;
                return resultFlag;
            }
            
        }
        public List<cGatherUrlBaseItem> ToList()
        {
            lock (((ICollection)this).SyncRoot)
            {
                List<cGatherUrlBaseItem> runUrls = new List<cGatherUrlBaseItem>();
                foreach (var item in this)
                {
                    runUrls.AddRange(item.Value.ToList());
                }
                return runUrls;
            }
        }

        private List<cGatherUrlBaseItem> CompletedUrls { get; set; }

        public void Add(cGatherUrlBaseItem item)
        {
            lock (((ICollection)this).SyncRoot)
            {
                this.CompletedUrls.Add(item);
            }
        }
        public void AddRange(IEnumerable<cGatherUrlBaseItem> collection)
        {
            lock (((ICollection)this).SyncRoot)
            {
                this.CompletedUrls.AddRange(collection);
            }
        }
        public List<cGatherUrlBaseItem> GetCompletedUrlsList()
        {
            lock (((ICollection)this).SyncRoot)
            {
                return this.CompletedUrls;
            }
        }
    }


}
