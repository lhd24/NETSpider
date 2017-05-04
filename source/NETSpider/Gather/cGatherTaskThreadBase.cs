using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NETSpider.Entity;
using System.Net;
using System.Text.RegularExpressions;
using System.IO.Compression;
using System.IO;

namespace NETSpider.Gather
{
    public class cGatherTaskThreadBase
    {
        HttpWebRequest wReq;

        public void Load()
        {
            wReq.Proxy = null;
            wReq.Credentials = CredentialCache.DefaultCredentials;
            //设置页面超时时间为12秒
            wReq.Timeout = 12000;
            wReq.KeepAlive = true;
            wReq.Accept = "*/*";
        }
        public string GetHtml(string url, string cookie, EnumGloabParas.EnumEncodeType webCode, string startPos, string endPos, bool IsAjax)
        {
            Encoding wCode;
            string PostPara;
            CookieContainer CookieCon = new CookieContainer();
            if (Regex.IsMatch(url, @"<POST>.*</POST>", RegexOptions.IgnoreCase))
                wReq = (HttpWebRequest)WebRequest.Create(@url.Substring(0, url.IndexOf("<POST>")));
            else
            {
                Uri uri = new Uri(url);
                wReq = (HttpWebRequest)WebRequest.Create(uri);
            }
            Match a = Regex.Match(url, @"(http://).[^/]*[?=/]", RegexOptions.IgnoreCase);
            string url1 = a.Groups[0].Value.ToString();
            wReq.Referer = url1;
            if (cookie != "")
            {
                CookieCollection cl = GetWebCookies(cookie);
                CookieCon.Add(new Uri(url), cl);
                wReq.CookieContainer = CookieCon;
            }
            if (Regex.IsMatch(url, @"(?<=<POST>)[\S\s]*(?=</POST>)", RegexOptions.IgnoreCase))
            {
                Match s = Regex.Match(url, @"(?<=<POST>).*(?=</POST>)", RegexOptions.IgnoreCase);
                PostPara = s.Groups[0].Value.ToString();
                byte[] pPara = Encoding.ASCII.GetBytes(PostPara);
                wReq.ContentType = "application/x-www-form-urlencoded";
                wReq.ContentLength = pPara.Length;
                wReq.Method = "POST";
                System.IO.Stream reqStream = wReq.GetRequestStream();
                reqStream.Write(pPara, 0, pPara.Length);
                reqStream.Close();

            }
            else
            {
                wReq.Method = "GET";
                wReq.ContentType = "text/html";
            }
            this.Load();
            HttpWebResponse wResp = GetResponse(wReq, 0, null);
            System.IO.Stream respStream = wResp.GetResponseStream();
            string strWebData = "";
            switch (webCode)
            {
                case EnumGloabParas.EnumEncodeType.AUTO:
                    try
                    {
                        wCode = Encoding.Default;
                        string cType = wResp.ContentType.ToLower();
                        Match charSetMatch = Regex.Match(cType, "(?<=charset=)([^<]*)*", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string webCharSet = charSetMatch.ToString();
                        wCode = System.Text.Encoding.GetEncoding(webCharSet);
                    }
                    catch
                    {
                        wCode = Encoding.Default;
                    }
                    break;
                case EnumGloabParas.EnumEncodeType.GB2312:
                    wCode = Encoding.GetEncoding("gb2312");
                    break;
                case EnumGloabParas.EnumEncodeType.UTF8:
                    wCode = Encoding.UTF8;
                    break;
                case EnumGloabParas.EnumEncodeType.GBK:
                    wCode = Encoding.GetEncoding("GBK");
                    break;
                default:
                    wCode = Encoding.UTF8;
                    break;
            }
            if (wResp.ContentEncoding == "gzip")
            {
                GZipStream myGZip = new GZipStream(respStream, CompressionMode.Decompress);
                System.IO.StreamReader reader;
                reader = new System.IO.StreamReader(myGZip, wCode);
                strWebData = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
            }
            else
            {
                System.IO.StreamReader reader;
                reader = new System.IO.StreamReader(respStream, wCode);
                strWebData = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
            }
            if (!string.IsNullOrEmpty(startPos) && !string.IsNullOrEmpty(endPos))
            {
                string Splitstr = "(" + startPos + ").*?(" + endPos + ")";
                Match aa = Regex.Match(strWebData, Splitstr, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                if (aa.Success)
                {
                    strWebData = aa.Groups[0].ToString();
                }
            }
            if (IsAjax == true)
            {
                strWebData = System.Web.HttpUtility.UrlDecode(strWebData, Encoding.UTF8);
            }
            wResp.Close();
            wReq.Abort();
            return strWebData;
        }


        List<string> userAgent = new List<string>()
        {
            "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; QQWubi 133; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; CIBA; InfoPath.2)",
            "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0.2062.103 Safari/537.36",
            "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.0; .NET CLR 1.1.4322; .NET CLR 2.0.50215;)",
            "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; SLCC1; .NET CLR 2.0.50727; .NET CLR 3.0.04506; .NET CLR 3.5.21022; .NET CLR 1.0.3705; .NET CLR 1.1.4322)",
        };
        private HttpWebResponse GetResponse(HttpWebRequest wReq, int count, Exception ex)
        {
            if (count + 1 == userAgent.Count)
            {
                throw ex;
            }
            HttpWebResponse wResp = null;
            try
            {
                wReq.UserAgent = userAgent[count];
                wResp = (HttpWebResponse)wReq.GetResponse();
            }
            catch (Exception ee)
            {
                return GetResponse(wReq, count + 1, ee);
            }
            return wResp;
        }
        private CookieCollection GetWebCookies(string cookie)
        {
            CookieCollection cl = new CookieCollection();
            foreach (string sc in cookie.Split(';'))
            {
                string ss = sc.Trim();
                if (ss.IndexOf("&") > 0)
                {
                    foreach (string s1 in ss.Split('&'))
                    {
                        string s2 = s1.Trim();
                        string s4 = s2.Substring(s2.IndexOf("=") + 1, s2.Length - s2.IndexOf("=") - 1);

                        cl.Add(new Cookie(s2.Split('=')[0].Trim(), s4, "/"));
                    }
                }
                else
                {
                    string s3 = sc.Trim();
                    cl.Add(new Cookie(s3.Split('=')[0].Trim(), s3.Split('=')[1].Trim(), "/"));
                }
            }
            return cl;
        }

        public List<string> GetNextLevelUrl(string parentUrl, string html, string urlRule)
        {
            ///category-<Regex:\S+>/goods-<Regex:\d+>.html
            List<string> resultUrls = new List<string>();
            string urlTempRule = "";

            if (urlRule.StartsWith("<Regex:"))
            {
                urlTempRule = @"(?<=[href=|src=|open(][\W])";
                //处理前缀
                string strPre = urlRule.Substring(urlRule.IndexOf("<Regex:") + 7, urlRule.IndexOf(">") - 7);
                urlTempRule += strPre;
                //处理中间内容
                string cma = @"(?<=<Common:)\S+?(?=>)";

                Regex cmas = new Regex(cma, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                MatchCollection cs = cmas.Matches(urlRule);
                foreach (Match ma in cs)
                {
                    urlTempRule += @"(\S*)" + ma.Value.ToString();
                }

                //处理后缀
                if (Regex.IsMatch(urlRule, "<End:"))
                {
                    string s = urlRule.Substring(urlRule.IndexOf("<End:") + 5, urlRule.Length - urlRule.IndexOf("<End:") - 6);
                    urlTempRule += @"(\S*)" + s;
                }
                else
                {
                    urlTempRule += @"(\S[^'"">]*)(?=[\s'""])";
                }

            }
            else
            {
                urlTempRule = @"(?<=[href=|src=|open(][\W])" + RegexString.RegexReplaceTrans(urlRule) + @"(\S[^'"">]*)(?=[\s'""])";
            }
            MatchCollection matchs = Regex.Matches(html, urlTempRule, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            foreach (Match item in matchs)
            {
                string url = GetNextUrl(item.Value, parentUrl);
                if (!resultUrls.Contains(url))
                {
                    resultUrls.Add(url);
                }
            }
            return resultUrls;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceUrl">获取的导航页面原始值</param>
        /// <param name="parentUrl">上级导航页面</param>
        /// <returns></returns>
        public string GetNextUrl(string sourceUrl, string parentUrl)
        {
            if (sourceUrl.Substring(0, 1) == "/")
            {
                string PreUrl = parentUrl;
                PreUrl = PreUrl.Substring(7, PreUrl.Length - 7);
                PreUrl = PreUrl.Substring(0, PreUrl.IndexOf("/"));
                PreUrl = "http://" + PreUrl;
                sourceUrl = PreUrl + sourceUrl;
            }
            else if (sourceUrl.StartsWith("http://", StringComparison.CurrentCultureIgnoreCase))
            {

            }
            else if (sourceUrl.StartsWith("?", StringComparison.CurrentCultureIgnoreCase))
            {
                Match aa = Regex.Match(parentUrl, @".*(?=\?)");
                if (aa.Success)
                {
                    string PreUrl = aa.Groups[0].Value.ToString();
                    sourceUrl = PreUrl + sourceUrl;
                }
                else
                {
                    sourceUrl = parentUrl + sourceUrl;
                }
            }
            else
            {
                Match aa = Regex.Match(parentUrl, ".*/");
                string PreUrl = aa.Groups[0].Value.ToString();
                sourceUrl = PreUrl + sourceUrl;
            }
            return sourceUrl;
        }

        public EnumGloabParas.EnumDownloadResult DownloadImage(string url, string downFilePath, string taskTempName)
        {
            string newFile = getNewFileName(url, downFilePath, taskTempName);
            if (string.IsNullOrEmpty(newFile))
            {
                return EnumGloabParas.EnumDownloadResult.PathErr;
            }
            return DownloadImage(url, newFile);
        }

        private string getNewFileName(string url, string downFilePath, string taskTempName)
        {
            if (url.IndexOf("/") == 1)
            {
                return string.Empty;
            }
            string fileName = url.Substring(url.LastIndexOf("/") + 1);
            string newFile = downFilePath + @"\" + taskTempName;
            if (!System.IO.Directory.Exists(newFile))
            {
                System.IO.Directory.CreateDirectory(newFile);
            }
            newFile += @"\" + fileName;
            return newFile;
        }
        //下载文件，是
        public EnumGloabParas.EnumDownloadResult DownloadImage(string url, string path)
        {
            WebClient mywebclient = new WebClient();
            try
            {
                if (!System.IO.File.Exists(path))
                {
                    mywebclient.DownloadFile(url, path);
                    mywebclient.Dispose();
                }
                return EnumGloabParas.EnumDownloadResult.Succeed;
            }
            catch (Exception)
            {
                return EnumGloabParas.EnumDownloadResult.Err;
            }
        }
        public EnumGloabParas.EnumDownloadResult DownloadFile(string url, string downFilePath, string taskTempName)
        {
            string newFile = getNewFileName(url, downFilePath, taskTempName);
            if (string.IsNullOrEmpty(newFile))
            {
                return EnumGloabParas.EnumDownloadResult.PathErr;
            }
            return DownloadFile(url, newFile);
        }
        public const int DEF_PACKET_LENGTH = 1024;
        //下载文件，是一个单线程的方法，适用于小文件下载，仅支持http方式
        public EnumGloabParas.EnumDownloadResult DownloadFile(string url, string path)
        {

            HttpWebRequest wReq = null;
            HttpWebResponse wRep = null;
            FileStream SaveFileStream = null;

            int startingPoint = 0;

            try
            {
                //For using untrusted SSL Certificates

                wReq = (HttpWebRequest)HttpWebRequest.Create(url);
                wReq.AddRange(startingPoint);

                wRep = (HttpWebResponse)wReq.GetResponse();
                Stream responseSteam = wRep.GetResponseStream();

                if (startingPoint == 0)
                {
                    SaveFileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                }
                else
                {
                    SaveFileStream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                }

                int bytesSize;
                long fileSize = wRep.ContentLength;
                byte[] downloadBuffer = new byte[DEF_PACKET_LENGTH];

                while ((bytesSize = responseSteam.Read(downloadBuffer, 0, downloadBuffer.Length)) > 0)
                {
                    SaveFileStream.Write(downloadBuffer, 0, bytesSize);
                }
                responseSteam.Dispose();
                SaveFileStream.Close();
                SaveFileStream.Dispose();
                wRep.Close();
                return EnumGloabParas.EnumDownloadResult.Succeed;
            }
            catch (System.Exception)
            {
                return EnumGloabParas.EnumDownloadResult.Err;
            }

        }
    }
}
