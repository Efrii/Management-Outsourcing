using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("TB_Company")]
    public class Company
    {
        [Key]
        public int Id_company { get; set; }

        [Required]
        public string Name_company { get; set; }

        [Required]
        public string Email_company { get; set; }

        [Required]
        public string Address_company { get; set; }

        [Required]
        public string Phone_company { get; set; }

        public ICollection<Jobs> Jobess { get; set; }

    }
}
