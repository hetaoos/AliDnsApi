using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public int RefreshInterval { get; set; }

        /// <summary>
        /// 域名
        /// </summary>
        public List<string> Domains { get; set; }
    }
}
