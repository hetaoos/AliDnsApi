using AliDns;
using AliDns.DomainRecord;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AliDnsUpdater.Services
{
    /// <summary>
    /// 更新服务
    /// </summary>
    public class AliDnsUpdateService
    {
        private readonly IOptions<AliDnsSettings> _optionsAccessor;
        private readonly AliDnsApi _api;
        private Thread thread;
        private string last_ip;
        public DateTime last_update = DateTime.MinValue;

        public AliDnsUpdateService(IOptions<AliDnsSettings> optionsAccessor, AliDnsApi api)
        {
            _optionsAccessor = optionsAccessor;
            _api = api;
            CheckAndStartUpdateThread();
        }

        private async void UpdateThread()
        {
            int i = 0;
            do
            {
                if (!((DateTime.Now - last_update).TotalMinutes > _optionsAccessor.Value.RefreshInterval))
                {
                    Thread.Sleep(60000);
                    continue;
                }
                try
                {
                    var ip = await MyIP();
                    if (ip == last_ip && i++ <= 10)
                    {
                        Thread.Sleep(60000);
                        last_update = DateTime.Now;
                        continue;
                    }
                    last_update = DateTime.Now;
                    await Refresh(ip);
                    last_ip = ip;
                    i = 0;
                }
                catch
                {

                }

            } while (true);


        }

        private void CheckAndStartUpdateThread()
        {
            lock (this)
            {
                if (!(_optionsAccessor.Value.RefreshInterval > 0))
                    return;
                if (thread?.IsAlive == true)
                    return;
                thread = new Thread(UpdateThread);
                thread.Start();
            }
        }

        /// <summary>
        /// 获取当前ip
        /// </summary>
        /// <returns></returns>
        public async Task<string> MyIP()
        {
            try
            {
                using (HttpClientHandler httpClientHandler = new HttpClientHandler() { Proxy = null, UseProxy = false })
                {
                    using (HttpClient client = new HttpClient(httpClientHandler))
                    {
                        var ip = await client.GetStringAsync("http://whatismyip.akamai.com/");
                        return ip;
                    }
                }
            }
            catch
            {
                return "0.0.0.0";
            }
        }

        /// <summary>
        /// 刷新ip
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <returns></returns>
        public async Task<string> Refresh(string ip)
        {
            CheckAndStartUpdateThread();
            if (string.IsNullOrWhiteSpace(ip))
            {
                ip = await MyIP();
            }
            if (IPAddress.TryParse(ip, out IPAddress ipAddress) == false)
                return $"IP地址非法:{ip}";
            ip = ipAddress.ToString();
            if (ip == "0.0.0.0")
                return "IP不正确。";
            var subDomains = _optionsAccessor.Value.Domains?.Where(o => string.IsNullOrWhiteSpace(o) == false)
                  .Select(o => o.Trim().ToLower())
                  .Distinct().ToList();
            if (!(subDomains.Count > 0))
                return "域名列表为空。";
            StringBuilder sb = new StringBuilder();
            do
            {
                var r = await _api.Domain.DescribeDomainRecords();
                if (!(r.Domains?.Domain?.Count > 0))
                {
                    sb.AppendLine("获取域名列表出错，或者您的账户上没有域名。");
                    break;
                }
                var domains = r.Domains.Domain.Select(o => o.DomainName.ToLower()).ToList();
                foreach (var domain in domains)
                {
                    //查找该域名的子域名
                    var ls = subDomains.Where(o => o.EndsWith(domain)).ToList();
                    if (ls.Count == 0)
                        continue;
                    //移除
                    subDomains.RemoveAll(o => ls.Contains(o));
                    var len = domain.Length;
                    //获取子域名
                    ls = ls.Select(o => o.Substring(0, o.Length - len).Trim().Trim('.')).ToList();
                    //替换空为@
                    if (ls.Contains(""))
                    {
                        ls.Remove("");
                        ls.Add("@");
                        ls = ls.Distinct().ToList();
                    }
                    //获取已有记录
                    var records = (await _api.DomainRecord.DescribeDomainRecords(new DescribeDomainRecordsParam()
                    {
                        DomainName = domain,
                        PageSize = 500,
                    }))?.DomainRecords?.Record;
                    if (records == null)
                    {
                        sb.AppendLine($"获取域名解析记录错误：{domain}");
                        continue;
                    }
                    records = records.Where(o => o.Type == "A" || o.Type == "CNAME" || o.Type == "REDIRECT_URL" || o.Type == "FORWORD_URL").ToList();
                    foreach (var rr in ls)
                    {
                        var old = records.FirstOrDefault(o => o.RR == rr);
                        try
                        {
                            if (old != null)//更新
                            {
                                if (old.Type == "A" && old.Value == ip)
                                {
                                    sb.AppendLine($"解析记录未更新：{rr}.{domain}");
                                    continue;
                                }
                                var rd = await _api.DomainRecord.UpdateDomainRecord(new UpdateDomainRecordParam()
                                {
                                    DomainName = domain,
                                    RecordId = old.RecordId,
                                    RR = rr,
                                    Type = "A",
                                    Value = ip,
                                });
                                sb.AppendLine($"更新解析记录成功：{rr}.{domain}");
                            }
                            else//添加
                            {
                                var rd = await _api.DomainRecord.AddDomainRecord(new AddDomainRecordParam()
                                {
                                    DomainName = domain,
                                    RR = rr,
                                    Type = "A",
                                    Value = ip,
                                });
                                sb.AppendLine($"新增解析记录成功：{rr}.{domain}");
                            }
                        }
                        catch
                        {
                            sb.AppendLine($"更新解析记录失败：{rr}.{domain}");
                        }
                    }
                }

                if (subDomains.Count > 0)
                {
                    sb.AppendLine($"以下域名无法无权更新：{string.Join(", ", subDomains)}");
                }

            } while (false);

            return sb.ToString();
        }
    }
}
