using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Base;
using API.Models;
using API.Repositories.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class EmployeeController : BaseController<Employee, EmployeeRepository, int>
    {
        EmployeeRepository employeeRepository;

        public EmployeeController(EmployeeRepository repository) : base(repository)
        {
            this.employeeRepository = repository;
        }

        [HttpGet("GetEmployeeByRole/{role}")]
        public ActionResult<List<UserRole>> GetAll(string role)
        {
            var result = employeeRepository.GetEmployeByRole(role);

            if (result != null)
                return Ok(new { status = 200, message = "Success Get All Data", data = result });
            else
                return NotFound(new { status = 404, message = "Data Not Found" });
        }

        [HttpGet("GetDetailClass/{id}")]
        public ActionResult<List<Employee>> GetDetailClass(int id)
        {
            var result = employeeRepository.GetDetailClass(id);

            if (result != null)
                return Ok(new { status = 200, message = "Success Get All Data", data = result });
            else
                return NotFound(new { status = 404, message = "Data Not Found" });
        }

        [HttpGet("GetTrainner/{id}")]
        public ActionResult<List<GetTrainnerVM>> GetTrainner(int id)
        {
            var result = employeeRepository.GetTrainner(id);

            if (result != null)
                return Ok(new { status = 200, message = "Success Get All Data", data = result });
            else
                return NotFound(new { status = 404, message = "Data Not Found" });
        }

        //[Authorize(Roles = "OPERASIONAL")]
        [Route("SendInterview")]
        [HttpPost]
        public ActionResult<int> SendInterview(SendInterviewVM interviewVM)
        {
            var result = employeeRepository.SendInterview(interviewVM);

            if (result > 0)
            {
                return Ok(new { status = 200, data = result });
            }
            else
            {
                return BadRequest(new { status = 400, message = "Gagal Kirim Email" });
            }
        }

        //[Authorize(Roles = "OPERASIONAL")]
        [Route("SendInterviewResult")]
        [HttpPost]
        public ActionResult<int> SendInterviewResult(SendInterviewVM interviewVM)
        {
            var result = employeeRepository.SendInterviewResult(interviewVM);

            if (result > 0)
            {
                return Ok(new { status = 200, data = result });
            }
            else
            {
                return BadRequest(new { status = 400, message = "Gagal Kirim Email" });
            }
        }

        [Route("SendJobMapping")]
        [HttpPost]
        public ActionResult<int> SendJobMapping(JobMappingVM jobMapping)
        {
            var result = employeeRepository.JobMapping(jobMapping);

            if (result > 0)
            {
                return Ok(new { status = 200, data = result });
            }
            else
            {
                return BadRequest(new { status = 400, message = "Gagal Kirim Email" });
            }
        }
    }
}
