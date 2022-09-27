using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using API.Context;
using API.Models;
using API.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace API.Repositories.Data
{
    public class CVRepository : GenericRepository<CV, int>
    {
        MyContext myContext;

        public CVRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public CV GetCvById(int id)
        {
            var data = myContext.CVs
                .Include(x => x.Employees)
                .Include(x => x.Companys)
                .Where(x => x.Employees.Id_employee == id)
                .SingleOrDefault();

            return data;
        }

        public List<CV> GetJoinByIdClass(int id)
        {
            var data = myContext.CVs
                .Include(x => x.Employees)
                .Include(x => x.Companys)
                .Where(x => x.Employees.Class.Id_class == id)
                .ToList();

            return data;
        }

        public List<CV> GetCvByCompany(int id)
        {
            var data = myContext.CVs
                .Include(x => x.Employees)
                .Include(x => x.Companys)
                .Where(x => x.Id_company == id)
                .Where(x => x.Is_check == true)
                .ToList();

            return data;
        }

        public int UpdateCv(CV cV)
        {
            var cv = myContext.Set<CV>().Find(cV.Id_cv);
            cv.Id_employee = cV.Id_cv;
            myContext.Entry(cv).State = EntityState.Modified;
            myContext.SaveChanges();

            return 1;
        }

        // Update Check CV
        public int IsCheck(CvVM cV)
        {
            var cv = myContext.Set<CV>().Find(cV.Id_cv);
            cv.Is_check = cV.Is_check;
            myContext.Entry(cv).State = EntityState.Modified;
            myContext.SaveChanges();

            return 1;
        }
    }
}
