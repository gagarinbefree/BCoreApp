using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using IdentityModel.Client;
using BCoreMvc.Models.Commands;
using System;

namespace BCoreMvc.Controllers
{
    public class HomeController : Controller
    {
        private IPostCommands _cmd;

        public HomeController(IPostCommands cmd)
        {
            _cmd = cmd;
        }

        public async Task<IActionResult> Index()
        {            
            var xxx = await _cmd.GetAll();

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Secure()
        {
            /*var tokenClient = new TokenClient("http://localhost:5000/connect/token", "mvc", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("BCoreIdentityApi");

            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
            var content = await client.GetStringAsync("http://localhost:5001/api/identity");*/

            ViewData["Message"] = "Secure page.";

            return View();
        }

        public async Task Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookies");
            await HttpContext.Authentication.SignOutAsync("oidc");
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> CallApiUsingClientCredentials()
        {
            var tokenClient = new TokenClient("http://localhost:5000/connect/token", "mvc", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("BCoreIdentityApi");

            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
            var content = await client.GetStringAsync("http://localhost:5001/api/identity");

            ViewBag.Json = JArray.Parse(content).ToString();

            return View("json"); 
        }

        public async Task<IActionResult> CallApiUsingUserAccessToken()
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var content = await client.GetStringAsync("http://localhost:5001/api/identity");

            ViewBag.Json = JArray.Parse(content).ToString();

            return View("json");
        }
    }
}