using System.Collections.Generic;

namespace AliDns
{
    /// <summary>
    /// 返回结果
    /// </summary>
    public class IAliDnsApiResult
    {
        /// <summary>
        /// 唯一识别码
        /// </summary>
        public string RequestId { get; set; }
    }

    /// <summary>
    /// 分页返回结果
    /// </summary>
    public class IAliDnsApiPageResult : IAliDnsApiResult
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 解析记录总数
        /// </summary>
        public int TotalCount { get; set; }
    }
}
