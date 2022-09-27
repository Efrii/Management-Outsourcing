using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using CLIENT.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CLIENT.Repositories.Data
{
    public class JobsRepository : GenericRepository<Jobs, int>
    {
        public JobsRepository() : base("Jobs/")
        {

        }

        public async Task<List<Jobs>> GetJoinID(int id)
        {
            List<Jobs> data = new List<Jobs>();

            var responseTask = await client.GetAsync(request + "GetJoinID/" + id);

            if (responseTask.IsSuccessStatusCode)
            {
                var readTask = await responseTask.Content.ReadAsStringAsync();
                var parsedObject = JObject.Parse(readTask);
                var dataOnly = parsedObject["data"].ToString();

                data = JsonConvert.DeserializeObject<List<Jobs>>(dataOnly);
            }

            return data;
        }

        public async Task<List<Jobs>> GetJoinName(string name)
        {
            List<Jobs> data = new List<Jobs>();

            var responseTask = await client.GetAsync(request + "GetJoinName/" + name);

            if (responseTask.IsSuccessStatusCode)
            {
                var readTask = await responseTask.Content.ReadAsStringAsync();
                var parsedObject = JObject.Parse(readTask);
                var dataOnly = parsedObject["data"].ToString();

                data = JsonConvert.DeserializeObject<List<Jobs>>(dataOnly);
            }

            return data;
        }
    }
}
