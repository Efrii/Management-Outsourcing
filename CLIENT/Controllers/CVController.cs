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
    public class CVController : BaseController<CV, CVRepository, int>
    {
        CVRepository CVRepository;

        public CVController(CVRepository repository) : base(repository)
        {
            this.CVRepository = repository;
        }

        [Authorize(Roles = "EMPLOYEEBOOTCAMP")]
        [Route("uploadcv")]
        [HttpPost]
        public JsonResult UploadCV([FromForm] CV cV)
        {
            var result = CVRepository.UploadCV(cV);

            if (result != 0)
            {
                return Json(result);
            }
            else
            {
                return Json(result);
            }
        }

        [Authorize(Roles = "EMPLOYEEBOOTCAMP")]
        [Route("putcv")]
        [HttpPut]
        public JsonResult PutCV([FromForm] CV cV)
        {
            var result = CVRepository.PutCV(cV);

            if (result != 0)
            {
                return Json(result);
            }
            else
            {
                return Json(result);
            }
        }

        [Authorize(Roles = "OPERASIONAL")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "EMPLOYEEBOOTCAMP")]
        [HttpGet("MyCv")]
        public ActionResult MyCv()
        {
            return View();
        }

        [Authorize(Roles = "CLIENT")]
        [HttpGet("Cvin")]
        public ActionResult Cvin()
        {
            return View();
        }

        [HttpGet("GetCvBy/{id}")]
        public async Task<JsonResult> GetJoinID(int id)
        {
            var data = await CVRepository.GetCvById(id);

            if (data != null)
            {
                return Json(data);
            }

            return Json(data);
        }

        [HttpGet("GetJoinByIdClass/{id}")]
        public async Task<JsonResult> GetJoinByIdClass(int id)
        {
            var data = await CVRepository.GetJoinByIdClass(id);

            if (data != null)
            {
                return Json(data);
            }

            return Json(data);
        }

        [HttpGet("GetCvByCompany/{id}")]
        public async Task<JsonResult> GetCvByCompany(int id)
        {
            var data = await CVRepository.GetCvByCompany(id);

            if (data != null)
            {
                return Json(data);
            }

            return Json(data);
        }

        [Authorize(Roles = "OPERASIONAL")]
        [HttpPost("IsCheck")]
        public JsonResult IsCheck([FromBody] CvVM cvVM)
        {
            var result = CVRepository.IsCheck(cvVM);

            return Json(result);
        }
    }
}
