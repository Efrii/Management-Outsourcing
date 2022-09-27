using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("TB_Acceptment")]
    public class Acceptment
    {
        [Key]
        public int Id_Acceptment { get; set; }

        [ForeignKey("Employee")]
        public int Id_employee { get; set; }
        public Employee Employee { get; set; }

        [Required]
        public string Name_Company { get; set; }

        [Required]
        public string Job_position { get; set; }
    }
}
