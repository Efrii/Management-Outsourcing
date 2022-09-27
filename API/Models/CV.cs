using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("TB_CV")]
    public class CV
    {
        [Key]
        public int Id_cv { get; set; }

        [ForeignKey("Employees")]
        public int Id_employee { get; set; }
        public Employee Employees { get; set; }

        [ForeignKey("Companys")]
        public int Id_company { get; set; }
        public Company Companys { get; set; }

        [Required]
        public string Job_selected { set; get; }

        [Required]
        public string Cv_employee { get; set; }

        [DefaultValue(false)]
        public bool Is_check { get; set; }

        public CV()
        {
            Is_check = false;
        }
    }
}
