using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("TB_UserRole")]
    public class UserRole
    {
        [Key]
        public int Id_UserRole { get; set; }

        [ForeignKey("User")]
        public int Id_user { get; set; }
        public User User { get; set; }

        [ForeignKey("Role")]
        public int Id_role { get; set; }
        public Role Role { get; set; }
    }
}
