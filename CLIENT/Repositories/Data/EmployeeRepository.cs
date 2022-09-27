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
    public class EmployeeRepository : GenericRepository<Employee, int>
    {

        public EmployeeRepository() : base("Employee/")
        {

        }

        // untuk get di score yang merupakan role employe
        public async Task<List<UserRole>> GetEmployeeByRole()
        {
            List<UserRole> data = new List<UserRole>();

            var responseTask = await client.GetAsync(request + "GetEmployeeByRole/EMPLOYEEBOOTCAMP");

            if (responseTask.IsSuccessStatusCode)
            {
                var readTask = await responseTask.Content.ReadAsStringAsync();
                var parsedObject = JObject.Parse(readTask);
                var dataOnly = parsedObject["data"].ToString();

                data = JsonConvert.DeserializeObject<List<UserRole>>(dataOnly);
            }

            return data;
        }

        public async Task<List<Employee>> GetDetailClass(int id)
        {
            List<Employee> data = new List<Employee>();

            var responseTask = await client.GetAsync(request + "GetDetailClass/" + id);

            if (responseTask.IsSuccessStatusCode)
            {
                var readTask = await responseTask.Content.ReadAsStringAsync();
                var parsedObject = JObject.Parse(readTask);
                var dataOnly = parsedObject["data"].ToString();

                data = JsonConvert.DeserializeObject<List<Employee>>(dataOnly);
            }

            return data;
        }

        public async Task<List<GetTrainnerVM>> GetTrainner(int id)
        {
            List<GetTrainnerVM> data = new List<GetTrainnerVM>();

            var responseTask = await client.GetAsync(request + "GetTrainner/" + id);

            if (responseTask.IsSuccessStatusCode)
            {
                var readTask = await responseTask.Content.ReadAsStringAsync();
                var parsedObject = JObject.Parse(readTask);
                var dataOnly = parsedObject["data"].ToString();

                data = JsonConvert.DeserializeObject<List<GetTrainnerVM>>(dataOnly);
            }

            return data;
        }

        public HttpStatusCode SendJobMapping(JobMappingVM jobMapping)
        {
            var postTask = client.PostAsJsonAsync<JobMappingVM>(request + "SendJobMapping", jobMapping);
            postTask.Wait();

            var result = postTask.Result;

            return result.StatusCode;
        }
        
        public HttpStatusCode SendInterview(SendInterviewVM interviewVM)
        {
            var postTask = client.PostAsJsonAsync<SendInterviewVM>(request + "SendInterview", interviewVM);
            postTask.Wait();

            var result = postTask.Result;

            return result.StatusCode;
        }
        
        public HttpStatusCode SendInterviewResult(SendInterviewVM interviewVM)
        {
            var postTask = client.PostAsJsonAsync<SendInterviewVM>(request + "SendInterviewResult", interviewVM);
            postTask.Wait();

            var result = postTask.Result;

            return result.StatusCode;
        }
    }
}
