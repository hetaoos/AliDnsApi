using AliDns;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new AliDnsApi("YOUR_ACCESS_ID", "YOUR_ACCESS_KEY");
            var r = api.DomainRecord.DescribeDomainRecords(new AliDns.DomainRecord.DescribeDomainRecordsParam()
            {
                DomainName = "xware.io",
            }).Result;
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
