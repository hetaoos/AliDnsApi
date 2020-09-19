using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Text;

namespace AliDnsUpdater
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //注册编码
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<AliDnsSettings>(cfg =>
                    {
                        //先从配置文件读取
                        hostContext.Configuration.GetSection("AliDns").Bind(cfg);

                        //从环境变量中读取配置
                        var accessId = Environment.GetEnvironmentVariable(nameof(cfg.AccessId));
                        var accessKey = Environment.GetEnvironmentVariable(nameof(cfg.AccessKey));
                        if (!(string.IsNullOrWhiteSpace(accessId) || string.IsNullOrWhiteSpace(accessKey)))
                        {
                            cfg.AccessId = accessId;
                            cfg.AccessKey = accessKey;
                        }
                        var value = Environment.GetEnvironmentVariable(nameof(cfg.Interval));
                        if (string.IsNullOrWhiteSpace(value) == false && double.TryParse(value, out double d) && d > 0)
                            cfg.Interval = (int)d;
                        var domains = Environment.GetEnvironmentVariable(nameof(cfg.Domains))?.Split(",; 、，；".ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
                        if (domains?.Any() == true)
                            cfg.Domains = domains;
                    });
                    services.AddHostedService<Worker>();
                });
    }
}