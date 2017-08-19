using System;
using System.Collections.Generic;
using System.Text;

namespace AliDns.DomainRecord
{
    /// <summary>
    /// 修改解析记录请求参数
    /// </summary>
    /// <seealso cref="AliDns.AddDomainRecordParam" />
    public class UpdateDomainRecordParam : AddDomainRecordParam
    {
        /// <summary>
        /// 操作接口名，系统规定参数
        /// </summary>
        public override string Action => "UpdateDomainRecord";

        /// <summary>
        /// 解析记录的ID
        /// </summary>
        public string RecordId { get; set; }

    }
}
