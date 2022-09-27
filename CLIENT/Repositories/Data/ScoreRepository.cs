using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using API.Models;
using API.ViewModels;
using CLIENT.Base;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CLIENT.Repositories.Data
{
    public class ScoreRepository : GenericRepository<Score, int>
    {
        public ScoreRepository() : base("Score/")
        {

        }

        public async Task<List<Score>> GetJoin()
        {
            List<Score> data = new List<Score>();

            var responseTask = await client.GetAsync(request + "GetJoin");

            if (responseTask.IsSuccessStatusCode)
            {
                var readTask = await responseTask.Content.ReadAsStringAsync();
                var parsedObject = JObject.Parse(readTask);
                var dataOnly = parsedObject["data"].ToString();

                data = JsonConvert.DeserializeObject<List<Score>>(dataOnly);
            }

            return data;
        }

        public async Task<Score> GetJoinID(int id)
        {
            Score data = new Score();

            var responseTask = await client.GetAsync(request + "data/" + id);

            if (responseTask.IsSuccessStatusCode)
            {
                var readTask = await responseTask.Content.ReadAsStringAsync();
                var parsedObject = JObject.Parse(readTask);
                var dataOnly = parsedObject["data"].ToString();

                data = JsonConvert.DeserializeObject<Score>(dataOnly);
            }

            return data;
        }

        public async Task<Score> GetJoinIdEmployee(int id)
        {
            Score data = new Score();

            var responseTask = await client.GetAsync(request + "GetJoinIdEmployee/" + id);

            if (responseTask.IsSuccessStatusCode)
            {
                var readTask = await responseTask.Content.ReadAsStringAsync();
                var parsedObject = JObject.Parse(readTask);
                var dataOnly = parsedObject["data"].ToString();

                data = JsonConvert.DeserializeObject<Score>(dataOnly);
            }

            return data;
        }

        public async Task<List<Score>> GetJoinClassId(int id)
        {
            List<Score> data = new List<Score>();

            var responseTask = await client.GetAsync(request + "GetJoinClassId/" + id);

            if (responseTask.IsSuccessStatusCode)
            {
                var readTask = await responseTask.Content.ReadAsStringAsync();
                var parsedObject = JObject.Parse(readTask);
                var dataOnly = parsedObject["data"].ToString();

                data = JsonConvert.DeserializeObject<List<Score>>(dataOnly);
            }

            return data;
        }

        public HttpStatusCode PostScore(ScoreVM score)
        {
            var postTask = client.PostAsJsonAsync<ScoreVM>(request + "PostScore", score);
            postTask.Wait();

            var result = postTask.Result;

            return result.StatusCode;
        }

        public HttpStatusCode PutScore(ScoreVM score)
        {
            var postTask = client.PutAsJsonAsync<ScoreVM>(request + "PutScore", score);
            postTask.Wait();

            var result = postTask.Result;

            return result.StatusCode;
        }
    }
}
