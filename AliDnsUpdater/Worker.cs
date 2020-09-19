using AliDns;
using AliDns.DomainRecord;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AliDnsUpdater
{
    public class Worker : BackgroundService
    {
        private readonly AliDnsSettings _cfg;
        private readonly ILogger<Worker> _logger;
        private AliDnsApi _api;
        private string _last_ip;
        public DateTime _last_update = DateTime.MinValue;

        public Worker(IOptions<AliDnsSettings> options, ILogger<Worker> logger)
        {
            _cfg = options.Value;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var error = _cfg.GetError();
            if (string.IsNullOrWhiteSpace(error) == false)
            {
                _logger.LogError(error);
                throw new Exception(error);
            }
            if (_cfg.Interval <= 0)
                _cfg.Interval = 10;
            _api = new AliDnsApi(_cfg.AccessId, _cfg.AccessKey);

            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                if (!((DateTime.Now - _last_update).TotalMinutes > _cfg.Interval))
                {
                    await Task.Delay(60_000, stoppingToken);
                    continue;
                }
                try
                {
                    var ip = await MyIP();
                    if (string.IsNullOrWhiteSpace(ip))
                    {
                        Console.WriteLine("获取外部IP错误。");
                        await Task.Delay(30_000, stoppingToken);//休息30s重试
                        continue;
                    }
                    //24小时强制更新一次
                    if (ip == _last_ip && (DateTime.Now - _last_update).TotalDays < 1)
                    {
                        await Task.Delay(60_000, stoppingToken);//休息30s重试
                        _last_update = DateTime.Now;
                        continue;
                    }
                    _last_update = DateTime.Now;
                    var msg = await Refresh(ip);
                    _logger.LogInformation(msg);
                    _last_ip = ip;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "更新解析错误。");
                }
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
                using var httpClientHandler = new HttpClientHandler();
                using var client = new HttpClient(httpClientHandler);
                return await client.GetStringAsync("http://whatismyip.akamai.com/");
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 刷新ip
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <returns></returns>
        public async Task<string> Refresh(string ip)
        {
            if (string.IsNullOrWhiteSpace(ip))
            {
                ip = await MyIP();
            }
            if (IPAddress.TryParse(ip, out IPAddress ipAddress) == false)
                return $"IP地址非法:{ip}";
            ip = ipAddress.ToString();
            if (ip == "0.0.0.0")
                return "IP不正确或者获取外部IP错误。";
            var subDomains = _cfg.Domains?.Where(o => string.IsNullOrWhiteSpace(o) == false)
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