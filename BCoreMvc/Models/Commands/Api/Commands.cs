using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using AutoMapper;
using System.Text;
using System.Security.Claims;
using System.Net;

namespace BCoreMvc.Models.Commands.Api
{
    public class Commands
    {        
        private string _apiURL { get; }
        protected IMapper Mapper { get; }

        public Commands(IConfiguration configuration, IMapper mapper)
        {            
            _apiURL = configuration.GetValue<string>("ApiURL");

            Mapper = mapper;
        }

        protected async Task<T> Get<T>(string url)
        {                   
            string json = "";
            using (HttpClient client = new HttpClient { BaseAddress = new Uri(_apiURL) })
            using (HttpResponseMessage response = await client.GetAsync(url))
            using (HttpContent content = response.Content)
            {
                json = await content.ReadAsStringAsync();
            }
            
            return JsonConvert.DeserializeObject<T>(json);
        }

        protected async Task<T> Get<T>(string url, int? page)
        {
            throw new NotImplementedException();
        }

        protected async Task<T> Post<T>(string url, T item)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            string json = "";
            using (HttpClient client = new HttpClient() { BaseAddress = new Uri(_apiURL) })
            using (HttpResponseMessage response = await client.PostAsync(url, jsonContent))
            using (HttpContent content = response.Content)
            {
                json = await content.ReadAsStringAsync();
            }

            return JsonConvert.DeserializeObject<T>(json);
        }

        protected async Task<int> Delete(string url)
        {            
            using (HttpClient client = new HttpClient() { BaseAddress = new Uri(_apiURL) })
            using (HttpResponseMessage response = await client.DeleteAsync(url))
            {
                if (response.IsSuccessStatusCode)
                    return 1;
            }

            return -1;
        }


        protected string GetUserId(ClaimsPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("sub");
            if (claim != null)
                return claim.Value;

            return "";
        }
    }
}

