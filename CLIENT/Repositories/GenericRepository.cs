using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CLIENT.Base;
using CLIENT.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CLIENT.Repositories
{
    public class GenericRepository<TModel, TPrimaryKey> : IGeneralRepository<TModel, TPrimaryKey>
        where TModel : class
    {
        internal readonly Uri baseAddress = new Uri("https://localhost:5001/api/");
        protected readonly string request;
        internal readonly HttpClient client;
        internal readonly IHttpContextAccessor accessor;

        public GenericRepository(string request)
        {
            this.request = request;
            accessor = new HttpContextAccessor();
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessor.HttpContext.Session.GetString("Token"));
        }

        public async Task<List<TModel>> Get()
        {
            List<TModel> model = new List<TModel>();

            var responseTask = await client.GetAsync(request);

            if (responseTask.IsSuccessStatusCode)
            {
                var readTask = await responseTask.Content.ReadAsStringAsync();
                var parsedObject = JObject.Parse(readTask);
                var dataOnly = parsedObject["data"].ToString();

                model = JsonConvert.DeserializeObject<List<TModel>>(dataOnly);
            }

            return model;
        }

        public async Task<TModel> Get(TPrimaryKey Id)
        {
            TModel model = null;

            var responseTask = await client.GetAsync(request + Id.ToString());

            if (responseTask.IsSuccessStatusCode)
            {
                var readTask = responseTask.Content.ReadAsStringAsync().Result;
                var parsedObject = JObject.Parse(readTask);
                var dataOnly = parsedObject["data"].ToString();

                model = JsonConvert.DeserializeObject<TModel>(dataOnly);
            }

            return model;
        }

        public HttpStatusCode Post(TModel model)
        {
            var postTask = client.PostAsJsonAsync<TModel>(request, model);
            postTask.Wait();

            var result = postTask.Result;

            return result.StatusCode;
        }

        public HttpStatusCode Put(TModel model)
        {
            var putTask = client.PutAsJsonAsync<TModel>(request, model);
            putTask.Wait();

            var result = putTask.Result;

            return result.StatusCode;
        }

        public HttpStatusCode Delete(TPrimaryKey Id)
        {
            var deleteTask = client.DeleteAsync(request + Id.ToString());
            deleteTask.Wait();

            var result = deleteTask.Result;

            return result.StatusCode;
        }
    }
}
