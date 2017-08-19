using System;
using System.Collections.Generic;
using System.Text;

namespace AliDns
{
    /// <summary>
    /// DNS服务器列表
    /// </summary>
    public class DnsServers
    {
        /// <summary>
        /// DNS服务器名称
        /// </summary>
        public List<string> DnsServer { get; set; }

        public override string ToString()
        {
            if (DnsServer?.Count > 0)
                return string.Join(", ", DnsServer);
            return string.Empty;
        }
    }

    /// <summary>
    /// 状态列表
    /// </summary>
    public class StatusList
    {
        /// <summary>
        /// Status名称
        /// </summary>
        public List<string> Status { get; set; }

        public override string ToString()
        {
            if (Status?.Count > 0)
                return string.Join(", ", Status);
            return string.Empty;
        }
    }

    /// <summary>
    /// 域名
    /// </summary>
    public class DomainItem
    {
        /// <summary>
        /// 名在解析系统中的DNS列表
        /// </summary>
        public DnsServers DnsServers { get; set; }
        /// <summary>
        /// 域名ID
        /// </summary>
        public string DomainId { get; set; }
        /// <summary>
        /// 域名名称
        /// </summary>
        public string DomainName { get; set; }
        /// <summary>
        /// 中文域名的PunyCode码，英文域名返回为空
        /// </summary>
        public string PunyCode { get; set; }

        public override string ToString()
        {
            return DomainName;
        }
    }
    /// <summary>
    /// 域名列表
    /// </summary>
    public class Domains
    {
        /// <summary>
        /// 
        /// </summary>
        public List<DomainItem> Domain { get; set; }

        public override string ToString()
        {
            return $"Count: {Domain?.Count ?? 0}";
        }
    }
    /// <summary>
    /// 解析记录列表
    /// </summary>
    public class DomainRecords
    {
        /// <summary>
        /// 记录
        /// </summary>
        public List<RecordItem> Record { get; set; }

        public override string ToString()
        {
            return $"Count: {Record?.Count ?? 0}";
        }
    }

    /// <summary>
    /// Record结构表
    /// </summary>
    public class RecordItem
    {
        /// <summary>
        /// 域名名称
        /// </summary>
        public string DomainName { get; set; }
        /// <summary>
        /// 解析记录ID
        /// </summary>
        public string RecordId { get; set; }
        /// <summary>
        /// 主机记录
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
        /// 生存时间
        /// </summary>
        public int TTL { get; set; }
        /// <summary>
        /// MX记录的优先级
        /// </summary>
        public int? Priority { get; set; }
        /// <summary>
        /// 解析线路
        /// </summary>
        public string Line { get; set; }
        /// <summary>
        /// 解析记录状态，Enable/Disable
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 解析记录锁定状态，true/false
        /// </summary>
        public bool Locked { get; set; }

        public override string ToString()
        {
            return $"{RR}.{DomainName} {Type} {Value}";
        }
    }

    /// <summary>
    /// 域名操作日志
    /// </summary>
    public class DomainRecordLogs
    {
        /// <summary>
        /// 记录
        /// </summary>
        public List<RecordLogItem> RecordLog { get; set; }

        public override string ToString()
        {
            return $"Count: {RecordLog?.Count ?? 0}";
        }
    }

    /// <summary>
    /// 域名操作日志
    /// </summary>
    public class RecordLogItem
    {
        /// <summary>
        /// G操作时间
        /// </summary>
        public DateTime ActionTime { get; set; }
        /// <summary>
        /// 	操作行为
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 操作消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 操作者IP
        /// </summary>
        public string ClientIp { get; set; }

        public override string ToString()
        {
            return $"{Action} {Message}";
        }
    }
}
