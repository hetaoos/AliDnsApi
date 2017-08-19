﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AliDns.DomainRecord
{
    /// <summary>
    /// 添加解析记录请求参数
    /// </summary>
    /// <seealso cref="AliDns.IAliDnsApiParam" />
    public class AddDomainRecordParam : IAliDnsApiParam
    {
        /// <summary>
        /// 操作接口名，系统规定参数
        /// </summary>
        public string Action => "AddDomainRecord";

        /// <summary>
        /// 域名名称
        /// </summary>
        public string DomainName { get; set; }
        /// <summary>
        /// 主机记录，如果要解析@.exmaple.com，主机记录要填写"@”，而不是空
        /// </summary>
        public string RR { get; set; }
        /// <summary>
        /// 记录类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 记录值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 生存时间，默认为600秒（10分钟）
        /// </summary>
        public int? TTL { get; set; }
        /// <summary>
        /// MX记录的优先级，取值范围[1,10]，记录类型为MX记录时，此参数必须
        /// </summary>
        public int? Priority { get; set; }
        /// <summary>
        /// 解析线路，默认为default
        /// </summary>
        public string Line { get; set; }

    }
}
