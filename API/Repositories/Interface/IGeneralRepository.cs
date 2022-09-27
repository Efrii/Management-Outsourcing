using System;
using System.Collections.Generic;

namespace API.Repositories.Interface
{
    public interface IGeneralRepository<TModel, TPrimaryKey>
        where TModel : class
    {
        List<TModel> Get();

        TModel Get(TPrimaryKey Id);

        int Post(TModel model);

        int Put(TModel model);

        int Delete(TPrimaryKey Id);
    }
}
