using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NETSpider.Entity;
using System.Threading;
using System.Collections;
using System.Data;

namespace NETSpider.Gather
{


    public class cGatherTaskManage : IDisposable
    {
        public void Dispose()
        {
            this.m_TaskItemEntity = null;
        }
        private TaskRunItem m_TaskItemEntity;
        #region Protities
        public EnumGloabParas.EnumEncodeType WebEncode
        {
            get { return m_TaskItemEntity.WebEncode; }
        }
        public bool IsAjax
        {
            get { return m_TaskItemEntity.IsAjax; }
        }
        public string WebCookie
        {
            get { return m_TaskItemEntity.WebCookie.Value; }
        }
        public int TotalCount
        {
            get { return m_TaskItemEntity.TotalCount; }
        }
        public int TrueCount
        {
            get { return m_TaskItemEntity.TrueCount; }
        }
        public int ErrorCount
        {
            get { return m_TaskItemEntity.ErrorCount; }
        }
        public string DownFilePath
        {
            get { return m_TaskItemEntity.DownFilePath.Value; }
        }
        public string TaskTempName
        {
            get { return m_TaskItemEntity.TaskTempName.Value; }
        }
        public int TryAgainCount
        {
            get { return m_TaskItemEntity.TryAgainFlag ? m_TaskItemEntity.TryAgainCount : 0; }
        }
        public EnumGloabParas.EnumGatherTaskType GatherTaskType
        {
            get;
            set;
        }
        public List<TaskColumnItem> ColumnItemList
        {
            get { return m_TaskItemEntity.ColumnItemList; }
        }
        public string LastStartPos
        {
            get
            {
                return string.IsNullOrEmpty(m_TaskItemEntity.LastStartPos.Value) ? "" : m_TaskItemEntity.LastStartPos.Value;
            }
        }
        public string LastEndPos
        {
            get
            {
                return string.IsNullOrEmpty(m_TaskItemEntity.LastEndPos.Value) ? "" : m_TaskItemEntity.LastEndPos.Value;
            }
        }
        public Int64 TaskID { get { return m_TaskItemEntity.TaskID; } }
        public int ThreadNum { get; set; }
        private List<cGatherTaskThreadItem> threadItemList = new List<cGatherTaskThreadItem>();
        private cGatherGetTempData cTempData = new cGatherGetTempData();

        internal List<string> completeThreadList = new List<string>();
        private DateTime startTime = DateTime.Now;
        public bool? _downFileFlag;
        /// <summary>
        /// 是否要下载文件
        /// </summary>
        public bool DownFileFlag
        {
            get
            {
                if (_downFileFlag.HasValue)
                {
                    return _downFileFlag.Value;
                }
                _downFileFlag = this.ColumnItemList.Where(q => q.DataFileType != EnumGloabParas.EnumDataFileType.Text).FirstOrDefault() != null;
                return _downFileFlag.Value;
            }
        }
        public bool DownFileQueue
        {
            get { return m_TaskItemEntity.DownFileQueue; }
        }
        public EnumGloabParas.EnumThreadState GaterherState
        {
            get { return m_TaskItemEntity.GaterherState; }
            set { m_TaskItemEntity.GaterherState = value; }
        }
        #endregion
        public cGatherTaskManage(TaskRunItem _taskItemEntity)
        {
            this.LoadTask(_taskItemEntity);
        }
        private int _thredPos = 0;
        public int ThreadPos
        {
            get
            {
                if (_thredPos >= this.ThreadNum)
                {
                    _thredPos = 0;
                }
                return _thredPos++;
            }
        }
        public void LoadTask(TaskRunItem _taskItemEntity)
        {
            this.GatherTaskType = EnumGloabParas.EnumGatherTaskType.Noraml;
            this.m_TaskItemEntity = _taskItemEntity;
            this.ThreadNum = _taskItemEntity.ThreadNum;
            runWebUrls = new cGatherUrlItemDictionary();
            runFileUrls = new cGatherUrlBaseItemDictionary();

            for (var i = 0; i < this.ThreadNum; i++)
            {
                runWebUrls.Add(i, new cGatherUrlItemQueue());
                runFileUrls.Add(i, new cGatherUrlBaseItemQueue());
            }
            if (_taskItemEntity.GatherUrlItemTempList != null)
            {
                List<cGatherUrlItem> mainList = _taskItemEntity.GatherUrlItemTempList.Where(q => q.GaterherFlag != EnumGloabParas.EnumUrlGaterherState.Run).ToList();
                foreach (var item in mainList)
                {
                    runWebUrls[0].Enqueue(item);
                }
                mainList = _taskItemEntity.GatherUrlItemTempList.Where(q => q.GaterherFlag == EnumGloabParas.EnumUrlGaterherState.Run).ToList();
                foreach (var item in mainList)
                {
                    runWebUrls[ThreadPos].Enqueue(item);
                }
            }
            if (_taskItemEntity.GatherFileItemTempList != null)
            {
                foreach (var item in _taskItemEntity.GatherFileItemTempList)
                {
                    runFileUrls[ThreadPos].Enqueue(item);
                }

            }
            if (_taskItemEntity.GatherUrlItemCompleteList != null)
            {
                this.runWebUrls.AddRange(_taskItemEntity.GatherUrlItemCompleteList);
            }
            if (_taskItemEntity.GatherFileItemCompleteList != null)
            {
                this.runFileUrls.AddRange(_taskItemEntity.GatherFileItemCompleteList);
            }
            this.GatherTaskType = EnumGloabParas.EnumGatherTaskType.RunData;
        }

