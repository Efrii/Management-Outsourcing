using System;
using API.Models;

namespace CLIENT.Repositories.Data
{
    public class ClassRepository : GenericRepository<Class, int>
    {
        public ClassRepository() : base("Class/")
        {

        }
    }
}
