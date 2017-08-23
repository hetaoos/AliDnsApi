using System;
using System.Collections.Generic;
using System.Text;

namespace AliDns.Domain
{
    /// <summary>
    /// 获取域名列表请求参数
    /// </summary>
    /// <seealso cref="AliDns.IAliDnsApiParam" />
    public class DescribeDomainsParam : IAliDnsApiParam
    {
        /// <summary>
        /// 操作接口名，系统规定参数
        /// </summary>
        public string Action => "DescribeDomains";

        /// <summary>
        /// 当前页数，起始值为1，默认为1
        /// </summary>
        public int PageNumber { get; set; } = 1;
        /// <summary>
        /// 分页查询时设置的每页行数，最大值500，默认为20
        /// </summary>
        public int PageSize { get; set; } = 20;
        /// <summary>
        /// 关键字，按照”%KeyWord%”模式搜索，不区分大小写
        /// </summary>
        public string KeyWord { get; set; }
        /// <summary>
        /// 记域名分组ID，如果不填写则默认为全部分组
        /// </summary>
        public string GroupId { get; set; }

    }
}
