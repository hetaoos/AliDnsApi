using System;
using System.Collections.Generic;
using System.Text;

namespace AliDns.DomainRecord
{
    /// <summary>
    /// 删除主机记录对应的解析记录请求参数
    /// </summary>
    /// <seealso cref="AliDns.IAliDnsApiParam" />
    public class DeleteSubDomainRecordsParam : IAliDnsApiParam
    {
        /// <summary>
        /// 操作接口名，系统规定参数
        /// </summary>
        public string Action => "DeleteSubDomainRecords";

        /// <summary>
        /// 域名名称
        /// </summary>
        public string DomainName { get; set; }


        /// <summary>
        /// 主机记录，如果要解析@.exmaple.com，主机记录要填写"@”，而不是空
        /// </summary>
        public string RR { get; set; }

        /// <summary>
        /// 如果不填写，则返回子域名对应的全部解析记录类型。
        /// </summary>
        public string Type { get; set; }
    }
}
