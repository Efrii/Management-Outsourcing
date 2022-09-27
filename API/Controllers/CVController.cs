using System;
using System.Collections.Generic;
using System.IO;
using API.Base;
using API.Models;
using API.Repositories.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class CVController : BaseController<CV, CVRepository, int>
    {
        CVRepository CVRepository;

        public CVController(CVRepository repository) : base(repository)
        {
            this.CVRepository = repository;
        }

        [HttpGet("GetCv/{id}")]
        public ActionResult<CV> GetAll(int id)
        {
            var result = CVRepository.GetCvById(id);

            if (result != null)
                return Ok(new { status = 200, message = "Success Get All Data", data = result });
            else
                return NotFound(new { status = 404, message = "Data Not Found" });
        }

        [HttpGet("GetJoinByIdClass/{id}")]
        public ActionResult<List<CV>> GetJoin(int id)
        {
            var result = CVRepository.GetJoinByIdClass(id);
             
            if (result != null)
                return Ok(new { status = 200, message = "Success Get All Data", data = result });
            else
                return NotFound(new { status = 404, message = "Data Not Found" });
        }

        [HttpGet("GetCvByCompany/{id}")]
        public ActionResult<CV> GetCvByCompany(int id)
        {
            var result = CVRepository.GetCvByCompany(id);

            if (result != null)
                return Ok(new { status = 200, message = "Success Get All Data", data = result });
            else
                return NotFound(new { status = 404, message = "Data Not Found" });
        }

        [HttpPost("IsCheck")]
        public ActionResult IsCheck([FromBody] CvVM cvVM)
        {
            var result = CVRepository.IsCheck(cvVM);

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