        private void SetThreadNum()
        {
            this.ThreadNum = m_TaskItemEntity.ThreadNum;
            runWebUrls = new cGatherUrlItemDictionary();
            runFileUrls = new cGatherUrlBaseItemDictionary();
            for (var i = 0; i < this.ThreadNum; i++)
            {
                runWebUrls.Add(i, new cGatherUrlItemQueue());
                runFileUrls.Add(i, new cGatherUrlBaseItemQueue());
            }
        }

        public void Start()
        {
            while (this.GatherTaskType == EnumGloabParas.EnumGatherTaskType.Noraml)
            {
                System.Threading.Thread.Sleep(100);
            }
            startTime = DateTime.Now;
            if (m_TaskItemEntity == null)
            {
                throw new Exception("_taskItemEntity is null");
            }
            if (this.ThreadNum == 0)
            {
                throw new Exception("ThreadNum is null");
            }
            if (this.GaterherState == EnumGloabParas.EnumThreadState.SpiderCompleted)
            {
                this.OnThreadAllCompleted();
                return;
            }
            //任务暂停，再重启不用获取主网页
            if (this.GaterherState != EnumGloabParas.EnumThreadState.Suspended)
            {
                ThreadGetMainUrlsWork();
            }
            threadItemList = new List<cGatherTaskThreadItem>();
            this.m_ThreadState = EnumGloabParas.EnumThreadState.Run;
            completeThreadList = new List<string>();
            this.GaterherState = EnumGloabParas.EnumThreadState.Run;
            for (var i = 0; i < this.ThreadNum; i++)
            {
                cGatherTaskThreadItem threadItem = new cGatherTaskThreadItem(this, i);
                threadItem.e_TotalCount += new OnGatherTotalCount(cGatherTaskManage_e_TotalCount);
                threadItem.e_CompleteCount += new OnGatherCompleteCount(threadItem_e_CompleteCount);
                threadItem.e_OnGatherNotityCompleted += new OnGatherNotityCompleted(threadItem_e_OnGatherNotityCompleted);
                threadItem.e_OnGatherDataCompleted += new OnGatherDataCompleted(threadItem_e_OnGatherDataCompleted);
                threadItem.m_GatherData = GetDataTable();
                threadItem.ThreadState = EnumGloabParas.EnumThreadState.Run;
                this.threadItemList.Add(threadItem);
                threadItem.Start();
            }


        }
        object locker = new object();
        private DataTable GetDataTable()
        {
            DataTable dataTable = new System.Data.DataTable();
            foreach (var item in m_TaskItemEntity.ColumnItemList)
            {
                dataTable.Columns.Add(item.DataTextType.Value, typeof(string));
            }
            return dataTable;
        }
        public static object syncRoot = new object();
        void threadItem_e_OnGatherDataCompleted(cGatherDataEventArgs e)
        {
            lock (syncRoot)
            {
                if (e_OnGatherDataCompleted != null)
                {
                    e.RunWebUrls = runWebUrls.ToList();
                    e.RunCompleteWebUrls = runWebUrls.GetCompletedWebUrlsList();
                    e.RunCompleteFileUrls = runFileUrls.GetCompletedUrlsList();
                    e.RunFileUrls = runFileUrls.ToList();
                    e_OnGatherDataCompleted(e);
                }
            }
        }
        void threadItem_e_OnGatherNotityCompleted(string threadName)
        {
            lock (locker)
            {
                if (!completeThreadList.Contains(threadName))
                {
                    completeThreadList.Add(threadName);
                }
                if (completeThreadList.Count == this.ThreadNum)
                {
                    this.ThreadState = EnumGloabParas.EnumThreadState.SpiderCompleted;
                }
            }
        }
        public void Stop()
        {
            completeThreadList = new List<string>();
            this.m_ThreadState = EnumGloabParas.EnumThreadState.SpiderCompleted;
        }
        internal cGatherUrlItemDictionary runWebUrls = new cGatherUrlItemDictionary();
        internal cGatherUrlBaseItemDictionary runFileUrls = new cGatherUrlBaseItemDictionary();

