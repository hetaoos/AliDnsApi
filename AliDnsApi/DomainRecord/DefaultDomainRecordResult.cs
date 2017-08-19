using System;
using System.Collections.Generic;
using System.Text;

namespace AliDns.DomainRecord
{
    /// <summary>
    /// 默认返回结果
    /// </summary>
    /// <seealso cref="AliDns.IAliDnsApiBaseResult" />
    public class DefaultDomainRecordResult : IAliDnsApiBaseResult
    {
        /// <summary>
        /// 解析记录的ID
        /// </summary>
        public string RecordId { get; set; }
        
    }
}
