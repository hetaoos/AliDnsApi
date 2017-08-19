using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AliDns.DomainRecord
{
    /// <summary>
    /// 解析管理
    /// </summary>
    public class DomainRecordApi
    {
        public AliDnsApi api { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainRecordApi"/> class.
        /// </summary>
        /// <param name="api">The API.</param>
        public DomainRecordApi(AliDnsApi api)
        {
            this.api = api;
        }

        /// <summary>
        /// 获取解析记录列表
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public async Task<DescribeDomainRecordsResult> DescribeDomainRecords(DescribeDomainRecordsParam param)
        {
            return await api.GetAsync<DescribeDomainRecordsParam, DescribeDomainRecordsResult>(param);
        }

        /// <summary>
        /// 添加解析记录
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public async Task<AddDomainRecordResult> AddDomainRecord(AddDomainRecordParam param)
        {
            return await api.GetAsync<AddDomainRecordParam, AddDomainRecordResult>(param);
        }
    }
}
