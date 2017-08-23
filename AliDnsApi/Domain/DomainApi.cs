using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AliDns.Domain
{
    /// <summary>
    /// 域名管理管理
    /// </summary>
    public class DomainApi
    {
        public AliDnsApi api { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainApi"/> class.
        /// </summary>
        /// <param name="api">The API.</param>
        public DomainApi(AliDnsApi api)
        {
            this.api = api;
        }

        /// <summary>
        /// 获取域名列表
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public async Task<DescribeDomainsResult> DescribeDomainRecords(DescribeDomainsParam param = null)
        {
            return await api.GetAsync<DescribeDomainsParam, DescribeDomainsResult>(param ?? new DescribeDomainsParam());
        }

    }
}
