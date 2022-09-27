using System;
using System.Linq;
using API.Context;
using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class AccountRepository
    {
        MyContext myContext;

        public AccountRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public User GetLogin(string username, string password)
        {
            var data = myContext.Users.
                Include(x => x.Employees).
                Include(x => x.UserRoles).
                ThenInclude(x => x.Role).
                Where(x => x.Username == username).
                Where(x => x.Password == password).
                SingleOrDefault();

            return data;
        }

        public User GetLoginCompany(string username, string password)
        {
            var data = myContext.Users
                .Include(x => x.Companys)
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .Where(x => x.Username == username)
                .Where(x => x.Password == password)
                .SingleOrDefault();

            return data;
        }

        public bool CekEmailEmployee(string email)
        {
            var data = myContext.Employees.FirstOrDefault(x => x.Email_employee == email);

            return data != null;
        }

        public bool CekEmailCompany(string email)
        {
            var data = myContext.Companys.FirstOrDefault(x => x.Email_company == email);

            return data != null;
        }

        // Register ROLE "EMPLOYEEBOOTCAMP"
        public int RegisterEmployee(RegisterEmployeeVM register)
        {
            var cek = CekEmailEmployee(register.Email_employee);

            if (cek == false)
            {
                var emp = new Employee
                {
                    Name_employee = register.Name_employee,
                    Email_employee = register.Email_employee,
                    Nik_employee = register.Nik_employee,
                    Datebirth = register.Datebirth,
                    Age_employee = register.AgeEmployee,
                    Gender_Employee = register.GenderEmployee,
                    Phone_number = register.GenderEmployee
                };

                register.Id_employee = emp.Id_employee;
                myContext.Employees.Add(emp);
                myContext.SaveChanges();

                var user = new User
                {
                    Id_employee = emp.Id_employee,
                    Username = register.Username,
                    Password = register.Password
                };

                register.Id_employee = (int)user.Id_employee;
                myContext.Users.Add(user);
                myContext.SaveChanges();

                var role = new UserRole
                {
                    Id_user = user.Id_User,
                    Id_role = 4
                };

                register.Id_user = role.Id_user;
                myContext.UserRoles.Add(role);
                var data = myContext.SaveChanges();

                return data;

            }
            else if (cek == true)
            {
                return 2;
            }

            return 0;
        }

        // Register Company Role "CLIENT"
        public int RegisterCompany(RegisterCompanyVM register)
        {
            var cek = CekEmailCompany(register.Email_company);

            if (cek == false)
            {
                var emp = new Company
                {
                    Name_company = register.Name_company,
                    Email_company = register.Email_company,
                    Address_company = register.Address_company,
                    Phone_company = register.Phone_number
                };

                register.Id_company = emp.Id_company;
                myContext.Companys.Add(emp);
                myContext.SaveChanges();

                var user = new User
                {
                    Id_company = emp.Id_company,
                    Username = register.Username,
                    Password = register.Password
                };

                register.Id_company = (int)user.Id_company;
                myContext.Users.Add(user);
                myContext.SaveChanges();

                var role = new UserRole
                {
                    Id_user = user.Id_User,
                    Id_role = 5
                };

                register.Id_user = role.Id_user;
                myContext.UserRoles.Add(role);
                var data = myContext.SaveChanges();

                return data;

            }
            else if (cek == true)
            {
                return 2;
            }

            return 0;
        }
    }
}
