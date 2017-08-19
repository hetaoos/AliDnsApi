using System;
using System.Collections.Generic;
using System.Text;

namespace AliDns.DomainRecord
{
    /// <summary>
    /// 设置解析记录状态
    /// </summary>
    /// <seealso cref="AliDns.DomainRecord.DefaultDomainRecordResult" />
    public class DescribeDomainRecordInfoResult :  RecordItem, IAliDnsApiResult
    {

        /// <summary>
        /// 唯一识别码
        /// </summary>
        public string RequestId { get; set; }

    }
}
