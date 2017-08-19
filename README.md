# AliDnsApi
阿里云的域名云解析SDK for .NET Core 2.0

### 如何使用

举个例子：
```cs
var api = new AliDnsApi("YOUR_ACCESS_ID", "YOUR_ACCESS_KEY");
var r = await api.DomainRecord.DescribeDomainRecords(new DescribeDomainRecordsParam()
        {
             DomainName = "xware.io",
        });
```