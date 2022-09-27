using System;
using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ClassController : BaseController<Class, ClassRepository, int>
    {
        public ClassController(ClassRepository repository) : base(repository)
        {

        }
    }
}
