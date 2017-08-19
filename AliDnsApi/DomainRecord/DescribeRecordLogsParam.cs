using System;
using System.Collections.Generic;
using System.Text;

namespace AliDns.DomainRecord
{
    /// <summary>
    /// 获取域名的解析操作日志请求参数
    /// </summary>
    /// <seealso cref="AliDns.IAliDnsApiParam" />
    public class DescribeRecordLogsParam : IAliDnsApiParam
    {
        /// <summary>
        /// 操作接口名，系统规定参数
        /// </summary>
        public string Action => "DescribeRecordLogs";

        /// <summary>
        /// 域名名称
        /// </summary>
        public string DomainName { get; set; }
        /// <summary>
        /// 当前页数，起始值为1，默认为1
        /// </summary>
        public int PageNumber { get; set; } = 1;
        /// <summary>
        /// 分页查询时设置的每页行数，最大值100，默认为20
        /// </summary>
        public int PageSize { get; set; } = 20;
        /// <summary>
        /// 关键字，按照”%KeyWord%”模式搜索，不区分大小写
        /// </summary>
        public string KeyWord { get; set; }


    }
}
