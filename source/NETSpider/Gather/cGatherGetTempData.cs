using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NETSpider.Entity;
using System.Text.RegularExpressions;
using System.Data;

namespace NETSpider.Gather
{
    public delegate void OnEnqueueUrls(string url, string nextPageText, List<string> levelUrlList, string startPos, string endPos);


    public class cGatherGetTempData : cGatherTaskThreadBase
    {
        public List<cGatherUrlItem> runWebUrls = new List<cGatherUrlItem>();
        public bool _GatherUrlItemSingleFlag = false;
        public bool GatherUrlItemSingleFlag
        {
            get { return _GatherUrlItemSingleFlag; }
            set { _GatherUrlItemSingleFlag = value; }
        }
        public cGatherGetTempData()
        {
            runWebUrls = new List<cGatherUrlItem>();
        }
        public DataTable GetDataTable(DataTable dataTable, List<TaskColumnItem> columnItemList, string html, ref string errMsg)
        {
            try
            {
                #region MyRegion
                string strCut = string.Empty;
                bool newTable = false;
                if (dataTable == null)
                {
                    dataTable = new DataTable();
                    newTable = true;
                }
                foreach (var temp in columnItemList)
                {
                    if (newTable)
                    {
                        dataTable.Columns.Add(temp.DataTextType.Value, typeof(string));
                    }
                    strCut += GetRegString(temp) + "|";
                }
                strCut = strCut.Substring(0, strCut.Length - 1);
                #endregion
                Regex re = new Regex(@strCut, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                MatchCollection mc = re.Matches(html);
                if (mc.Count == 0)
                {
                    return dataTable;
                }
                #region MyRegion
                int rows = 0; //统计共采集了多少行
                int m = 0;   //计数使用
                DataRow drNew;
                try
                {

                    while (m < mc.Count)
                    {
                        //新建新行
                        drNew = dataTable.NewRow();
                        rows++;
                        for (int i = 0; i < columnItemList.Count; i++)
                        {
                            #region columnItemList
                            TaskColumnItem item = columnItemList[i];
                            if (m < mc.Count)
                            {

                                if (i == 0)
                                {
                                    string tempValue = mc[m].Value;
                                    while (!tempValue.StartsWith(item.StartPos.Value, StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        int index = tempValue.IndexOf(item.StartPos.Value);
                                        if (index != -1)
                                        {
                                            tempValue = tempValue.Substring(index);
                                            continue;
                                        }
                                        m++;
                                        if (m >= mc.Count)
                                        {
                                            //退出所有循环
                                            goto ExitWhile;
                                        }
                                    }
                                    string value = tempValue.Substring(item.StartPos.Value.Length, tempValue.Length - item.StartPos.Value.Length);
                                    if (value.IndexOf(item.EndPos.Value) != -1)
                                    {
                                        value = value.Substring(0, value.IndexOf(item.EndPos.Value));
                                    }
                                    drNew[i] = this.GetExportLimitColumnValue(item, value);
                                    m++;
                                }
                                else
                                {
                                    string tempValue = mc[m].Value;
                                    #region MyRegion
                                    if (tempValue.StartsWith(item.StartPos.Value, StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        string value = tempValue.Substring(item.StartPos.Value.Length, tempValue.Length - item.StartPos.Value.Length);
                                        drNew[i] = GetExportLimitColumnValue(item, value);
                                        m++;
                                    }
                                    else
                                    {
                                        if (tempValue.StartsWith(item.StartPos.Value, StringComparison.CurrentCultureIgnoreCase))
                                        {
                                            m++; i--;
                                        }
                                        else
                                        {
                                            if (i < columnItemList.Count - 1)
                                            {
                                                if (!tempValue.StartsWith(columnItemList[i + 1].StartPos.Value, StringComparison.CurrentCultureIgnoreCase))
                                                {
                                                    m++; i--;
                                                }
                                            }
                                            else
                                            {
                                                m++; i--;
                                            }
                                        }
                                    }
                                    #endregion
                                }
                            }
                            #endregion
                        }
                        dataTable.Rows.Add(drNew);
                        drNew = null;

                    }
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
                #endregion
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
        ExitWhile:
            return dataTable;
        }
        public DataTable GetDataTable(DataTable dataTable, List<TaskColumnItem> columnItemList, cGatherUrlItem item, string webCookie, EnumGloabParas.EnumEncodeType webEncode, bool isAjax, ref string errMsg)
        {
            try
            {
                string html = this.GetHtml(item.Url, webCookie, webEncode, item.StartPos, item.EndPos, isAjax);
                if (string.IsNullOrEmpty(html))
                {
                    errMsg = "网页获取为空,有可能是采集范围设置错误或网络错误";
                    return null;
                }
                dataTable = this.GetDataTable(dataTable, columnItemList, html, ref errMsg);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dataTable;
        }


        public string GetNextPage(string url, string html, string NextPageText)
        {
            string NRule = "((?<=href=[\'|\"])\\S[^#+$<>\\s]*(?=[\'|\"]))[^<]*(?<=" + NextPageText + ")";
            Match charSetMatch = Regex.Match(html, NRule, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string strNext = charSetMatch.Groups[1].Value;
            if (!string.IsNullOrEmpty(strNext))
            {
                string nextUrl = this.GetNextUrl(strNext, url);
                return nextUrl;
            }
            return string.Empty;
        }

        public string GetMatchUrl(string url, string nextPageText, List<string> levelUrlList, string startPos, string endPos, OnEnqueueUrls enqueueUrl)
        {
            Match m = Regex.Match(url, RegexString.Regex01, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            if (m.Success)
            {
                GetUrlNumber(m, ref url, nextPageText, levelUrlList, startPos, endPos, enqueueUrl);
            }
            m = Regex.Match(url, RegexString.Regex02, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            if (m.Success)
            {
                GetUrlLetter(m, ref url, nextPageText, levelUrlList, startPos, endPos, enqueueUrl);
            }
            m = Regex.Match(url, RegexString.Regex03, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            if (m.Success)
            {
                GetUrlLetter(m, ref url, nextPageText, levelUrlList, startPos, endPos, enqueueUrl);
            }
            m = Regex.Match(url, RegexString.Regex04, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            if (m.Success)
            {
                GetUrlDict(m, ref url, nextPageText, levelUrlList, startPos, endPos, enqueueUrl);
            }
            return url;
        }


        public void ThreadGetMainUrlsWork(TaskItem m_TaskItemEntity)
        {
            foreach (var item in m_TaskItemEntity.UrlList)
            {
                string url = item.MainUrl.Value;
                string nextPageText = item.NextPageFlag ? item.NextPageText.Value : "";
                List<string> levelUrlList = item.LevelUrlList.Select(q => q.LevelUrl).ToList();
                string tempUrl = this.GetMatchUrl(url, nextPageText, levelUrlList, "", "", this.EnqueueUrls);
                this.EnqueueUrls(tempUrl, nextPageText, levelUrlList, "", "");
                if (this._GatherUrlItemSingleFlag) { break; }
            }
        }


        #region private protites
        private static void GetDataRowValue(List<TaskColumnItem> columnItemList, ref int columnIndex, ref string value, ref string tempValue, DataRow dr, ref int tmpIndex)
        {
            while (columnIndex < columnItemList.Count)
            {
                var temp = columnItemList[columnIndex];
                bool startFlag = value.StartsWith(temp.StartPos.Value, StringComparison.CurrentCultureIgnoreCase);
                if (startFlag)
                {
                    string itemValue = value.Substring(temp.StartPos.Value.Length, value.Length - temp.StartPos.Value.Length);
                    if (itemValue.IndexOf(temp.EndPos.Value) != -1)
                    {
                        itemValue = itemValue.Substring(0, itemValue.IndexOf(temp.EndPos.Value));
                    }
                    dr[columnIndex] = itemValue;
                    columnIndex++;
                    if (value.Length > temp.StartPos.Value.Length + itemValue.Length + temp.EndPos.Value.Length)
                        value = value.Substring(temp.StartPos.Value.Length + itemValue.Length + temp.EndPos.Value.Length);
                }
                else
                {
                    if (value.IndexOf(temp.StartPos.Value) != -1)
                    {
                        value = value.Substring(value.IndexOf(temp.StartPos.Value));
                    }
                    else
                    {
                        //查找不到当前列的数据,获取下一列数据
                        dr[columnIndex] = "";
                        columnIndex++;
                    }
                }
            }
        }


        private string GetExportLimitColumnValue(TaskColumnItem temp, string columValue)
        {
            switch (temp.ExportLimit)
            {
                case EnumGloabParas.EnumExportLimit.ExportLimit1:
                    break;
                case EnumGloabParas.EnumExportLimit.ExportLimit2:
                    columValue = ReplaceHtml(columValue).Trim();
                    break;
                case EnumGloabParas.EnumExportLimit.ExportLimit3:
                    break;
                case EnumGloabParas.EnumExportLimit.ExportLimit4:
                    break;
                case EnumGloabParas.EnumExportLimit.ExportLimit5:
                    columValue = columValue.Trim();
                    break;
                case EnumGloabParas.EnumExportLimit.ExportLimit6:
                    columValue = temp.ExportLimitText + columValue.Trim();
                    break;
                case EnumGloabParas.EnumExportLimit.ExportLimit7:
                    columValue = columValue.Trim() + temp.ExportLimitText;
                    break;
                case EnumGloabParas.EnumExportLimit.ExportLimit8:
                    string[] itemList = temp.ExportLimitText.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (var i = 0; i < itemList.Length; i += 2)
                    {
                        if (itemList.Length > i + 1)
                        {
                            string st1 = itemList[i].TrimStart('"').TrimEnd('"');
                            string st2 = itemList[i + 1].TrimStart('"').TrimEnd('"');
                            columValue = columValue.Replace(st1, st2);
                        }
                    }
                    break;
                case EnumGloabParas.EnumExportLimit.ExportLimit9:
                    break;
                default:
                    break;
            }
            if (temp.ExportLimitSpaceFlag)
            {
                columValue = columValue.Trim();
            }
            return columValue;
        }

        private string GetRegString(TaskColumnItem temp)
        {
            string strCut = string.Empty;
            strCut += "(?<" + temp.DataTextType.Value + ">" + RegexString.RegexReplaceTrans(temp.StartPos.Value) + ")";
            switch (temp.LimitSign)
            {
                case EnumGloabParas.EnumLimitSign.LimitSign1:
                    strCut += ".*?";
                    break;
                case EnumGloabParas.EnumLimitSign.LimitSign2:
                    strCut += "[^<>]*?";
                    break;
                case EnumGloabParas.EnumLimitSign.LimitSign3:
                    strCut += "[\\u4e00-\\u9fa5]*?";
                    break;
                case EnumGloabParas.EnumLimitSign.LimitSign4:
                    strCut += "[^\\x00-\\xff]*?";
                    break;
                case EnumGloabParas.EnumLimitSign.LimitSign5:
                    strCut += "[\\d]*?";
                    break;
                case EnumGloabParas.EnumLimitSign.LimitSign6:
                    strCut += "[\\x00-\\xff]*?";
                    break;
                case EnumGloabParas.EnumLimitSign.LimitSign7:
                    strCut += temp.LimitSignText.ToString();
                    break;
                default:
                    strCut += "[\\S\\s]*?";
                    break;
            }
            strCut += "(?=" + RegexString.RegexReplaceTrans(temp.EndPos.Value) + ")";
            return strCut;
        }

        private string ReplaceHtml(string str)
        {
            string m_outstr = str;
            System.Text.RegularExpressions.Regex objReg0 = new System.Text.RegularExpressions.Regex("&nbsp;", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            m_outstr = objReg0.Replace(m_outstr, " ");
            System.Text.RegularExpressions.Regex objReg1 = new System.Text.RegularExpressions.Regex("(<[^>]+?>)", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            m_outstr = objReg1.Replace(m_outstr, "");
            System.Text.RegularExpressions.Regex objReg2 = new System.Text.RegularExpressions.Regex("(\\s)+", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            m_outstr = objReg2.Replace(m_outstr, " ");
            return m_outstr;
        }
        private void GetUrlNumber(Match m, ref string url, string nextPageText, List<string> levelUrlList, string startPos, string endPos, OnEnqueueUrls enqueueUrl)
        {
            #region 取大括号内的数字分页数据
            string[] str = m.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (str.Length == 3)
            {
                int[] pageItem = Array.ConvertAll<string, int>(str, q => DMSFrame.TryParse.StrToInt(q));
                string tempUrl0 = url.Substring(0, m.Index - 1);
                int lastIndex = m.Index + m.Length + 1;
                string tempUrl1 = string.Empty;
                if (url.Length > lastIndex)
                {
                    tempUrl1 = url.Substring(lastIndex);
                }
                if (pageItem[0] < pageItem[1] && pageItem[2] <= pageItem[1] && pageItem[2] > 0)//递增
                {
                    for (int i = pageItem[0]; i <= pageItem[1]; i += pageItem[2])
                    {
                        string tempUrl = tempUrl0 + i + tempUrl1;

                        url = GetMatchUrl(tempUrl, nextPageText, levelUrlList, startPos, endPos, enqueueUrl);
                        if (_GatherUrlItemSingleFlag) { break; }
                        enqueueUrl(url, nextPageText, levelUrlList, startPos, endPos);
                    }
                }
                else if (pageItem[0] >= pageItem[1] && pageItem[0] > pageItem[2] && pageItem[2] < 0)//递减
                {
                    for (int i = pageItem[0]; i >= pageItem[1]; i += pageItem[2])
                    {
                        string tempUrl = tempUrl0 + i + tempUrl1;
                        url = GetMatchUrl(tempUrl, nextPageText, levelUrlList, startPos, endPos, enqueueUrl);
                        if (_GatherUrlItemSingleFlag) { break; }
                        if (i == pageItem[1]) { break; }
                        enqueueUrl(url, nextPageText, levelUrlList, startPos, endPos);
                    }
                }
            }
            #endregion
        }

        private void GetUrlLetter(Match m, ref string url, string nextPageText, List<string> levelUrlList, string startPos, string endPos, OnEnqueueUrls enqueueUrl)
        {
            string[] str = m.Value.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            #region 取大括号内的字典数据
            if (str.Length == 2)
            {
                #region 取大括号内的字母数据
                string tempCode = str[0];
                string tempUrl0 = url.Substring(0, m.Index - 1);
                int lastIndex = m.Index + m.Length + 1;
                string tempUrl1 = string.Empty;
                if (url.Length > lastIndex)
                {
                    tempUrl1 = url.Substring(lastIndex);
                }
                string tempUrl = tempUrl0 + tempCode + tempUrl1;
                url = GetMatchUrl(tempUrl, nextPageText, levelUrlList, startPos, endPos, enqueueUrl);
                if (_GatherUrlItemSingleFlag) { return; }
                enqueueUrl(url, nextPageText, levelUrlList, startPos, endPos);
                char start = tempCode[0];
                char end = str[1][0];
                int value = start < end ? 1 : -1;
                while (true)
                {
                    char cb = Convert.ToChar(start + value);
                    tempUrl = tempUrl0 + cb + tempUrl1;
                    url = GetMatchUrl(tempUrl, nextPageText, levelUrlList, startPos, endPos, enqueueUrl);
                    if (_GatherUrlItemSingleFlag) { break; }
                    if (cb == end) { break; }
                    enqueueUrl(url, nextPageText, levelUrlList, startPos, endPos);

                    start = cb;
                }
                #endregion
            }
            #endregion
        }

        private void GetUrlDict(Match m, ref string url, string nextPageText, List<string> levelUrlList, string startPos, string endPos, OnEnqueueUrls enqueueUrl)
        {
            string str = m.Value.Substring(5);//去除dict:
            if (!string.IsNullOrEmpty(str))
            {
                string errMsg = string.Empty;
                DictList dictList = XmlHelper.LoadFromXml<DictList>(Program.GetConfigPath(@"dict.xml"), ref errMsg);
                Dict dictEntity = dictList.Where(q => q.CategoryName.Value == str).FirstOrDefault();
                if (dictEntity.DictItemList == null || dictEntity.DictItemList.Count == 0)
                {
                    string tempUrl0 = url.Substring(0, m.Index - 1);
                    string tempCode = "";
                    string tempUrl1 = string.Empty;
                    int lastIndex = m.Index + m.Length + 1;
                    if (url.Length > lastIndex)
                    {
                        tempUrl1 = url.Substring(lastIndex);
                    }
                    string tempUrl = tempUrl0 + tempCode + tempUrl1;
                    url = GetMatchUrl(tempUrl, nextPageText, levelUrlList, startPos, endPos, enqueueUrl);
                    if (_GatherUrlItemSingleFlag) { return; }
                    enqueueUrl(url, nextPageText, levelUrlList, startPos, endPos);
                }
                else
                {
                    string tempUrl0 = url.Substring(0, m.Index - 1);
                    string tempUrl1 = string.Empty;
                    int index = 1;
                    foreach (var item in dictEntity.DictItemList)
                    {                       
                        string tempCode = item.DictName.Value;                      
                        int lastIndex = m.Index + m.Length + 1;
                        if (url.Length > lastIndex)
                        {
                            tempUrl1 = url.Substring(lastIndex);
                        }
                        string tempUrl = tempUrl0 + tempCode + tempUrl1;
                        url = GetMatchUrl(tempUrl, nextPageText, levelUrlList, startPos, endPos, enqueueUrl);
                        if (_GatherUrlItemSingleFlag) { return; }
                        if (index == dictEntity.DictItemList.Count)
                        {
                            break;
                        }
                        enqueueUrl(url, nextPageText, levelUrlList, startPos, endPos);
                        index++;
                    }
                }
            }
        }
        private void EnqueueUrls(string url, string nextPageText, List<string> levelUrlList, string startPos, string endPos)
        {
            EnumGloabParas.EnumUrlGaterherState gaterherFlag = levelUrlList.Count > 0 ? EnumGloabParas.EnumUrlGaterherState.First : EnumGloabParas.EnumUrlGaterherState.Run;
            runWebUrls.Add(new cGatherUrlItem()
            {
                Url = url,
                GaterherFlag = gaterherFlag,
                Level = 0,
                LevelUrlList = levelUrlList,
                NextPageText = nextPageText,
                StartPos = startPos,
                EndPos = endPos,
            });
        }

        #endregion
    }
}
