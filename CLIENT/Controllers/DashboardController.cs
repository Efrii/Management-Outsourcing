using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CLIENT.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {

        [Authorize(Roles = "MANAGER")]
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }

        [Authorize(Roles = "CLIENT")]
        [HttpGet]
        public ActionResult Company()
        {
            return View("Company");
        }

        [Authorize(Roles = "EMPLOYEEBOOTCAMP")]
        [HttpGet]
        public ActionResult Bootcamp()
        {
            return View("Bootcamp");
        }

        [Authorize(Roles = "TRAINNER")]
        [HttpGet]
        public ActionResult Trainner()
        {
            return View("Trainner");
        }

        [Authorize(Roles = "OPERASIONAL")]
        [HttpGet]
        public ActionResult Operasional()
        {
            return View("Operasional");
        }

        [AllowAnonymous]
        [HttpGet("404")]
        public ActionResult NFound()
        {
            return View("404");
        }

        [AllowAnonymous]
        [HttpGet("403")]
        public ActionResult Forbiiden()
        {
            return View("403");
        }

        [AllowAnonymous]
        [HttpGet("401")]
        public ActionResult Unauth()
        {
            return View("401");
        }

        [AllowAnonymous]
        [HttpGet("405")]
        public ActionResult ServerError()
        {
            return View("405");
        }
    }
}
