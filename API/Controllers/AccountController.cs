using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repositories.Data;
using API.Services;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        AccountRepository accountRepository;
        IConfiguration config;

        public AccountController(AccountRepository accountRepository, IConfiguration config)
        {
            this.accountRepository = accountRepository;
            this.config = config;
        }

        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Post([FromBody] LoginVM login)
        {
            var result = accountRepository.GetLogin(login.Username, login.Password);
            //var jwt = new JwtService(config);
            if (result != null)
            {
                //var idToken = jwt.GenerateSecurityToken(result);
                return Ok(new { status = 200, data = result});
            }
            else
            {
                return BadRequest(new { status = 400, message = "Username or Password is invalid" });
            }
        }

        [Route("Login/Company")]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult LoginCompany([FromBody] LoginVM login)
        {
            var result = accountRepository.GetLoginCompany(login.Username, login.Password);
            //var jwt = new JwtService(config);
            if (result != null)
            {
                //var idToken = jwt.GenerateSecurityToken(result);
                return Ok(new { status = 200, data = result });
            }
            else
            {
                return BadRequest(new { status = 400, message = "Username or Password is invalid" });
            }
        }

        [Route("Register/Employee")]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult<int> Register([FromBody] RegisterEmployeeVM register)
        {
            var result = accountRepository.RegisterEmployee(register);

            if (result == 1)
            {
                return Ok(new { status = 200, data = result });
            }
            else if (result == 2)
            {
                return Conflict(new { status = 409, message = "Email Sudah Digunakan" });
            }
            else
            {
                return BadRequest(new { status = 400, message = "Request is invalid" });
            }
        }

        [Route("Register/Company")]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult<int> RegisterCompany([FromBody] RegisterCompanyVM register)
        {
            var result = accountRepository.RegisterCompany(register);

            if (result == 1)
            {
                return Ok(new { status = 200, data = result });
            }
            else if (result == 2)
            {
                return Conflict(new { status = 409, message = "Email Sudah Digunakan" });
            }
            else
            {
                return BadRequest(new { status = 400, message = "Request is invalid" });
            }
        }
    }
}
