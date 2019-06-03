using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Incdev.Interface;
using Incdev.Interface.Configuration;
using Incdev.Interface.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MVCClient.Models;
using Newtonsoft.Json.Linq;


namespace MVCClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly Endpoints _endpoints;
        private readonly ILogger _log;
        private HttpContext _ctx;
        private readonly ITestService _svc;

        public HomeController(IOptions<Endpoints> endpointSrc, ILogger<HomeController> log, IHttpContextAccessor ctxAccess, ITestService svc)
        {
            _endpoints = endpointSrc.Value;
            _log = log;
            _ctx = ctxAccess.HttpContext;
            _svc = svc;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [NonAction]
        public void ExceptionEnerator()
        {
            throw new Exception("Test");   
        }

        [Authorize]
        [HttpGet]
        public IActionResult Contact()
        {
            var pageTitle = $"Your contact page. {User.Identity.Name}";
            ViewData["Message"] = pageTitle;

            _log.LogEvent()( EventRegister.ContactEvent, ("Client", new { ExtraData = "Fred Johnstone." }));

            try{
                ExceptionEnerator();
            }
            catch(Exception ex)
            {
                _log.LogExceptionEvent()(EventRegister.ExceptionGeneratedEvent, ex, ("DATA", "Some Extra Info."));
            }

            _svc.DoStuff();


            _log.LogEvent() (EventRegister.TheEvent, ("STUFF", 23), ("MO-STUFF", 45.7));

            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CallApiUsingUserAccessToken()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var content = await client.GetStringAsync(_endpoints.API + "/identity");

            ViewBag.Json = JArray.Parse(content).ToString();

            return View("json");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
