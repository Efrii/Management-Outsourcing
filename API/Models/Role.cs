using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("TB_Role")]
    public class Role
    {
        [Key]
        public int Id_role { get; set; }

        [Required(ErrorMessage = "Role Employee is required")]
        public string Name_role { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
