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
    public class ScoreController : BaseController<Score, ScoreRepository, int>
    {

        ScoreRepository ScoreRepository;

        public ScoreController(ScoreRepository repository) : base(repository)
        {
            this.ScoreRepository = repository;
        }

        [Route("GetJoin")]
        [HttpGet]
        public ActionResult<List<Score>> GetAll()
        {
            var result = ScoreRepository.GetJoin();

            if (result != null)
                return Ok(new { status = 200, message = "Success Get All Data", data = result });
            else
                return NotFound(new { status = 404, message = "Data Not Found" });
        }

        [HttpGet("data/{idkey}")]
        public ActionResult<Employee> GetJoinId(int idkey)
        {
            var result = ScoreRepository.GetJoinID(idkey);

            if (result != null)
                return Ok(new { status = 200, message = "Success Get All Data", data = result });
            else
                return NotFound(new { status = 404, message = "Data Not Found" });
        }

        [HttpGet("GetJoinClassId/{id}")]
        public ActionResult<Score> GetJoinClassId(int id)
        {
            var result = ScoreRepository.GetJoinClassId(id);

            if (result != null)
                return Ok(new { status = 200, message = "Success Get All Data", data = result });
            else
                return NotFound(new { status = 404, message = "Data Not Found" });
        }

        [HttpGet("GetJoinIdEmployee/{id}")]
        public ActionResult<Score> GetJoinIdEmployee(int id)
        {
            var result = ScoreRepository.GetJoinIdEmployee(id);

            if (result != null)
                return Ok(new { status = 200, message = "Success Get All Data", data = result });
            else
                return NotFound(new { status = 404, message = "Data Not Found" });
        }

        [HttpPost("PostScore")]
        public ActionResult Post([FromBody] ScoreVM score)
        {
            var result = ScoreRepository.PostScore(score);

            if (result == 1)
            {
                return Ok(new { status = 200, data = result });
            }
            else if (result == 2)
            {
                return Conflict(new { status = 409, message = "Employee Sudah Memiliki Nilai" });
            }
            else
            {
                return BadRequest(new { status = 400, message = "Insert Score is invalid" });
            }
        }

        [HttpPut("PutScore")]
        public ActionResult PutScore([FromBody] ScoreVM score)
        {
            var result = ScoreRepository.PutScore(score);

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
