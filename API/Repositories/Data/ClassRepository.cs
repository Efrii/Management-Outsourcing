using System;
using System.Collections.Generic;
using System.Linq;
using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class ClassRepository : GenericRepository<Class, int>
    {
        MyContext myContext;

        public ClassRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

    }
}
