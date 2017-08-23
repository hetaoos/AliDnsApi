using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using AliDns;
using System.Net;
using System.Text;
using AliDns.DomainRecord;
using System.Net.Http;
using AliDnsUpdater.Services;

namespace AliDnsUpdater.Controllers
{
    [Route("api/[controller]")]
    public class UpdateController : Controller
    {
        private readonly IOptions<AliDnsSettings> _optionsAccessor;
        private readonly AliDnsUpdateService _aliDnsUpdateService;

        public UpdateController(IOptions<AliDnsSettings> optionsAccessor, AliDnsUpdateService aliDnsUpdateService)
        {
            _optionsAccessor = optionsAccessor;
            _aliDnsUpdateService = aliDnsUpdateService;
        }
        // GET api/values
        [HttpGet("domains")]
        public IEnumerable<string> Domains()
        {
            return _optionsAccessor.Value.Domains;
        }

        [HttpGet("myip")]
        public async Task<string> MyIP()
        {
            return await _aliDnsUpdateService.MyIP();
        }

        [HttpGet("")]
        public async Task<string> Get(string ip)
        {
            return await _aliDnsUpdateService.Refresh(ip);
        }
    }
}
