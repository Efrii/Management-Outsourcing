using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("TB_Jobs")]
    public class Jobs
    {
        [Key]
        public int Id_jobs { get; set; }

        [ForeignKey("Companys")]
        public int Id_company { get; set; }
        public Company Companys { get; set; }

        [Required]
        public string Name_jobs { get; set; }

        [Required]
        public string Recruitment_skill { get; set; }
    }
}
