# AliDnsApi
阿里云域名解析管理Api for .NET Standard 2.0

### 如何使用

举个例子：
```cs
var api = new AliDnsApi("YOUR_ACCESS_ID", "YOUR_ACCESS_KEY");
var r = await api.DomainRecord.DescribeDomainRecords(new DescribeDomainRecordsParam()
        {
             DomainName = "xware.io",
        });
```

### Docker
```
docker run -d \
    --env AccessId=xxxx \
    --env AccessKey=xxx \
    --env Interval=10 \
    --env Domains=demo.com,*.demo.com,www.demo.com \
    hetaoos/alidns
```

**Environment Variables**
- **AccessId**: AccessId
- **AccessKey**: AccessKey
- **Interval**: IP detection interval (minutes), (default: 10)
- **Domains**: The dns record list, multiple records are separated by commas.
