using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("TB_User")]
    public class User
    {
        [Key]
        public int Id_User { get; set; }

        [ForeignKey("Employees")]
        public int? Id_employee { get; set; }
        public Employee Employees { get; set; }

        [ForeignKey("Companys")]
        public int? Id_company { get; set; }
        public Company Companys { get; set; }

        [Required(ErrorMessage = "Usernamer is required")]
        [MinLength(5, ErrorMessage = "Usernamer cannot be less than 5 characters")]
        [StringLength(12)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password cannot be less than 8 characters")]
        [StringLength(12)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
