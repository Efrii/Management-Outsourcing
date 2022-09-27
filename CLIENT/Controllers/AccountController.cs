using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API.Models;
using API.Services;
using API.ViewModels;
using CLIENT.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CLIENT.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        AccountRepository accountRepository;
        IConfiguration config;
        Uri baseAddress = new Uri("https://localhost:5001/api/");
        HttpClient client;

        public AccountController(AccountRepository accountRepository, IConfiguration config)
        {
            this.accountRepository = accountRepository;
            this.config = config;
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult LoginCompany()
        {
            return View("LoginCompany");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public JsonResult Auth([FromBody] User login)
        {
            var postTask = client.PostAsJsonAsync<User>("Account/Login", login);
            postTask.Wait();

            var jwt = new JwtService(config);

            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync().Result;
                var parsedObject = JObject.Parse(readTask);
                //var token = parsedObject["token"].ToString();

                var d = parsedObject["data"].ToString();
                var datas = JsonConvert.DeserializeObject<User>(d);

                var idToken = jwt.GenerateSecurityToken(datas);

                List<RoleVM> userRole = new List<RoleVM>();
                foreach (var item in datas.UserRoles)
                {
                    RoleVM role = new RoleVM()
                    {
                        RoleEmployee = item.Role.Name_role
                    };

                    userRole.Add(role);
                }

                // Set Session 
                HttpContext.Session.SetString("Role", JsonConvert.SerializeObject(userRole));
                HttpContext.Session.SetString("Id", datas.Id_employee .ToString());
                HttpContext.Session.SetString("Username", datas.Employees.Name_employee);
                HttpContext.Session.SetString("Email", datas.Employees.Email_employee);
                HttpContext.Session.SetString("Token", idToken);

                return Json(new
                {
                    status = result.StatusCode,
                    data = new
                    {
                        id = datas.Id_employee,
                        username = datas.Username,
                        email = datas.Employees.Email_employee,
                        role = userRole
                    },
                    token = idToken
                });

            }
            else
            {
                return Json(new { status = result.StatusCode, message = result.RequestMessage });
            }

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public JsonResult AuthCompany([FromBody] User login)
        {
            var postTask = client.PostAsJsonAsync<User>("Account/Login/Company", login);
            postTask.Wait();

            var jwt = new JwtService(config);

            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync().Result;
                var parsedObject = JObject.Parse(readTask);
                //var token = parsedObject["token"].ToString();

                var d = parsedObject["data"].ToString();
                var datas = JsonConvert.DeserializeObject<User>(d);

                var idToken = jwt.GenerateSecurityToken(datas);

                List<RoleVM> userRole = new List<RoleVM>();
                foreach (var item in datas.UserRoles)
                {
                    RoleVM role = new RoleVM()
                    {
                        RoleEmployee = item.Role.Name_role
                    };

                    userRole.Add(role);
                }

                // Set Session 
                HttpContext.Session.SetString("Role", JsonConvert.SerializeObject(userRole));
                HttpContext.Session.SetString("Id", datas.Id_company.ToString());
                HttpContext.Session.SetString("Username", datas.Companys.Name_company);
                HttpContext.Session.SetString("Email", datas.Companys.Email_company);
                HttpContext.Session.SetString("Token", idToken);

                return Json(new
                {
                    status = result.StatusCode,
                    data = new
                    {
                        id = datas.Id_company,
                        username = datas.Username,
                        email = datas.Companys.Email_company,
                        role = userRole
                    },
                    token = idToken
                });

            }
            else
            {
                return Json(new { status = result.StatusCode, message = result.RequestMessage });
            }

        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult LogoutCompany()
        {
            HttpContext.Session.Clear();

            return Redirect("LoginCompany");
        }

        [HttpPost("Account/Register/Employee")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public JsonResult RegisterEmployee([FromBody] RegisterEmployeeVM register)
        {
            var data = accountRepository.RegisterEmpoyee(register);

            return Json(data);
        }

        [HttpPost("Account/Register/Company")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public JsonResult RegisterCompany([FromBody] RegisterCompanyVM register)
        {
            var data = accountRepository.RegisterCompany(register);

            return Json(data);
        }

        //[HttpPost]
        //public JsonResult SendOTP([FromBody] ResetpassVM reset)
        //{
        //    var data = accountRepository.SendOTP(reset);

        //    return Json(data);
        //}
    }
}
