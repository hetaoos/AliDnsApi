using System;
using System.Collections.Generic;
using System.Text;

namespace AliDns.DomainRecord
{
    /// <summary>
    /// 删除主机记录对应的解析记录
    /// </summary>
    /// <seealso cref="AliDns.DomainRecord.DefaultDomainRecordResult" />
    public class DeleteSubDomainRecordsResult : DefaultDomainRecordResult
    {

        /// <summary>
        /// 主机记录，如果要解析@.exmaple.com，主机记录要填写"@”，而不是空
        /// </summary>
        public string RR { get; set; }

        /// <summary>
        /// 被删除的解析记录总数
        /// </summary>
        public int TotalCount { get; set; }

    }
}
