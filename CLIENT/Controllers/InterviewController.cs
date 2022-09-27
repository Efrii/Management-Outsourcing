using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.ViewModels;
using CLIENT.Base;
using CLIENT.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CLIENT.Controllers
{
    [Authorize]
    public class InterviewController : BaseController<Interview, InterviewRepository, int>
    {
        InterviewRepository InterviewRepository;

        public InterviewController(InterviewRepository interviewRepository) : base(interviewRepository)
        {
            this.InterviewRepository = interviewRepository;
        }

        [HttpGet("GetJoin")]
        public async Task<JsonResult> GetJoin()
        {
            var data = await InterviewRepository.GetJoin();

            if (data != null)
            {
                return Json(data);
            }

            return Json(data);
        }

        [HttpGet("GetInterviewById/{id}")]
        public async Task<JsonResult> GetInterviewById(int id)
        {
            var data = await InterviewRepository.GetInterviewByIdEmp(id);

            if (data != null)
            {
                return Json(data);
            }

            return Json(data);
        }

        [HttpGet("GetInterviewsByIdInter/{id}")]
        public async Task<JsonResult> GetInterviewsByIdInter(int id)
        {
            var data = await InterviewRepository.GetInterviewsByIdInter(id);

            if (data != null)
            {
                return Json(data);
            }

            return Json(data);
        }

        [HttpGet("GetInterviewByIdCompany/{id}")]
        public async Task<JsonResult> GetInterviewByIdCompany(int id)
        {
            var data = await InterviewRepository.GetInterviewByIdCompany(id);

            if (data != null)
            {
                return Json(data);
            }

            return Json(data);
        }

        [HttpPost("IsDoneInterview")]
        public JsonResult IsDoneInterview([FromBody] IsDoneInterviewVM done)
        {
            var result = InterviewRepository.IsDoneInterview(done);

            return Json(result);
        }

        [HttpPost("IsAcceptmentInterview")]
        public JsonResult IsAcceptmentInterview([FromBody] IsAcceptmentInterviewVM done)
        {
            var result = InterviewRepository.IsAcceptmentInterview(done);

            return Json(result);
        }

        [Authorize(Roles = "OPERASIONAL")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = "EMPLOYEEBOOTCAMP")]
        [HttpGet("Schedule")]
        public ActionResult Schedule()
        {
            return View();
        }

        [Authorize(Roles = "CLIENT")]
        [HttpGet("Company")]
        public ActionResult Company()
        {
            return View();
        }

        [Authorize(Roles = "OPERASIONAL")]
        [HttpGet("Result")]
        public ActionResult Result()
        {
            return View();
        }
    }
}
