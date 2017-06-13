using BCoreDal.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BCore
{
    public class Seed
    {
        private HttpClient _client = new HttpClient();

        public Seed()
        {
            _client.BaseAddress = new Uri("http://localhost:2116/api/");
        }

        public async Task StartAsync()
        {
            /*await _clearAsync();

            var res = await _createCollection("Posts");
            Collection parent = JsonConvert.DeserializeObject<Collection>(await res.Content.ReadAsStringAsync());
            if (parent == null)
                throw new Exception("Collection not created");

            await _createCollection("Parts", parent.Id);
            await _createCollection("Hashes", parent.Id);
            await _createCollection("Comments", parent.Id);*/


        }

        /*private async Task _clearAsync()
        {
            List<Collection> collections = await _getAllCollectionsAsync();
            foreach (Collection c in collections)
            {
                await _client.DeleteAsync(String.Format("Collections/{0}", c.Id));
            }            
        }

        private async Task<HttpResponseMessage> _createCollection(string name, Guid? parentId = null)
        {
            var c = new Collection
            {
                Name = "Posts",
                CreatedOn = DateTime.Now,
                ParentId = parentId
            };
            var httpContent = new StringContent(JsonConvert.SerializeObject(c), Encoding.UTF8, "application/json");

            return await _client.PostAsync("Collections", httpContent);
        }

        private async Task<HttpResponseMessage> _createItem(string name, Guid collectionId)
        {
            var i = new Item
            {
                Name = name,
                CreatedOn = DateTime.Now,
                Deleted = false,
                CollectionId = collectionId                
            }
        }
        

        private async Task<List<Collection>> _getAllCollectionsAsync()
        {
            var responce = await _client.GetAsync("Collections");

            string json = await responce.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Collection>>(json) ?? new List<Collection>();
        }*/
    }
}
