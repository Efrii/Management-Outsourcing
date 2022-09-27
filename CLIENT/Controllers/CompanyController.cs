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
    public class CompanyController : BaseController<Company, CompanyRepository, int>
    {
        CompanyRepository CompanyRepository;

        public CompanyController(CompanyRepository companyRepository) : base(companyRepository)
        {
            this.CompanyRepository = companyRepository;
        }

        [Authorize(Roles = "CLIENT")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