        internal bool ContainsUrl(string url)
        {
            return this.runWebUrls.ContainsUrl(url);
        }
        internal bool ContainsDownFileUrl(string url)
        {
            return this.runFileUrls.ContainsUrl(url);
        }
        private void threadItem_e_CompleteCount(cGatherCompleteCountEventArgs e)
        {
            if (e.CompleteType == EnumGloabParas.EnumThreadCompleteType.Error)
            {
                this.runWebUrls.Add(new cGatherUrlItem()
                {
                    Url = e.Url,
                    GaterherFlag = EnumGloabParas.EnumUrlGaterherState.Error,
                    EndPos = e.EndPos,
                    Level = e.Level,
                    LevelUrlList = e.LevelUrlList,
                    NextPageText = e.NextPageText,
                    StartPos = e.StartPos,
                });
                m_TaskItemEntity.ErrorCount++;
            }
            else if (e.CompleteType == EnumGloabParas.EnumThreadCompleteType.Success)
            {
                this.runWebUrls.Add(new cGatherUrlItem()
                {
                    Url = e.Url,
                    GaterherFlag = EnumGloabParas.EnumUrlGaterherState.Completed,
                    EndPos = e.EndPos,
                    Level = e.Level,
                    LevelUrlList = e.LevelUrlList,
                    NextPageText = e.NextPageText,
                    StartPos = e.StartPos,
                });
                m_TaskItemEntity.TrueCount++;
            }
            else if (e.CompleteType == EnumGloabParas.EnumThreadCompleteType.FileError)
            {
                this.runFileUrls.Add(new cGatherUrlBaseItem()
                {
                    Url = e.Url,
                    GaterherFlag = EnumGloabParas.EnumUrlGaterherState.FileError,
                });
                m_TaskItemEntity.ErrorCount++;
            }
            else if (e.CompleteType == EnumGloabParas.EnumThreadCompleteType.FileSuccess)
            {
                this.runFileUrls.Add(new cGatherUrlBaseItem()
                {
                    Url = e.Url,
                    GaterherFlag = EnumGloabParas.EnumUrlGaterherState.FileCompleted,
                });
                m_TaskItemEntity.TrueCount++;
            }

            m_TaskItemEntity.GaterherState = EnumGloabParas.EnumThreadState.Run;
            if (e_GatherManagereCompleteCount != null)
            {
                e_GatherManagereCompleteCount(new cGatherCompletedEventArgs()
                {
                    TotalCount = m_TaskItemEntity.TotalCount,
                    TrueCount = m_TaskItemEntity.TrueCount,
                    ErrorCount = m_TaskItemEntity.ErrorCount,
                    GaterherState = m_TaskItemEntity.GaterherState,
                });
            }
        }

        private void cGatherTaskManage_e_TotalCount(cGatherCompletedEventArgs evt)
        {
            m_TaskItemEntity.GaterherState = EnumGloabParas.EnumThreadState.Run;
            m_TaskItemEntity.TotalCount += evt.TotalCount;
            if (e_TotalCount != null)
            {
                e_TotalCount(new cGatherCompletedEventArgs()
                {
                    ErrorCount = m_TaskItemEntity.ErrorCount,
                    TotalCount = m_TaskItemEntity.TotalCount,
                    TrueCount = m_TaskItemEntity.TrueCount,
                    GaterherState = m_TaskItemEntity.GaterherState,
                });
            }
        }
        private EnumGloabParas.EnumThreadState m_ThreadState = EnumGloabParas.EnumThreadState.Normal;
        public EnumGloabParas.EnumThreadState ThreadState
        {
            get { return m_ThreadState; }
            set { m_ThreadState = value; }
        }

