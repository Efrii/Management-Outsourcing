using System;
using System.Collections.Generic;
using System.Linq;
using API.Context;
using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class InterviewRepository : GenericRepository<Interview, int>
    {

        MyContext myContext;

        public InterviewRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public List<Interview> GetJoin()
        {
            var data = myContext.Interviews
                .Include(x => x.Employees)
                .Include(x => x.Employees.Class)
                .Include(x => x.Companys)
                .ToList();

            return data;
        }

        public List<Interview> GetInterviewsByIdEmp(int id)
        {
            var data = myContext.Interviews
                .Include(x => x.Employees)
                .Include(x => x.Companys)
                .ThenInclude(x => x.Jobess)
                .Where(x => x.Id_employee == id)
                .ToList();

            return data;
        }

        public Interview GetInterviewsByIdInter(int id)
        {
            var data = myContext.Interviews
                .Include(x => x.Employees)
                .Include(x => x.Companys)
                .ThenInclude(x => x.Jobess)
                .Where(x => x.Id_interview == id)
                .SingleOrDefault();

            return data;
        }

        public List<Interview> GetInterviewsByIdCompany(int id)
        {
            var data = myContext.Interviews
                .Include(x => x.Employees)
                .Include(x => x.Companys)
                .ThenInclude(x => x.Jobess)
                .Where(x => x.Companys.Id_company == id)
                .ToList();

            return data;
        }

        public int IsDoneInterview(IsDoneInterviewVM done)
        {
            var interview = myContext.Set<Interview>().Find(done.Id_interview);
            interview.Is_done = done.Is_done;
            myContext.Entry(interview).State = EntityState.Modified;
            myContext.SaveChanges();

            return 1;
        }

        public int IsAcceptmentInterview(IsAcceptmentInterviewVM done)
        {
            var interview = myContext.Set<Interview>().Find(done.Id_interview);
            interview.Is_accepted = done.Is_accepted;
            myContext.Entry(interview).State = EntityState.Modified;
            myContext.SaveChanges();

            return 1;
        }

    }
}
