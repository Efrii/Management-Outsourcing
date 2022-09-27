using System;
using System.Collections.Generic;
using System.Linq;
using API.Context;
using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class AcceptmentRepository : GenericRepository<Acceptment, int>
    {
        MyContext myContext;

        public AcceptmentRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public List<Acceptment> GetJoin()
        {
            var data = myContext.Acceptments
                .Include(x => x.Employee)
                .Include(x => x.Employee.Class)
                .Where(x => x.Employee.Is_place != false)
                .ToList();

            return data;
        }

        public Acceptment GetJoinIdEmployee(int id)
        {
            var data = myContext.Acceptments
                .Include(x => x.Employee)
                .Where(x => x.Employee.Id_employee == id)
                .SingleOrDefault();

            return data;
        }

        public bool CekAcceptment(int id)
        {
            var data = myContext.Acceptments.FirstOrDefault(x => x.Employee.Id_employee == id);

            return data != null;
        }

        // Post Acceptment dengan Update Id_accetp di emplpye
        public int PostAcceptment(AcceptmentVM acceptment)
        {
            var check = CekAcceptment(acceptment.Id_employee);

            if(check == false)
            {
                var acp = new Acceptment
                {
                    Id_employee = acceptment.Id_employee,
                    Name_Company = acceptment.Name_Company,
                    Job_position = acceptment.Job_position
                };

                acceptment.Id_employee = acp.Id_employee;
                myContext.Acceptments.Add(acp);
                myContext.SaveChanges();

                var employee = myContext.Set<Employee>().Find(acceptment.Id_employee);
                employee.Is_place = acceptment.Is_place;
                myContext.Entry(employee).State = EntityState.Modified;
                myContext.SaveChanges();

                return 1;
            }
            else if (check == true)
            {
                return 2;
            }

            return 0;


        }
    }
}
