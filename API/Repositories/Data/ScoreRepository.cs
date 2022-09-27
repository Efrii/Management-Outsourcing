using System;
using System.Collections.Generic;
using System.Linq;
using API.Context;
using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class ScoreRepository : GenericRepository<Score, int>
    {
        MyContext myContext;

        public ScoreRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public List<Score> GetJoin()
        {
            var data = myContext.Scores
                .Include(x => x.Employees)
                .Include(x => x.Employees.Class)
                .ToList();

            return data;
        }

        public Score GetJoinID(int id)
        {
            var data = myContext.Scores
                .Include(x => x.Employees)
                .Include(x => x.Employees.Class)
                .Where(x => x.Id_score == id)
                .FirstOrDefault();

            return data;
        }

        public Score GetJoinIdEmployee(int id)
        {
            var data = myContext.Scores
                .Include(x => x.Employees)
                .Include(x => x.Employees.Class)
                .Where(x => x.Id_employee == id)
                .FirstOrDefault();

            return data;
        }

        public List<Score> GetJoinClassId(int id)
        {
            var data = myContext.Scores
                .Include(x => x.Employees)
                .Include(x => x.Employees.Class)
                .Where(x => x.Employees.Class.Id_class == id)
                .ToList();

            return data;
        }

        public bool CekEmployeIsScore(int score)
        {
            var data = myContext.Scores.FirstOrDefault(x => x.Id_employee == score);

            return data != null;
        }

        public int PostScore(ScoreVM score)
        {

            var cek = CekEmployeIsScore(score.Id_employee);

            if(cek == false)
            {
                var employee = myContext.Set<Employee>().Find(score.Id_employee);
                employee.Id_class = score.Id_class;
                employee.Id_trainner = score.Id_trainner;
                myContext.Entry(employee).State = EntityState.Modified;
                myContext.SaveChanges();

                var sco = new Score
                {
                    Id_employee = score.Id_employee,
                    Segment1 = score.Segment1,
                    Segment2 = score.Segment2,
                    Segment3 = score.Segment3,
                    Segment4 = score.Segment4,
                    Total = score.Total
                };

                score.Id_employee = sco.Id_employee;
                myContext.Scores.Add(sco);

                myContext.SaveChanges();

                return 1;
            }
            else
            {
                return 2;
            }
        }

        public int PutScore(ScoreVM score)
        {
            var employee = myContext.Set<Employee>().Find(score.Id_employee);
            employee.Id_class = score.Id_class;
            employee.Id_trainner = score.Id_trainner;
            myContext.Entry(employee).State = EntityState.Modified;
            myContext.SaveChanges();

            var scores = myContext.Set<Score>().Find(score.Id_score);
            scores.Id_employee = score.Id_employee;
            scores.Segment1 = score.Segment1;
            scores.Segment2 = score.Segment2;
            scores.Segment3 = score.Segment3;
            scores.Segment4 = score.Segment4;
            scores.Total = score.Total;
            myContext.Entry(scores).State = EntityState.Modified;
            myContext.SaveChanges();

            return 1;
        }
    }
}
