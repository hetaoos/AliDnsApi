using System;
using System.Collections.Generic;
using System.Text;

namespace AliDns.DomainRecord
{
    /// <summary>
    /// 获取域名的解析操作日志
    /// </summary>
    /// <seealso cref="AliDns.DomainRecord.DefaultDomainRecordResult" />
    public class DescribeRecordLogsResult : IAliDnsApiPageResult
    {

        /// <summary>
        /// 域名操作日志列表
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public DomainRecordLogs RecordLogs { get; set; }

    }
}
