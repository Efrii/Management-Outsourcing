using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using API.Models;

namespace API.ViewModels
{
    public class RegisterCompanyVM
    {
        public int Id_company { get; set; }
        public int Id_user { get; set; }

        [Required(ErrorMessage = "Name Company is required")]
        public string Name_company { get; set; }

        [Required(ErrorMessage = "Email Company is required")]
        public string Email_company { get; set; }

        [Required(ErrorMessage = "Address Company is required")]
        public string Address_company { get; set; }

        [Required(ErrorMessage = "Phone Number Company is required")]
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
