using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using API.Models;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CLIENT.Repositories.Data
{
    public class CVRepository : GenericRepository<CV, int>
    {
        IHttpContextAccessor accessor;
        IHostEnvironment hostingEnvironment;

        public CVRepository(IHostEnvironment hostingEnvironment) : base("CV/")
        {
            accessor = new HttpContextAccessor();
            this.hostingEnvironment = hostingEnvironment;
        }

        public HttpStatusCode UploadCV(CV cV)
        {
            var files = accessor.HttpContext.Request.Form.Files;
            if (files != null && files.Count > 0)
            {
                var pathDatabase = "";
                foreach (var file in files)
                {
                    FileInfo fi = new FileInfo(file.FileName);
                    var newFilename = "CV_"+ fi.Name + fi.Extension;
                    var pathDirectory = Path.Combine("", hostingEnvironment.ContentRootPath + "/wwwroot/Data/" + newFilename);
                    pathDatabase = "https://localhost:60682/Data/" + newFilename;
                    using (var stream = new FileStream(pathDirectory, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                CV data = new CV();
                data.Id_employee = cV.Id_employee;
                data.Id_company = cV.Id_company;
                data.Job_selected = cV.Job_selected;
                data.Cv_employee = pathDatabase;
                var postTask = client.PostAsJsonAsync<CV>(request, data);
                postTask.Wait();

                var result = postTask.Result;

                return result.StatusCode;

            }
            else
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        public HttpStatusCode PutCV(CV cV)
        {
            var files = accessor.HttpContext.Request.Form.Files;
            if (files != null && files.Count > 0)
            {
                var pathDatabase = "";
                foreach (var file in files)
                {
                    FileInfo fi = new FileInfo(file.FileName);
                    var newFilename = "CV_" + fi.Name + fi.Extension;
                    var pathDirectory = Path.Combine("", hostingEnvironment.ContentRootPath + "/wwwroot/Data/" + newFilename);
                    pathDatabase = "https://localhost:60682/Data/" + newFilename;
                    using (var stream = new FileStream(pathDirectory, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                CV data = new CV();
                data.Id_cv = cV.Id_cv;
                data.Id_employee = cV.Id_employee;
                data.Id_company = cV.Id_company;
                data.Job_selected = cV.Job_selected;
                data.Cv_employee = pathDatabase;
                var postTask = client.PutAsJsonAsync<CV>(request, data);
                postTask.Wait();

                var result = postTask.Result;

                return result.StatusCode;

            }
            else
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        public async Task<CV> GetCvById(int id)
        {
            CV data = new CV();

            var responseTask = await client.GetAsync(request + "GetCv/" + id);

            if (responseTask.IsSuccessStatusCode)
            {
                var readTask = await responseTask.Content.ReadAsStringAsync();
                var parsedObject = JObject.Parse(readTask);
                var dataOnly = parsedObject["data"].ToString();

                data = JsonConvert.DeserializeObject<CV>(dataOnly);
            }

            return data;
        }

        public async Task<List<CV>> GetCvByCompany(int id)
        {
            List<CV> data = new List<CV>();

            var responseTask = await client.GetAsync(request + "GetCvByCompany/" + id);

            if (responseTask.IsSuccessStatusCode)
            {
                var readTask = await responseTask.Content.ReadAsStringAsync();
                var parsedObject = JObject.Parse(readTask);
                var dataOnly = parsedObject["data"].ToString();

                data = JsonConvert.DeserializeObject<List<CV>>(dataOnly);
            }

            return data;
        }

        public async Task<List<CV>> GetJoinByIdClass(int id)
        {
            List<CV> data = new List<CV>();

            var responseTask = await client.GetAsync(request + "GetJoinByIdClass/" + id);

            if (responseTask.IsSuccessStatusCode)
            {
                var readTask = await responseTask.Content.ReadAsStringAsync();
                var parsedObject = JObject.Parse(readTask);
                var dataOnly = parsedObject["data"].ToString();

                data = JsonConvert.DeserializeObject<List<CV>>(dataOnly);
            }

            return data;
        }

        public HttpStatusCode IsCheck(CvVM cvVM)
        {
            var postTask = client.PostAsJsonAsync<CvVM>(request + "IsCheck", cvVM);
            postTask.Wait();

            var result = postTask.Result;

            return result.StatusCode;
        }

    }
}
