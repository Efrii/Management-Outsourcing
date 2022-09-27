using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    //[Table("TB_Class")]
    public class Class
    {
        [Key]
        public int Id_class { get; set; }

        [Required]
        public string Name_class { get; set; }
    }
}
