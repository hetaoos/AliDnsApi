using System;
using System.Collections.Generic;
using System.Text;

namespace AliDns.DomainRecord
{
    /// <summary>
    /// 添加解析记录结果
    /// </summary>
    /// <seealso cref="AliDns.IAliDnsApiResult" />
    public class AddDomainRecordResult : IAliDnsApiResult
    {
        /// <summary>
        /// 解析记录的ID
        /// </summary>
        public string RecordId { get; set; }



    }
}
