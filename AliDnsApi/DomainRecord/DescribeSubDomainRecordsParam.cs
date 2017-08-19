using System;
using System.Collections.Generic;
using System.Text;

namespace AliDns.DomainRecord
{
    /// <summary>
    /// 获取子域名的解析记录列表
    /// </summary>
    /// <seealso cref="AliDns.IAliDnsApiParam" />
    public class DescribeSubDomainRecordsParam : IAliDnsApiParam
    {
        /// <summary>
        /// 操作接口名，系统规定参数
        /// </summary>
        public string Action => "DescribeSubDomainRecords";

        /// <summary>
        /// 域名名称
        /// </summary>
        public string SubDomain { get; set; }
        /// <summary>
        /// 当前页数，起始值为1，默认为1
        /// </summary>
        public int PageNumber { get; set; } = 1;
        /// <summary>
        /// 分页查询时设置的每页行数，最大值500，默认为20
        /// </summary>
        public int PageSize { get; set; } = 20;
        /// <summary>
        /// 如果不填写，则返回子域名对应的全部解析记录类型。解析类型包括(不区分大小写)
        /// </summary>
        public string Type { get; set; }

    }
}
