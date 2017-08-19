using System;
using System.Collections.Generic;
using System.Text;

namespace AliDns.DomainRecord
{
    /// <summary>
    /// 解析记录列表
    /// </summary>
    /// <seealso cref="AliDns.IAliDnsApiPageResult" />
    public class DescribeDomainRecordsResult: IAliDnsApiPageResult
    {
        /// <summary>
        /// 解析记录列表
        /// </summary>
        public DomainRecords DomainRecords { get; set; }

    }
}
