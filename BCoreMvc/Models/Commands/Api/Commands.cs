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

namespace BCoreMvc.Models.Commands.Api
{
    public class Commands
    {
        private IConfiguration _configuration { get; }
        protected string ApiURL { get; }
        protected IMapper Mapper { get; }

        public Commands(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            ApiURL = _configuration.GetValue<string>("ApiURL");
            Mapper = mapper;
        }

        protected async Task<T> Get<T>(string url)
        {
            string json = "";
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(url))
            using (HttpContent content = response.Content)
            {
                json = await content.ReadAsStringAsync();
            }

            return JsonConvert.DeserializeObject<T>(json);
        }

        protected async Task<T> Post<T>(string url, T item)
        {
            /*var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(model);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            return await client.PostAsync(requestUrl, stringContent);*/

            var jsonContent = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            string json = "";
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.PostAsync(url, jsonContent))
            using (HttpContent content = response.Content)
            {
                json = await content.ReadAsStringAsync();
            }

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}

