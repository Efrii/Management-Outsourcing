using System;
using System.Net;
using System.Net.Http;
using API.Models;
using API.ViewModels;
using CLIENT.Base;

namespace CLIENT.Repositories.Data
{
    public class AccountRepository : GenericRepository<User, int>
    {
        public AccountRepository() : base("Account/")
        {

        }

        public HttpStatusCode RegisterEmpoyee(RegisterEmployeeVM registerEmployee)
        {
            var postTask = client.PostAsJsonAsync<RegisterEmployeeVM>(request + "Register/Employee", registerEmployee);
            postTask.Wait();

            var result = postTask.Result;

            return result.StatusCode;
        }

        public HttpStatusCode RegisterCompany(RegisterCompanyVM registerCompany)
        {
            var postTask = client.PostAsJsonAsync<RegisterCompanyVM>(request + "Register/Company", registerCompany);
            postTask.Wait();

            var result = postTask.Result;

            return result.StatusCode;
        }

    }
}
