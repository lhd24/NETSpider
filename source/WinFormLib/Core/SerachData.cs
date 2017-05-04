using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinFormLib.Core
{
    public class SerachData
    {
        /// <summary>
        /// 查询行数,默认50行
        /// </summary>
        public string TopCount { get; set; }
        /// <summary>
        /// 查询条件
        /// </summary>
        public string SearchResult { get; set; }
        /// <summary>
        /// 查询排序
        /// </summary>
        public string SearchOrder { get; set; }
        /// <summary>
        /// 已有行数
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// 查询结果后已有行数
        /// </summary>
        public int TotalCount { get; set; }

        #region ICloneable 成员

        public SerachData Clone()
        {
            return new SerachData()
            {
                TopCount = this.TopCount,
                SearchOrder = this.SearchOrder,
                SearchResult = this.SearchResult,
                RowCount = this.RowCount
            };
        }

        #endregion
    }
}