        public void OnThreadAbort(cGatherTaskThreadItem item)
        {
            lock (locker)
            {
                this.ThreadNum--;
                if (this.ThreadNum == 0)
                {
                    this.OnThreadAllCompleted();
                }
            }
        }

        /// <summary>
        /// 所有线程执行完成后触发的任务
        /// </summary>
        private void OnThreadAllCompleted()
        {
            this.m_ThreadState = EnumGloabParas.EnumThreadState.SpiderCompleted;
            System.Threading.Thread.Sleep(1000);//回收线程中
            //再次验证线程是否结束
            if (e_ThreadAllCompleted != null)
            {
                int queueCount = runWebUrls.QueueCount;
                m_TaskItemEntity.GaterherState = queueCount == 0 ? EnumGloabParas.EnumThreadState.SpiderCompleted : EnumGloabParas.EnumThreadState.Suspended;
                e_ThreadAllCompleted(new cGatherUrlItemEventArgs()
                {
                    RunWebUrls = this.runWebUrls.ToList(),
                    RunCompleteWebUrls = this.runWebUrls.GetCompletedWebUrlsList(),
                    RunFileUrls = this.runFileUrls.ToList(),
                    RunCompleteFileUrls = this.runFileUrls.GetCompletedUrlsList(),
                    ErrorCount = m_TaskItemEntity.ErrorCount,
                    TrueCount = m_TaskItemEntity.TrueCount,
                    TotalCount = m_TaskItemEntity.TotalCount,
                });
            }
        }
        public void OnLog(cGatherEventArgs evt)
        {
            if (e_Log != null)
            {
                e_Log(evt);
            }
        }

        private void ThreadGetMainUrlsWork()
        {
            lock (((ICollection)runWebUrls).SyncRoot)
            {
                foreach (var item in this.m_TaskItemEntity.UrlList)
                {
                    string url = item.MainUrl.Value;
                    string nextPageText = item.NextPageFlag ? item.NextPageText.Value : "";
                    List<string> levelUrlList = item.LevelUrlList.Select(q => q.LevelUrl).ToList();
                    string tempUrl = cTempData.GetMatchUrl(url, nextPageText, levelUrlList, "", "", this.EnqueueUrls);
                    this.EnqueueUrls(tempUrl, nextPageText, levelUrlList, "", "");
                }
            }
        }
        private void EnqueueUrls(string url, string nextPageText, List<string> levelUrlList, string startPos, string endPos)
        {
            if (this.ContainsUrl(url))
            {
                return;
            }
            EnumGloabParas.EnumUrlGaterherState gaterherFlag = levelUrlList.Count > 0 ? EnumGloabParas.EnumUrlGaterherState.First : EnumGloabParas.EnumUrlGaterherState.Run;
            this.runWebUrls[this.ThreadPos].Enqueue(new cGatherUrlItem()
            {
                Url = url,
                GaterherFlag = gaterherFlag,
                Level = 0,
                LevelUrlList = levelUrlList,
                NextPageText = nextPageText,
                StartPos = startPos,
                EndPos = endPos,
            });
            m_TaskItemEntity.TotalCount++;
            if (e_TotalCount != null)
            {
                e_TotalCount(new cGatherCompletedEventArgs()
                {
                    TotalCount = m_TaskItemEntity.TotalCount,
                    TrueCount = m_TaskItemEntity.TrueCount,
                    ErrorCount = m_TaskItemEntity.ErrorCount,
                });
            }
        }



        public event OnGatherLog e_Log;
        public event OnGatherTotalCount e_TotalCount;
        public event OnGatherThreadAllCompleted e_ThreadAllCompleted;
        //public event OnGatherGetMainUrlCompleted e_GatherGetMainUrlCompeleted;
        public event OnGatherManagereCompleteCount e_GatherManagereCompleteCount;
        /// <summary>
        /// 
        /// </summary>
        public event OnGatherDataCompleted e_OnGatherDataCompleted;


    }
}
