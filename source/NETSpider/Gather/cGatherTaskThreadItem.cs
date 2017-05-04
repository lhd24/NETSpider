using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NETSpider.Entity;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data;
using System.ComponentModel;

namespace NETSpider.Gather
{
    public class cGatherTaskThreadItem : cGatherTaskThreadBase
    {
        private cGatherTaskManage m_GatherTaskManage;
        private int ThreadIndex = 0;
        private cGatherGetTempData _getTempData;
        internal string ThreadName { get; set; }
        public cGatherTaskThreadItem(cGatherTaskManage manager, int index)
        {
            this.m_GatherTaskManage = manager;
            this._getTempData = new cGatherGetTempData();
            this.ThreadIndex = index;
            this.ThreadName = "thread" + index;
            threadWorker = new BackgroundWorker();
            threadWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(threadWorker_RunWorkerCompleted);
            threadWorker.DoWork += new DoWorkEventHandler(threadWorker_DoWork);
        }

        void threadWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ThreadGetUrlsWork();
        }

        void threadWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            m_GatherTaskManage.OnThreadAbort(this);
        }

        public void Start()
        {
            try
            {
                this.ThreadState = EnumGloabParas.EnumThreadState.Run;
                threadWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                m_GatherTaskManage.OnLog(new cGatherEventArgs() { Message = ex.Message, MessageType = EnumGloabParas.EnumMessageType.ERROR });
            }
        }
        public void Stop()
        {
            try
            {
                //if (threadUrls.ThreadState == System.Threading.ThreadState.Running)
                {
                    this.ThreadState = EnumGloabParas.EnumThreadState.SpiderCompleted;
                    threadWorker.CancelAsync();
                }
            }
            catch (Exception ex)
            {
                m_GatherTaskManage.OnLog(new cGatherEventArgs()
                {
                    Message = ex.Message,
                    ThreadName = this.ThreadName,
                    MessageType = EnumGloabParas.EnumMessageType.ERROR
                });
            }
        }
        private static object syncroot = new object();
        public DataTable m_GatherData { get; set; }

        public EnumGloabParas.EnumThreadState ThreadState
        {
            get;
            set;
        }
        internal void ThreadGetUrlsWork()
        {
            this.ThreadState = EnumGloabParas.EnumThreadState.Run;
            cGatherUrlItem gatherUrlItem;
            while (m_GatherTaskManage.ThreadState == EnumGloabParas.EnumThreadState.Run
                && this.ThreadState == EnumGloabParas.EnumThreadState.Run)
            {
                #region 始终在执行线程
                try
                {

                    if (m_GatherTaskManage.runWebUrls[this.ThreadIndex].Count > 0)
                    {

                        #region 采集网页数据中
                        lock ((m_GatherTaskManage.runWebUrls[this.ThreadIndex]).SyncRoot)
                        {
                            if (m_GatherTaskManage.runWebUrls[this.ThreadIndex].Count > 0)
                            {
                                lock (((ICollection)m_GatherTaskManage.completeThreadList).SyncRoot)
                                {
                                    if (m_GatherTaskManage.completeThreadList.Contains(this.ThreadName))
                                    {
                                        lock (((ICollection)m_GatherTaskManage.completeThreadList).SyncRoot)
                                        {
                                            if (m_GatherTaskManage.completeThreadList.Contains(this.ThreadName))
                                            {
                                                m_GatherTaskManage.completeThreadList.Remove(this.ThreadName);
                                            }
                                        }
                                    }
                                }
                                #region 运行采集网页数据
                                gatherUrlItem = m_GatherTaskManage.runWebUrls[this.ThreadIndex].Dequeue();

                                if (gatherUrlItem.GaterherFlag == EnumGloabParas.EnumUrlGaterherState.Run)
                                {
                                    bool exitFlag = false;
                                    string errMsg = string.Empty;
                                    GatherWebUrl(gatherUrlItem, ref exitFlag, ref errMsg);
                                    if (exitFlag) { break; }
                                }
                                else
                                {
                                    #region 查找下级网页
                                    m_GatherTaskManage.OnLog(new cGatherEventArgs()
                                    {
                                        Message = gatherUrlItem.Url + "查找下级网页中.",
                                        MessageType = EnumGloabParas.EnumMessageType.NOTIFY,
                                        ThreadName = ThreadName
                                    });

                                    string html = string.Empty;
                                    int tryCount = m_GatherTaskManage.TryAgainCount;
                                    bool exitFlag = false;
                                    #region 重试获取网页中
                                    while (true)
                                    {
                                        try
                                        {
                                            html = this.GetHtml(gatherUrlItem.Url, m_GatherTaskManage.WebCookie, m_GatherTaskManage.WebEncode, gatherUrlItem.StartPos, gatherUrlItem.EndPos, m_GatherTaskManage.IsAjax);
                                        }
                                        catch (Exception ex)
                                        {
                                            m_GatherTaskManage.OnLog(new cGatherEventArgs()
                                            {
                                                Message = DateTime.Now.Ticks + " " + gatherUrlItem.Url + "采集出现错误，重试中." + ex.Message,
                                                MessageType = EnumGloabParas.EnumMessageType.ERROR,
                                                ThreadName = ThreadName
                                            });
                                            if (tryCount > 0)
                                            {
                                                tryCount--;
                                                continue;
                                            }
                                            exitFlag = true;
                                        }
                                        break;
                                    }



                                    #endregion
                                    if (exitFlag)
                                    {
                                        m_GatherTaskManage.OnLog(new cGatherEventArgs()
                                        {
                                            Message = gatherUrlItem.Url + "查找下级网页失败.",
                                            MessageType = EnumGloabParas.EnumMessageType.ERROR,
                                            ThreadName = ThreadName
                                        });
                                        if (e_CompleteCount != null)
                                        {
                                            e_CompleteCount(new cGatherCompleteCountEventArgs()
                                            {
                                                CompleteType = EnumGloabParas.EnumThreadCompleteType.Error,
                                                StartPos = gatherUrlItem.StartPos,
                                                EndPos = gatherUrlItem.EndPos,
                                                GaterherFlag = EnumGloabParas.EnumUrlGaterherState.FirstError,
                                                Level = gatherUrlItem.Level,
                                                LevelUrlList = gatherUrlItem.LevelUrlList,
                                                NextPageText = gatherUrlItem.NextPageText,
                                                Url = gatherUrlItem.Url,
                                            });
                                        }
                                        continue;
                                    }
                                    if (!string.IsNullOrEmpty(gatherUrlItem.NextPageText))
                                    {
                                        string nextUrl = _getTempData.GetNextPage(gatherUrlItem.Url, html, gatherUrlItem.NextPageText);
                                        if (!string.IsNullOrEmpty(nextUrl))
                                        {
                                            nextUrl = this.GetNextUrl(nextUrl, gatherUrlItem.Url);
                                            if (!m_GatherTaskManage.ContainsUrl(nextUrl))
                                            {
                                                //这里始终加入到第一进程中,提前采集可用的网址,当无网址时 0线程将会采集Run的网页
                                                m_GatherTaskManage.runWebUrls[m_GatherTaskManage.ThreadPos].Enqueue(new cGatherUrlItem()
                                                {
                                                    GaterherFlag = gatherUrlItem.GaterherFlag,
                                                    LevelUrlList = gatherUrlItem.LevelUrlList,
                                                    NextPageText = gatherUrlItem.NextPageText,
                                                    Url = nextUrl,
                                                    Level = gatherUrlItem.Level,
                                                    StartPos = gatherUrlItem.StartPos,
                                                    EndPos = gatherUrlItem.EndPos,
                                                });
                                                if (e_TotalCount != null)
                                                {
                                                    e_TotalCount(new cGatherCompletedEventArgs()
                                                    {
                                                        ErrorCount = 0,
                                                        TotalCount = 1,
                                                        TrueCount = 0,
                                                    });
                                                }
                                                m_GatherTaskManage.OnLog(new cGatherEventArgs()
                                                {
                                                    Message = gatherUrlItem.Url + "获取下一页导航成功.",
                                                    MessageType = EnumGloabParas.EnumMessageType.NOTIFY,
                                                    ThreadName = ThreadName
                                                });
                                            }
                                        }
                                        else
                                        {
                                            m_GatherTaskManage.OnLog(new cGatherEventArgs()
                                            {
                                                Message = gatherUrlItem.Url + "获取下一页导航失败.",
                                                MessageType = EnumGloabParas.EnumMessageType.ERROR,
                                                ThreadName = ThreadName
                                            });
                                            //这里为了避免没有下一页,不计入ErrorCount
                                        }
                                    }
                                    if (gatherUrlItem.LevelUrlList.Count > gatherUrlItem.Level)
                                    {
                                        #region 获取下页导航数据
                                        string nextUrl = gatherUrlItem.LevelUrlList[gatherUrlItem.Level];
                                        List<string> levelUrls = GetNextLevelUrl(gatherUrlItem.Url, html, nextUrl);
                                        m_GatherTaskManage.OnLog(new cGatherEventArgs()
                                        {
                                            Message = gatherUrlItem.Url + "获取新的网页个数" + levelUrls.Count + ".",
                                            MessageType = EnumGloabParas.EnumMessageType.NOTIFY,
                                            ThreadName = ThreadName
                                        });
                                        if (levelUrls.Count == 0)
                                        {
                                            m_GatherTaskManage.OnLog(new cGatherEventArgs()
                                            {
                                                Message = html,
                                                MessageType = EnumGloabParas.EnumMessageType.INFO,
                                                ThreadName = ThreadName
                                            });
                                        }
                                        foreach (var levelUrl in levelUrls)
                                        {
                                            if (m_GatherTaskManage.ContainsUrl(levelUrl))
                                            {
                                                continue;
                                            }
                                            if (gatherUrlItem.LevelUrlList.Count > gatherUrlItem.Level + 1)
                                            {
                                                //这里始终加入到第一进程中,提前采集可用的网址,当无网址时 0线程将会采集Run的网页
                                                m_GatherTaskManage.runWebUrls[m_GatherTaskManage.ThreadPos].Enqueue(new cGatherUrlItem()
                                                {
                                                    GaterherFlag = EnumGloabParas.EnumUrlGaterherState.First,
                                                    LevelUrlList = gatherUrlItem.LevelUrlList,
                                                    NextPageText = "",//下一页标识只针对一级页面有效的
                                                    Url = levelUrl,
                                                    Level = gatherUrlItem.Level + 1,
                                                    StartPos = "",
                                                    EndPos = "",
                                                });
                                            }
                                            else
                                            {
                                                //平均分配网址到线程中采集
                                                m_GatherTaskManage.runWebUrls[m_GatherTaskManage.ThreadPos].Enqueue(new cGatherUrlItem()
                                                {
                                                    GaterherFlag = EnumGloabParas.EnumUrlGaterherState.Run,
                                                    LevelUrlList = gatherUrlItem.LevelUrlList,
                                                    NextPageText = gatherUrlItem.NextPageText,
                                                    Url = levelUrl,
                                                    Level = gatherUrlItem.Level + 1,
                                                    StartPos = m_GatherTaskManage.LastStartPos,
                                                    EndPos = m_GatherTaskManage.LastEndPos,
                                                });
                                                if (e_TotalCount != null)
                                                {
                                                    e_TotalCount(new cGatherCompletedEventArgs()
                                                    {
                                                        ErrorCount = 0,
                                                        TotalCount = 1,
                                                        TrueCount = 0,
                                                    });
                                                }
                                            }
                                        }
                                        #endregion
                                        if (e_CompleteCount != null)
                                        {
                                            e_CompleteCount(new cGatherCompleteCountEventArgs()
                                            {
                                                CompleteType = EnumGloabParas.EnumThreadCompleteType.Success,
                                                StartPos = gatherUrlItem.StartPos,
                                                EndPos = gatherUrlItem.EndPos,
                                                GaterherFlag = EnumGloabParas.EnumUrlGaterherState.Completed,
                                                Level = gatherUrlItem.Level,
                                                LevelUrlList = gatherUrlItem.LevelUrlList,
                                                NextPageText = gatherUrlItem.NextPageText,
                                                Url = gatherUrlItem.Url,
                                            });
                                        }
                                    }
                                    else
                                    {
                                        exitFlag = false;
                                        string errMsg = string.Empty;
                                        GatherWebUrl(gatherUrlItem, ref exitFlag, ref errMsg);
                                        if (exitFlag) { break; }
                                    }

                                    #endregion
                                }
                                #endregion
                            }
                        }
                        #endregion
                    }
                    else if (m_GatherTaskManage.runFileUrls[this.ThreadIndex].Count > 0)
                    {
                        #region 采集图片队列
                        lock ((m_GatherTaskManage.runFileUrls[this.ThreadIndex]).SyncRoot)
                        {
                            if (m_GatherTaskManage.runFileUrls[this.ThreadIndex].Count > 0)
                            {
                                if (m_GatherTaskManage.completeThreadList.Contains(this.ThreadName))
                                {
                                    m_GatherTaskManage.completeThreadList.Remove(this.ThreadName);
                                }
                                cGatherUrlBaseItem gatherFileItem = m_GatherTaskManage.runFileUrls[this.ThreadIndex].Dequeue();
                                this.DownFileQueue(gatherFileItem.Url);
                            }
                        }
                        #endregion

                    }
                    else
                    {
                        if (e_OnGatherNotityCompleted != null)
                        {
                            e_OnGatherNotityCompleted(ThreadName);
                        }
                    }

                }
                catch (Exception ex)
                {
                    string errMsg = "error:" + ex.Message + ex.Source + ex.StackTrace;
                    m_GatherTaskManage.OnLog(new cGatherEventArgs()
                    {
                        Message = errMsg,
                        MessageType = EnumGloabParas.EnumMessageType.ERROR,
                        ThreadName = ThreadName,
                    });
                    DMSFrame.Loggers.LoggerManager.FileLogger.LogWithTime(errMsg);
                }
                finally
                {
                    //Monitor.Exit(m_GatherTaskManage);
                }
                #endregion
            }

        }
        private BackgroundWorker threadWorker;
        private void GatherWebUrl(cGatherUrlItem gatherUrlItem, ref bool exitFlag, ref string errMsg)
        {
            exitFlag = false;

            m_GatherTaskManage.OnLog(new cGatherEventArgs()
            {
                Message = DateTime.Now.Ticks + " " + gatherUrlItem.Url + "正在采集中,请稍候...",
                MessageType = EnumGloabParas.EnumMessageType.INFO,
                ThreadName = ThreadName
            });

            string html = string.Empty;
            int tryCount = m_GatherTaskManage.TryAgainCount;
            while (true)
            {
                try
                {
                    html = this.GetHtml(gatherUrlItem.Url, m_GatherTaskManage.WebCookie, m_GatherTaskManage.WebEncode, gatherUrlItem.StartPos, gatherUrlItem.EndPos, m_GatherTaskManage.IsAjax);
                }
                catch (Exception ex)
                {
                    m_GatherTaskManage.OnLog(new cGatherEventArgs()
                    {
                        Message = DateTime.Now.Ticks + " " + gatherUrlItem.Url + "采集出现错误，重试中..." + ex.Message,
                        MessageType = EnumGloabParas.EnumMessageType.ERROR,
                        ThreadName = ThreadName
                    });
                    if (tryCount > 0)
                    {
                        tryCount--;
                        continue;
                    }
                }
                break;
            }
            m_GatherTaskManage.OnLog(new cGatherEventArgs()
            {
                Message = DateTime.Now.Ticks + " " + gatherUrlItem.Url + "正在采集中,请稍候...",
                MessageType = EnumGloabParas.EnumMessageType.INFO,
                ThreadName = ThreadName
            });
            DataTable tempData = m_GatherData.Clone();
            tempData = _getTempData.GetDataTable(tempData, m_GatherTaskManage.ColumnItemList, gatherUrlItem, m_GatherTaskManage.WebCookie, m_GatherTaskManage.WebEncode, m_GatherTaskManage.IsAjax, ref errMsg);
            if (tempData == null || tempData.Rows.Count == 0 || !string.IsNullOrEmpty(errMsg))
            {
                m_GatherTaskManage.OnLog(new cGatherEventArgs()
                {
                    Message = gatherUrlItem.Url + "没有数据，也有可能是垃圾数据导致...",
                    MessageType = EnumGloabParas.EnumMessageType.ERROR,
                    ThreadName = ThreadName
                });
                if (e_CompleteCount != null)
                {
                    e_CompleteCount(new cGatherCompleteCountEventArgs()
                    {
                        CompleteType = EnumGloabParas.EnumThreadCompleteType.Error,
                        StartPos = gatherUrlItem.StartPos,
                        EndPos = gatherUrlItem.EndPos,
                        GaterherFlag = EnumGloabParas.EnumUrlGaterherState.Error,
                        Level = gatherUrlItem.Level,
                        LevelUrlList = gatherUrlItem.LevelUrlList,
                        NextPageText = gatherUrlItem.NextPageText,
                        Url = gatherUrlItem.Url,
                    });
                }
                return;
            }
            if (m_GatherTaskManage.DownFileFlag)
            {
                DownFileQueue(gatherUrlItem, tempData);
            }
            else
            {
                //直接下载图片
            }
            lock (tempData)
            {
                m_GatherData.Merge(tempData);
                if (e_OnGatherDataCompleted != null)
                {
                    e_OnGatherDataCompleted(new cGatherDataEventArgs()
                    {
                        TaskID = m_GatherTaskManage.TaskID,
                        dataTable = tempData,
                        ErrorCount = m_GatherTaskManage.ErrorCount,
                        TotalCount = m_GatherTaskManage.TotalCount,
                        TrueCount = m_GatherTaskManage.TrueCount,
                    });
                }
            }
            m_GatherTaskManage.OnLog(new cGatherEventArgs()
            {
                Message = DateTime.Now.Ticks + " " + gatherUrlItem.Url + "采集完成...",
                MessageType = EnumGloabParas.EnumMessageType.INFO,
                ThreadName = ThreadName
            });
            if (e_CompleteCount != null)
            {
                e_CompleteCount(new cGatherCompleteCountEventArgs()
                {
                    CompleteType = EnumGloabParas.EnumThreadCompleteType.Success,
                    StartPos = gatherUrlItem.StartPos,
                    EndPos = gatherUrlItem.EndPos,
                    GaterherFlag = EnumGloabParas.EnumUrlGaterherState.Completed,
                    Level = gatherUrlItem.Level,
                    LevelUrlList = gatherUrlItem.LevelUrlList,
                    NextPageText = gatherUrlItem.NextPageText,
                    Url = gatherUrlItem.Url,
                });
            }
        }
        private void DownFileQueue(cGatherUrlItem item, DataTable tempData)
        {
            List<TaskColumnItem> taskList = m_GatherTaskManage.ColumnItemList.Where(q => q.DataFileType != EnumGloabParas.EnumDataFileType.Text).ToList();
            foreach (DataRow dr in tempData.Rows)
            {
                foreach (TaskColumnItem column in taskList)
                {
                    #region 下载文件
                    string value = dr[column.DataTextType.Value].ToString();
                    string url = this.GetNextUrl(value, item.Url);
                    if (m_GatherTaskManage.DownFileQueue)
                    {
                        if (!m_GatherTaskManage.ContainsDownFileUrl(url))
                        {
                            m_GatherTaskManage.runFileUrls[m_GatherTaskManage.ThreadPos].Enqueue(new cGatherUrlBaseItem()
                            {
                                Url = url,
                            });
                            if (e_TotalCount != null)
                            {
                                e_TotalCount(new cGatherCompletedEventArgs()
                                {
                                    ErrorCount = 0,
                                    TotalCount = 1,
                                    TrueCount = 0,
                                });
                            }
                        }
                    }
                    else
                    {
                        //直接下载文件
                        if (e_TotalCount != null)
                        {
                            e_TotalCount(new cGatherCompletedEventArgs()
                            {
                                ErrorCount = 0,
                                GaterherState = EnumGloabParas.EnumThreadState.Run,
                                TotalCount = 1,
                                TrueCount = 0,
                            });
                        }
                        this.DownFileQueue(url);
                    }
                    #endregion
                }
            }
        }
        private void DownFileQueue(string url)
        {
            EnumGloabParas.EnumDownloadResult downloadResult = EnumGloabParas.EnumDownloadResult.Succeed;
            int tryCount = m_GatherTaskManage.TryAgainCount;
            while (true)
            {
                try
                {
                    if (url.IndexOf("jpg") != -1 || url.IndexOf("bmp") != -1 || url.IndexOf("gif") != -1 || url.IndexOf("jpeg") != -1)
                    {
                        downloadResult = this.DownloadImage(url, m_GatherTaskManage.DownFilePath, m_GatherTaskManage.TaskTempName);
                    }
                    else
                    {
                        downloadResult = this.DownloadFile(url, m_GatherTaskManage.DownFilePath, m_GatherTaskManage.TaskTempName);
                    }
                }
                catch (Exception ex)
                {
                    m_GatherTaskManage.OnLog(new cGatherEventArgs()
                    {
                        Message = DateTime.Now.Ticks + " " + url + "下载图片错误，重试中." + ex.Message,
                        MessageType = EnumGloabParas.EnumMessageType.ERROR,
                        ThreadName = ThreadName
                    });
                    if (tryCount > 0)
                    {
                        tryCount--;
                        continue;
                    }
                    downloadResult = EnumGloabParas.EnumDownloadResult.Err;
                }
                break;
            }
            if (e_CompleteCount != null)
            {
                #region MyRegion
                if (downloadResult == EnumGloabParas.EnumDownloadResult.Succeed)
                {
                    e_CompleteCount(new cGatherCompleteCountEventArgs()
                    {
                        CompleteType = EnumGloabParas.EnumThreadCompleteType.FileSuccess,
                        StartPos = "",
                        EndPos = "",
                        GaterherFlag = EnumGloabParas.EnumUrlGaterherState.FileCompleted,
                        Level = 0,
                        LevelUrlList = new List<string>(),
                        NextPageText = "",
                        Url = url,
                    });
                    string errMsg = url + "下载完成...";
                    m_GatherTaskManage.OnLog(new cGatherEventArgs()
                    {
                        Message = errMsg,
                        MessageType = EnumGloabParas.EnumMessageType.INFO,
                        ThreadName = this.ThreadName
                    });
                }
                else
                {

                    e_CompleteCount(new cGatherCompleteCountEventArgs()
                    {
                        CompleteType = EnumGloabParas.EnumThreadCompleteType.FileError,
                        StartPos = "",
                        EndPos = "",
                        GaterherFlag = EnumGloabParas.EnumUrlGaterherState.FileError,
                        Level = 0,
                        LevelUrlList = new List<string>(),
                        NextPageText = "",
                        Url = url,
                    });
                    string errMsg = url + "下载失败...";
                    m_GatherTaskManage.OnLog(new cGatherEventArgs()
                    {
                        Message = errMsg,
                        MessageType = EnumGloabParas.EnumMessageType.ERROR,
                        ThreadName = this.ThreadName
                    });
                }
                #endregion
            }
        }
        private string ReplaceHtml(string str)
        {
            string m_outstr = str;
            System.Text.RegularExpressions.Regex objReg = new System.Text.RegularExpressions.Regex("(<[^>]+?>)|&nbsp;", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            m_outstr = objReg.Replace(m_outstr, "");
            System.Text.RegularExpressions.Regex objReg2 = new System.Text.RegularExpressions.Regex("(\\s)+", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            m_outstr = objReg2.Replace(m_outstr, " ");
            return m_outstr;
        }
        /// <summary>
        /// 
        /// </summary>
        public event OnGatherTotalCount e_TotalCount;
        /// <summary>
        /// 采集完成后触发事件
        /// </summary>
        public event OnGatherCompleteCount e_CompleteCount;
        /// <summary>
        /// 
        /// </summary>
        public event OnGatherNotityCompleted e_OnGatherNotityCompleted;
        /// <summary>
        /// 
        /// </summary>
        public event OnGatherDataCompleted e_OnGatherDataCompleted;
    }
}
