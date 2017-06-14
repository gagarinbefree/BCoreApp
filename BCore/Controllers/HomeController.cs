using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityModel.Client;
using System.Net.Http;
using BCore.Models;
using Newtonsoft.Json.Linq;

namespace BCore.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("BCoreIdentityApi");
            if (tokenResponse.IsError)
                return View(new AcessMessageTest { Message = "Error token response" });

            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5002/api/identity");
            if (!response.IsSuccessStatusCode)
                return View(new AcessMessageTest { Message = response.StatusCode.ToString() });

            var content = await response.Content.ReadAsStringAsync();
            return View(new AcessMessageTest { Message = JArray.Parse(content).ToString() });
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
