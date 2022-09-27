using System;
using API.Models;

namespace CLIENT.Repositories.Data
{
    public class CompanyRepository : GenericRepository<Company, int>
    {
        public CompanyRepository() : base("Company/")
        {

        }
    }
}
