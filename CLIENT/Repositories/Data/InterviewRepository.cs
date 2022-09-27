using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using API.Models;
using API.ViewModels;
using CLIENT.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CLIENT.Repositories.Data
{
    public class InterviewRepository : GenericRepository<Interview, int>
    {
        public InterviewRepository() : base("Interview/")
        {

        }

        public async Task<List<Interview>> GetJoin()
        {
            List<Interview> data = new List<Interview>();

            var responseTask = await client.GetAsync(request + "GetJoin");

            if (responseTask.IsSuccessStatusCode)
            {
                var readTask = await responseTask.Content.ReadAsStringAsync();
                var parsedObject = JObject.Parse(readTask);
                var dataOnly = parsedObject["data"].ToString();

                data = JsonConvert.DeserializeObject<List<Interview>>(dataOnly);
            }

            return data;
        }

        public async Task<List<Interview>> GetInterviewByIdEmp(int id)
        {
            List<Interview> data = new List<Interview>();

            var responseTask = await client.GetAsync(request + "GetInterviewByIdEmp/" + id);

            if (responseTask.IsSuccessStatusCode)
            {
                var readTask = await responseTask.Content.ReadAsStringAsync();
                var parsedObject = JObject.Parse(readTask);
                var dataOnly = parsedObject["data"].ToString();

                data = JsonConvert.DeserializeObject<List<Interview>>(dataOnly);
            }

            return data;
        }

        public async Task<Interview> GetInterviewsByIdInter(int id)
        {
            Interview data = new Interview();

            var responseTask = await client.GetAsync(request + "GetInterviewsByIdInter/" + id);

            if (responseTask.IsSuccessStatusCode)
            {
                var readTask = await responseTask.Content.ReadAsStringAsync();
                var parsedObject = JObject.Parse(readTask);
                var dataOnly = parsedObject["data"].ToString();

                data = JsonConvert.DeserializeObject<Interview>(dataOnly);
            }

            return data;
        }

        public async Task<List<Interview>> GetInterviewByIdCompany(int id)
        {
            List<Interview> data = new List<Interview>();

            var responseTask = await client.GetAsync(request + "GetInterviewByIdCompany/" + id);

            if (responseTask.IsSuccessStatusCode)
            {
                var readTask = await responseTask.Content.ReadAsStringAsync();
                var parsedObject = JObject.Parse(readTask);
                var dataOnly = parsedObject["data"].ToString();

                data = JsonConvert.DeserializeObject<List<Interview>>(dataOnly);
            }

            return data;
        }

        public HttpStatusCode IsDoneInterview(IsDoneInterviewVM done)
        {
            var postTask = client.PostAsJsonAsync<IsDoneInterviewVM>(request + "IsDoneInterview", done);
            postTask.Wait();

            var result = postTask.Result;

            return result.StatusCode;
        }

        public HttpStatusCode IsAcceptmentInterview(IsAcceptmentInterviewVM done)
        {
            var postTask = client.PostAsJsonAsync<IsAcceptmentInterviewVM>(request + "IsAcceptmentInterview", done);
            postTask.Wait();

            var result = postTask.Result;

            return result.StatusCode;
        }
    }
}
