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
        /// 获取子域名的解析记录列表
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public async Task<DescribeDomainRecordsResult> DescribeSubDomainRecords(DescribeSubDomainRecordsParam param)
        {
            return await api.GetAsync<DescribeSubDomainRecordsParam, DescribeDomainRecordsResult>(param);
        }

        /// <summary>
        /// 添加解析记录
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public async Task<DefaultDomainRecordResult> AddDomainRecord(AddDomainRecordParam param)
        {
            return await api.GetAsync<AddDomainRecordParam, DefaultDomainRecordResult>(param);
        }

        /// <summary>
        /// 删除解析记录
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public async Task<DefaultDomainRecordResult> DeleteDomainRecord(DeleteDomainRecordParam param)
        {
            return await api.GetAsync<DeleteDomainRecordParam, DefaultDomainRecordResult>(param);
        }

        /// <summary>
        /// 修改解析记录
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public async Task<DefaultDomainRecordResult> UpdateDomainRecord(UpdateDomainRecordParam param)
        {
            return await api.GetAsync<UpdateDomainRecordParam, DefaultDomainRecordResult>(param);
        }

        /// <summary>
        /// 设置解析记录状态
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public async Task<SetDomainRecordStatusResult> SetDomainRecordStatus(SetDomainRecordStatusParam param)
        {
            return await api.GetAsync<SetDomainRecordStatusParam, SetDomainRecordStatusResult>(param);
        }

        /// <summary>
        /// 获取解析记录信息
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public async Task<DescribeDomainRecordInfoResult> DescribeDomainRecordInfo(DescribeDomainRecordInfoParam param)
        {
            return await api.GetAsync<DescribeDomainRecordInfoParam, DescribeDomainRecordInfoResult>(param);
        }
        /// <summary>
        /// 删除主机记录对应的解析记录
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public async Task<DeleteSubDomainRecordsResult> DeleteSubDomainRecords(DeleteSubDomainRecordsParam param)
        {
            return await api.GetAsync<DeleteSubDomainRecordsParam, DeleteSubDomainRecordsResult>(param);
        }
    }
}
