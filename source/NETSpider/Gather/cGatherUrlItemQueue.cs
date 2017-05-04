using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace NETSpider.Gather
{
    public class cGatherUrlItemQueue
    {
        public object SyncRoot = new object();
        public cGatherUrlItemQueue()
        {
            this.MainUrls = new Queue<cGatherUrlItem>();
            this.RunUrls = new Queue<cGatherUrlItem>();
        }
        public Queue<cGatherUrlItem> MainUrls { get; set; }
        public Queue<cGatherUrlItem> RunUrls { get; set; }
        public bool ContainsUrl(string url)
        {
            bool resultFlag = false;
            lock (((ICollection)RunUrls).SyncRoot)
            {
                resultFlag = RunUrls.Where(q => q.Url == url).FirstOrDefault() != null;
            }
            if (resultFlag == false)
            {
                lock (((ICollection)MainUrls).SyncRoot)
                {
                    resultFlag = MainUrls.Where(q => q.Url == url).FirstOrDefault() != null;
                }
            }
            return resultFlag;
        }
        public List<cGatherUrlItem> ToList()
        {
            List<cGatherUrlItem> runUrls = new List<cGatherUrlItem>();
            lock (((ICollection)MainUrls).SyncRoot)
            {
                runUrls.AddRange(this.MainUrls.ToList());
            }
            lock (((ICollection)RunUrls).SyncRoot)
            {
                runUrls.AddRange(this.RunUrls.ToList());
            }
            return runUrls;
        }
        public int Count
        {
            get { return this.MainUrls.Count + this.RunUrls.Count; }
        }

        public void Enqueue(cGatherUrlItem item)
        {
            if (item.GaterherFlag == EnumGloabParas.EnumUrlGaterherState.Run)
            {
                this.RunUrls.Enqueue(item);
            }
            else
            {
                this.MainUrls.Enqueue(item);
            }
        }
        public cGatherUrlItem Dequeue()
        {
            cGatherUrlItem item = null;
            lock (((ICollection)MainUrls).SyncRoot)
            {
                if (this.MainUrls.Count > 0)
                {
                    lock (((ICollection)MainUrls).SyncRoot)
                    {
                        item = MainUrls.Dequeue();
                        return item;
                    }
                }
            }
            lock (((ICollection)RunUrls).SyncRoot)
            {
                if (this.RunUrls.Count > 0)
                {
                    lock (((ICollection)RunUrls).SyncRoot)
                    {
                        item = RunUrls.Dequeue();
                        return item;
                    }
                }
            }
            return item;
        }
    }
}
