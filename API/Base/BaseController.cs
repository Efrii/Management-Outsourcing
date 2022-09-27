using System;
using System.Collections.Generic;
using System.Linq;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TModel, TRepository, TPrimaryKey> : ControllerBase
        where TModel : class
        where TRepository : IGeneralRepository<TModel, TPrimaryKey>
    {
        TRepository Repository;

        public BaseController(TRepository repository)
        {
            this.Repository = repository;
        }

        // GET: api/values
        [HttpGet]
        public ActionResult<List<TModel>> Get()
        {
            var result = Repository.Get();

            if (result.Count() != 0)
                return Ok(new { status = 200, message = "Success Get All Data", data = result });
            else
                return NotFound(new { status = 404, message = "Data Not Found" });
        }

        // GET api/values/5
        [HttpGet("{Id}")]
        public ActionResult<TModel> Get(TPrimaryKey Id)
        {
            var result = Repository.Get(Id);

            if (result != null)
                return Ok(new { status = 200, message = "Success Get Data By Id", data = result });
            else
                return NotFound(new { status = 404, message = "Data Not Found" });
        }

        // POST api/values
        [HttpPost]
        public ActionResult<int> Post([FromBody] TModel model)
        {
            var result = Repository.Post(model);

            if (result > 0)
                return Ok(new { status = 200, data = result });
            else
                return BadRequest(new { status = 400, message = "Request is invalid" });
        }

        // PUT api/values/5
        [HttpPut]
        public ActionResult<int> Put(TModel model)
        {
            var result = Repository.Put(model);

            if (result > 0)
                return Ok(new { status = 200, data = result });
            else
                return BadRequest(new { status = 404, message = "Request is invalid" });
        }

        // DELETE api/values/5
        [Route("{Id}")]
        [HttpDelete]
        public ActionResult<int> Delete(TPrimaryKey Id)
        {
            var result = Repository.Delete(Id);

            if (result > 0)
                return Ok(new { status = 200, data = result });
            else
                return BadRequest(new { status = 400, message = "Request is invalid" });
        }
    }
}
