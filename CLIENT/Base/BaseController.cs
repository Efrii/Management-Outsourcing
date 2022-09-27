using System;
using System.Threading.Tasks;
using CLIENT.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CLIENT.Base
{
    [Route("/[controller]")]
    [Controller]
    public class BaseController<TModel, TRepository, TPrimaryKey> : Controller
        where TModel : class
        where TRepository : IGeneralRepository<TModel, TPrimaryKey>
    {
        TRepository Repository;

        public BaseController(TRepository repository)
        {
            this.Repository = repository;
        }

        [HttpGet("Get")]
        public async Task<JsonResult> Get()
        {
            var result = await Repository.Get();

            return Json(result);
        }

        [HttpGet("Get/{Id}")]
        public async Task<JsonResult> Get(TPrimaryKey Id)
        {
            var result = await Repository.Get(Id);

            return Json(result);
        }

        [HttpPost("Add")]
        [ValidateAntiForgeryToken]
        public JsonResult Post([FromBody] TModel model)
        {
            var result = Repository.Post(model);

            return Json(result);
        }

        [HttpPut("Edit")]
        public JsonResult Put([FromBody] TModel model)
        {
            var result = Repository.Put(model);

            return Json(result);
        }

        [HttpDelete("Delete/{Id}")]
        public JsonResult Delete(TPrimaryKey Id)
        {
            var result = Repository.Delete(Id);

            return Json(result);
        }
    }
}
