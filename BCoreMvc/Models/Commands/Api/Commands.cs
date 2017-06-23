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
using Microsoft.AspNetCore.Authentication;


namespace BCoreMvc.Models.Commands.Api
{
    public class Commands
    {
        private IConfiguration _configuration { get; }
        protected Uri ApiPath { get; }
        protected IMapper Mapper { get; }        

        public Commands(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            ApiPath = new Uri(_configuration.GetValue<string>("ApiURL"));
            Mapper = mapper;
            User.Claims
        }

        protected async Task<T> Get<T>(string path)
        {
            string json = "";
            using (HttpClient client = new HttpClient { BaseAddress = ApiPath })
            using (HttpResponseMessage response = await client.GetAsync(path))
            using (HttpContent content = response.Content)
            {
                json = await content.ReadAsStringAsync();
            }

            return JsonConvert.DeserializeObject<T>(json);
        }

        protected async Task<T> Post<T>(string path, T item)
        {
            string json = "";
            StringContent strContent = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");            
            using (HttpClient client = new HttpClient { BaseAddress = ApiPath })
            using (HttpResponseMessage response = await client.PostAsync(path, strContent))
            using (HttpContent content = response.Content)
            {
                json = await content.ReadAsStringAsync();
            }

            return JsonConvert.DeserializeObject<T>(json);
        }        
    }
}

