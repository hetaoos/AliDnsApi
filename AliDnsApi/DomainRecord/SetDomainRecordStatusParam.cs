using System;
using System.Collections.Generic;
using System.Text;

namespace AliDns.DomainRecord
{
    /// <summary>
    /// 设置解析记录状态请求参数
    /// </summary>
    /// <seealso cref="AliDns.IAliDnsApiParam" />
    public class SetDomainRecordStatusParam : IAliDnsApiParam
    {
        /// <summary>
        /// 操作接口名，系统规定参数
        /// </summary>
        public string Action => "SetDomainRecordStatus";

        /// <summary>
        /// 解析记录的ID，此参数在添加解析时会返回，在获取域名解析列表时会返回
        /// </summary>
        public string RecordId { get; set; }


        /// <summary>
        /// Enable: 启用解析 Disable: 暂停解析
        /// </summary>
        public string Status { get; set; }
    }
}
