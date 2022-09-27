using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using API.Models;

namespace API.ViewModels
{
    public class RegisterEmployeeVM
    {
        public int Id_employee { get; set; }
        public int Id_user { get; set; }

        [Required(ErrorMessage = "Name Employee is required")]
        public string Name_employee { get; set; }

        [Required(ErrorMessage = "Email Employee is required")]
        public string Email_employee { get; set; }

        [Required(ErrorMessage = "NIK Employee is required")]
        public string Nik_employee { get; set; }

        [Required(ErrorMessage = "Datebirth Employee is required")]
        public DateTime Datebirth { get; set; }

        [Required(ErrorMessage = "Age Employee is required")]
        public string AgeEmployee { get; set; }

        [Required(ErrorMessage = "Name Employee is required")]
        public string GenderEmployee { get; set; }

        [Required(ErrorMessage = "Phone Number Employee is required")]
        public string Phone_number { get; set; }

        [Required(ErrorMessage = "Usernamer is required")]
        [MinLength(5, ErrorMessage = "Usernamer cannot be less than 5 characters")]
        [StringLength(12)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password cannot be less than 8 characters")]
        [StringLength(12)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}
