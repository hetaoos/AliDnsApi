using System;
using System.Collections.Generic;
using System.Text;

namespace AliDns.DomainRecord
{
    /// <summary>
    /// 设置解析记录状态
    /// </summary>
    /// <seealso cref="AliDns.DomainRecord.DefaultDomainRecordResult" />
    public class SetDomainRecordStatusResult : DefaultDomainRecordResult
    {

        /// <summary>
        /// Enable: 启用解析 Disable: 暂停解析
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

    }
}
