using System;
using System.Collections.Generic;
using System.Linq;
using API.Context;
using API.Repositories.Interface;

namespace API.Repositories
{
    public class GenericRepository<TModel, TPrimaryKey> : IGeneralRepository<TModel, TPrimaryKey>
        where TModel : class
    {
        MyContext myContext;

        public GenericRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public List<TModel> Get()
        {
            var data = myContext.Set<TModel>().ToList();
            return data;
        }

        public TModel Get(TPrimaryKey Id)
        {
            var data = myContext.Set<TModel>().Find(Id);

            return data;
        }

        public int Post(TModel model)
        {
            myContext.Set<TModel>().Add(model);
            var data = myContext.SaveChanges();

            return data;
        }

        public int Put(TModel model)
        {
            myContext.Set<TModel>().Update(model);
            var data = myContext.SaveChanges();

            return data;
        }

        public int Delete(TPrimaryKey Id)
        {
            var target = myContext.Set<TModel>().Find(Id);
            myContext.Set<TModel>().Remove(target);
            var data = myContext.SaveChanges();

            return data;
        }
    }
}
