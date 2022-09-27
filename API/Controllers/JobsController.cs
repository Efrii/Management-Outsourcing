using System;
using System.Collections.Generic;
using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : BaseController<Jobs, JobsRepository, int>
    {
        JobsRepository jobsRepository;

        public JobsController(JobsRepository repository) : base(repository)
        {
            this.jobsRepository = repository;
        }

        [Route("GetJoinID/{id}")]
        [HttpGet]
        public ActionResult<List<Jobs>> GetJoinID(int id)
        {
            var result = jobsRepository.GetJoinID(id);

            if (result != null)
                return Ok(new { status = 200, message = "Success Get All Data", data = result });
            else
                return NotFound(new { status = 404, message = "Data Not Found" });
        }

        [Route("GetJoinName/{name}")]
        [HttpGet]
        public ActionResult<List<Jobs>> GetJoinName(string name)
        {
            var result = jobsRepository.GetJoinName(name);

            if (result != null)
                return Ok(new { status = 200, message = "Success Get All Data", data = result });
            else
                return NotFound(new { status = 404, message = "Data Not Found" });
        }
    }
}
