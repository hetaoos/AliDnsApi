using System.Collections.Generic;
using System.Text;

namespace AliDnsUpdater
{
    /// <summary>
    /// 设置
    /// </summary>
    public class AliDnsSettings
    {
        /// <summary>
        /// AccessId
        /// </summary>
        public string AccessId { get; set; }

        /// <summary>
        /// AccessKey
        /// </summary>
        public string AccessKey { get; set; }

        /// <summary>
        /// 更新间隔，分钟
        /// </summary>
        public int Interval { get; set; } = 10;

        /// <summary>
        /// 域名
        /// </summary>
        public List<string> Domains { get; set; }

        /// <summary>
        /// 获取错误信息
        /// </summary>
        /// <returns></returns>
        public string GetError()
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrWhiteSpace(AccessId))
                sb.AppendLine($"The {nameof(AccessId)} value is invalid.");
            if (string.IsNullOrWhiteSpace(AccessKey))
                sb.AppendLine($"The {nameof(AccessKey)} value is invalid.");
            if (!(Domains?.Count > 0))
                sb.AppendLine($"The {nameof(Domains)} value is invalid.");
            return sb.ToString();
        }
    }
}