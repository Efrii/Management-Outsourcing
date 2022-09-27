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
    public class AcceptmentController : BaseController<Acceptment, AcceptmentRepository, int>
    {
        AcceptmentRepository acceptmentRepository;

        public AcceptmentController(AcceptmentRepository repository) : base(repository)
        {
            this.acceptmentRepository = repository;
        }

        [HttpGet("GetJoin")]
        public ActionResult<List<Acceptment>> GetJoin()
        {
            var result = acceptmentRepository.GetJoin();

            if (result != null)
                return Ok(new { status = 200, message = "Success Get All Data", data = result });
            else
                return NotFound(new { status = 404, message = "Data Not Found" });
        }

        [HttpPost("PostAcceptment")]
        public ActionResult PostAcceptment([FromBody] AcceptmentVM acceptment)
        {
            var result = acceptmentRepository.PostAcceptment(acceptment);

            if (result == 1)
            {
                return Ok(new { status = 200, data = result });
            }
            else if (result == 2)
            {
                return Conflict(new { status = 409, message = "Data Sudah Di Inputkan" });
            }
            else
            {
                return BadRequest(new { status = 400, message = "Request is invalid" });
            }
        }

        [HttpGet("GetJoinIdEmployee/{id}")]
        public ActionResult<Score> GetJoinIdEmployee(int id)
        {
            var result = acceptmentRepository.GetJoinIdEmployee(id);

            if (result != null)
                return Ok(new { status = 200, message = "Success Get All Data", data = result });
            else
                return NotFound(new { status = 404, message = "Data Not Found" });
        }
    }
}
