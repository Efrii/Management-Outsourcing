using System;
using System.Threading.Tasks;
using API.Models;
using API.ViewModels;
using CLIENT.Base;
using CLIENT.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLIENT.Controllers
{
    [Authorize]
    public class EmployeeController : BaseController<Employee, EmployeeRepository, int>
    {
        EmployeeRepository employeeRepository;

        public EmployeeController(EmployeeRepository repository) : base(repository)
        {
            this.employeeRepository = repository;
        }

        // untuk get di score yang merupakan role employe
        [HttpGet("GetEmployeeByRole")]
        public async Task<JsonResult> GetEmployeeByRole()
        {
            var data = await employeeRepository.GetEmployeeByRole();

            if (data != null)
            {
                return Json(data);
            }

            return Json(data);
        }

        [HttpGet("GetDetailClass/{id}")]
        public async Task<JsonResult> GetDetailClass(int id)
        {
            var data = await employeeRepository.GetDetailClass(id);

            if (data != null)
            {
                return Json(data);
            }

            return Json(data);
        }

        [HttpGet("GetTrainner/{id}")]
        public async Task<JsonResult> GetTrainner(int id)
        {
            var data = await employeeRepository.GetTrainner(id);

            if (data != null)
            {
                return Json(data);
            }

            return Json(data);
        }

        [Authorize(Roles = "OPERASIONAL")]
        [HttpPost("SendJobMapping")]
        public JsonResult SendJobMapping([FromBody] JobMappingVM jobMapping)
        {
            var result = employeeRepository.SendJobMapping(jobMapping);

            return Json(result);
        }

        [Authorize(Roles = "OPERASIONAL")]
        [HttpPost("SendInterview")]
        public JsonResult SendInterview([FromBody] SendInterviewVM interviewVM)
        {
            var result = employeeRepository.SendInterview(interviewVM);

            return Json(result);
        }

        [Authorize(Roles = "OPERASIONAL")]
        [HttpPost("SendInterviewResult")]
        public JsonResult SendInterviewResult([FromBody] SendInterviewVM interviewVM)
        {
            var result = employeeRepository.SendInterviewResult(interviewVM);

            return Json(result);
        }
    }
}
