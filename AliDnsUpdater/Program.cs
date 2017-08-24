using AliDns;
using AliDnsUpdater.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AliDnsUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            var cfg = new AliDnsSettings();
            //从环境变量中读取配置
            cfg.AccessId = Environment.GetEnvironmentVariable(nameof(cfg.AccessId));
            cfg.AccessKey = Environment.GetEnvironmentVariable(nameof(cfg.AccessKey));
            var value = Environment.GetEnvironmentVariable(nameof(cfg.Interval));
            if (string.IsNullOrWhiteSpace(value) == false && double.TryParse(value, out double d) && d > 0)
                cfg.Interval = (int)d;
            cfg.Domains = Environment.GetEnvironmentVariable(nameof(cfg.Domains))?.Split(",; 、，；".ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

            var error = cfg.GetError();
            if (string.IsNullOrWhiteSpace(error) == false)
            {
                Console.WriteLine(error);
            }
            else
            {
                var api = new AliDnsApi(cfg.AccessId, cfg.AccessKey);
                var service = new AliDnsUpdateService(cfg, api);
                Console.WriteLine("starting...");
            }
            Console.ReadKey();

        }
    }
}
