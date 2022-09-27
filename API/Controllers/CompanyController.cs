using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Base;
using API.Models;
using API.Repositories.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class CompanyController : BaseController<Company, CompanyRepository, int>
    {
        CompanyRepository companyRepository;

        public CompanyController(CompanyRepository repository) : base(repository)
        {
            this.companyRepository = repository;
        }

        [Route("SendInterview")]
        [HttpPost]
        public ActionResult<int> SendInterview(CompanyVM company)
        {
            var result = companyRepository.SendInterview(company.Email);

            if (result > 0)
            {
                return Ok(new { status = 200, data = result });
            }
            else
            {
                return BadRequest(new { status = 400, message = "Gagal Kirim Email" });
            }
        }

        [Route("SendInterviewResult")]
        [HttpPost]
        public ActionResult<int> SendInterviewResult(CompanyVM company)
        {
            var result = companyRepository.SendInterviewResult(company.Email);

            if (result > 0)
            {
                return Ok(new { status = 200, data = result });
            }
            else
            {
                return BadRequest(new { status = 400, message = "Gagal Kirim Email" });
            }
        }
    }
}
