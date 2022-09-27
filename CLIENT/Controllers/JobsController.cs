using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using CLIENT.Base;
using CLIENT.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CLIENT.Controllers
{
    [Authorize]
    public class JobsController : BaseController<Jobs, JobsRepository, int>
    {
        JobsRepository jobsRepository;

        public JobsController(JobsRepository repository) : base(repository)
        {
            this.jobsRepository = repository;
        }

        [HttpGet("GetJoinID/{id}")]
        public async Task<JsonResult> GetJoinID(int id)
        {
            var data = await jobsRepository.GetJoinID(id);

            if (data != null)
            {
                return Json(data);
            }

            return Json(data);
        }

        [HttpGet("GetJoinName/{name}")]
        public async Task<JsonResult> GetJoinName(string name)
        {
            var data = await jobsRepository.GetJoinName(name);

            if (data != null)
            {
                return Json(data);
            }

            return Json(data);
        }

        [Authorize(Roles = "CLIENT")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "OPERASIONAL")]
        [HttpGet("JobMapping")]
        public ActionResult JobMapping()
        {
            return View();
        }

    }
}
