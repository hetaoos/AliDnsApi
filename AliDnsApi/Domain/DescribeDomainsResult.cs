using System;
using System.Collections.Generic;
using System.Text;

namespace AliDns.Domain
{
    /// <summary>
    /// 获取域名列表
    /// </summary>
    /// <seealso cref="AliDns.IAliDnsApiPageResult" />
    public class DescribeDomainsResult: IAliDnsApiPageResult
    {
        /// <summary>
        /// 域名列表
        /// </summary>
        public Domains Domains { get; set; }

    }
}
