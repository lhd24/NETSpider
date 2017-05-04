using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace NETSpider.Gather
{
    public class cGatherUrlItemDictionary : Dictionary<int, cGatherUrlItemQueue>
    {
        public cGatherUrlItemDictionary()
        {
            this.CompletedWebUrls = new List<cGatherUrlItem>();
        }
        public bool ContainsUrl(string url)
        {
            bool resultFlag = false;
            lock (((ICollection)this).SyncRoot)
            {
                foreach (cGatherUrlItemQueue item in this.Values)
                {
                    resultFlag = item.ContainsUrl(url);
                    if (resultFlag)
                    {
                        return true;
                    }
                }
            }
            lock (((ICollection)CompletedWebUrls).SyncRoot)
            {
                resultFlag = CompletedWebUrls.Where(q => q.Url == url).FirstOrDefault() != null;
            }
            return resultFlag;
        }
        public List<cGatherUrlItem> ToList()
        {
            List<cGatherUrlItem> runUrls = new List<cGatherUrlItem>();
            foreach (var item in this)
            {
                runUrls.AddRange(item.Value.ToList());
            }
            return runUrls;
        }
        public int QueueCount
        {
            get
            {
                int count = 0;
                foreach (var item in this)
                {
                    count += item.Value.Count;
                }
                return count;
            }
        }
        private List<cGatherUrlItem> CompletedWebUrls { get; set; }

        public void Add(cGatherUrlItem item)
        {
            this.CompletedWebUrls.Add(item);
        }
        public void AddRange(IEnumerable<cGatherUrlItem> collection)
        {
            this.CompletedWebUrls.AddRange(collection);
        }
        public List<cGatherUrlItem> GetCompletedWebUrlsList()
        {
            return this.CompletedWebUrls;
        }
    }
}
