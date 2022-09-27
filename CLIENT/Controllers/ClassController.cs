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
    public class ClassController : BaseController<Class, ClassRepository, int>
    {
        public ClassController(ClassRepository repository) : base(repository)
        {

        }

        [Authorize(Roles = "TRAINNER")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
