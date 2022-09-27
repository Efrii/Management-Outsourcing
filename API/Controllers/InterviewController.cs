using System;
using System.Collections.Generic;
using API.Base;
using API.Models;
using API.Repositories.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewController : BaseController<Interview, InterviewRepository, int>
    {
        InterviewRepository interviewRepository;

        public InterviewController(InterviewRepository repository) : base(repository)
        {
            this.interviewRepository = repository;
        }

        [Route("GetJoin")]
        [HttpGet]
        public ActionResult<List<Interview>> GetAll()
        {
            var result = interviewRepository.GetJoin();

            if (result != null)
                return Ok(new { status = 200, message = "Success Get All Data", data = result });
            else
                return NotFound(new { status = 404, message = "Data Not Found" });
        }

        [HttpGet("GetInterviewByIdEmp/{id}")]
        public ActionResult<List<Interview>> GetInterviewByIdEmp(int id)
        {
            var result = interviewRepository.GetInterviewsByIdEmp(id);

            if (result != null)
                return Ok(new { status = 200, message = "Success Get All Data", data = result });
            else
                return NotFound(new { status = 404, message = "Data Not Found" });
        }

        [HttpGet("GetInterviewsByIdInter/{id}")]
        public ActionResult<Interview> GetInterviewsByIdInter(int id)
        {
            var result = interviewRepository.GetInterviewsByIdInter(id);

            if (result != null)
                return Ok(new { status = 200, message = "Success Get All Data", data = result });
            else
                return NotFound(new { status = 404, message = "Data Not Found" });
        }

        [HttpGet("GetInterviewByIdCompany/{id}")]
        public ActionResult<List<Interview>> GetInterviewByIdCompany(int id)
        {
            var result = interviewRepository.GetInterviewsByIdCompany(id);

            if (result != null)
                return Ok(new { status = 200, message = "Success Get All Data", data = result });
            else
                return NotFound(new { status = 404, message = "Data Not Found" });
        }

        [HttpPost("IsDoneInterview")]
        public ActionResult IsDoneInterview([FromBody] IsDoneInterviewVM done)
        {
            var result = interviewRepository.IsDoneInterview(done);

            if (result == 1)
            {
                return Ok(new { status = 200, data = result });
            }
            else
            {
                return BadRequest(new { status = 400, message = "Insert Score is invalid" });
            }
        }

        [HttpPost("IsAcceptmentInterview")]
        public ActionResult IsAcceptmentInterview([FromBody] IsAcceptmentInterviewVM done)
        {
            var result = interviewRepository.IsAcceptmentInterview(done);

            if (result == 1)
            {
                return Ok(new { status = 200, data = result });
            }
            else
            {
                return BadRequest(new { status = 400, message = "Insert Score is invalid" });
            }
        }
    }
}
