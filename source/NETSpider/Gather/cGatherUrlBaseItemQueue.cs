using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace NETSpider.Gather
{
    public class cGatherUrlBaseItemQueue
    {
        public object SyncRoot = new object();
        public cGatherUrlBaseItemQueue()
        {
            this.FileUrls = new Queue<cGatherUrlBaseItem>();
        }
        public Queue<cGatherUrlBaseItem> FileUrls { get; set; }

        public bool ContainsUrl(string url)
        {
            bool resultFlag = false;
            lock (((ICollection)FileUrls).SyncRoot)
            {
                resultFlag = FileUrls.Where(q => q.Url == url).FirstOrDefault() != null;
            }
            return false;
        }
        public List<cGatherUrlBaseItem> ToList()
        {
            List<cGatherUrlBaseItem> runUrls = new List<cGatherUrlBaseItem>();
            lock (((ICollection)FileUrls).SyncRoot)
            {
                runUrls.AddRange(this.FileUrls.ToList());
            }
            return runUrls;
        }
        public int Count
        {
            get { return this.FileUrls.Count; }
        }

        public void Enqueue(cGatherUrlBaseItem item)
        {
            this.FileUrls.Enqueue(item);
        }
        public cGatherUrlBaseItem Dequeue()
        {
            cGatherUrlBaseItem item = null;
            lock (((ICollection)FileUrls).SyncRoot)
            {
                if (this.FileUrls.Count > 0)
                {
                    lock (((ICollection)FileUrls).SyncRoot)
                    {
                        item = FileUrls.Dequeue();
                        return item;
                    }
                }
            }
            return item;
        }
    }
}
