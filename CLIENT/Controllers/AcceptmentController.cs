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
    public class AcceptmentController : BaseController<Acceptment, AcceptmentRepository, int>
    {
        AcceptmentRepository acceptmentRepository;

        public AcceptmentController(AcceptmentRepository repository) : base(repository)
        {
            this.acceptmentRepository = repository;
        }

        [Authorize(Roles = "OPERASIONAL")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "CLIENT")]
        [HttpGet("Company")]
        public ActionResult Company()
        {
            return View();
        }

        [HttpGet("GetJoin")]
        public async Task<JsonResult> GetJoin()
        {
            var data = await acceptmentRepository.GetJoin();

            if (data != null)
            {
                return Json(data);
            }

            return Json(data);
        }

        [HttpPost("PostAcceptment")]
        //[ValidateAntiForgeryToken]
        public JsonResult PostAcceptment([FromBody] AcceptmentVM acceptment)
        {
            var result = acceptmentRepository.PostAcceptment(acceptment);

            return Json(result);
        }

        [HttpGet("GetJoinIdEmployee/{id}")]
        public async Task<JsonResult> GetJoinIdEmployee(int id)
        {
            var data = await acceptmentRepository.GetJoinIdEmployee(id);

            if (data != null)
            {
                return Json(data);
            }

            return Json(data);
        }
    }
}
