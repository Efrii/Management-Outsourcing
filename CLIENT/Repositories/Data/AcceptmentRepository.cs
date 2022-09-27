using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using API.Models;
using API.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CLIENT.Repositories.Data
{
    public class AcceptmentRepository : GenericRepository<Acceptment, int>
    {
        public AcceptmentRepository() : base("Acceptment/")
        {

        }

        public async Task<List<Acceptment>> GetJoin()
        {
            List<Acceptment> data = new List<Acceptment>();

            var responseTask = await client.GetAsync(request + "GetJoin/");

            if (responseTask.IsSuccessStatusCode)
            {
                var readTask = await responseTask.Content.ReadAsStringAsync();
                var parsedObject = JObject.Parse(readTask);
                var dataOnly = parsedObject["data"].ToString();

                data = JsonConvert.DeserializeObject<List<Acceptment>>(dataOnly);
            }

            return data;
        }

        public HttpStatusCode PostAcceptment(AcceptmentVM acceptment)
        {
            var postTask = client.PostAsJsonAsync<AcceptmentVM>(request + "PostAcceptment", acceptment);
            postTask.Wait();

            var result = postTask.Result;

            return result.StatusCode;
        }

        public async Task<Acceptment> GetJoinIdEmployee(int id)
        {
            Acceptment data = new Acceptment();

            var responseTask = await client.GetAsync(request + "GetJoinIdEmployee/" + id);

            if (responseTask.IsSuccessStatusCode)
            {
                var readTask = await responseTask.Content.ReadAsStringAsync();
                var parsedObject = JObject.Parse(readTask);
                var dataOnly = parsedObject["data"].ToString();

                data = JsonConvert.DeserializeObject<Acceptment>(dataOnly);
            }

            return data;
        }

    }
}
