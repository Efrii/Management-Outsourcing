using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CLIENT.Repositories.Interface
{
    public interface IGeneralRepository<TModel, TPrimaryKey>
        where TModel : class
    {
        // GET
        Task<List<TModel>> Get();
        // GET Using Paramenter By ID (Primary Key)
        Task<TModel> Get(TPrimaryKey Id);
        // POST
        HttpStatusCode Post(TModel model);
        // PUT
        HttpStatusCode Put(TModel model);
        // DELETE
        HttpStatusCode Delete(TPrimaryKey Id);
    }
}
