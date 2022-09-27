using System;
using System.Collections.Generic;
using System.Linq;
using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class JobsRepository : GenericRepository<Jobs, int>
    {
        MyContext myContext;

        public JobsRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public List<Jobs> GetJoinID(int id)
        {
            var data = myContext.Jobs
                .Include(x => x.Companys)
                .Where(x => x.Id_company == id)
                .ToList();

            return data;
        }

        public List<Jobs> GetJoinName(string name)
        {
            var data = myContext.Jobs
                .Include(x => x.Companys)
                .Where(x => x.Companys.Name_company == name)
                .ToList();

            return data;
        }
    }
}
