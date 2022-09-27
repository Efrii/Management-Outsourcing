using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("TB_Interview")]
    public class Interview
    {
        [Key]
        public int Id_interview { get; set; }

        [ForeignKey("Employees")]
        public int Id_employee { get; set; }
        public Employee Employees { get; set; }

        [ForeignKey("Companys")]
        public int Id_company { get; set; }
        public Company Companys { get; set; }

        [Required]
        public string Job_position { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date_interview { get; set; }

        [Required]
        public string Url_Intertview { get; set; }

        [Required]
        public string Interviewer_name { get; set; }

        [DefaultValue(false)]
        public bool Is_done { get; set; }

        // 0 == PENDDING
        // 1 == DITOLAK
        // 2 == DITERIMA
        [DefaultValue(0)]
        public int Is_accepted { get; set; }

        public Interview()
        {
            Is_done = false;
            Is_accepted = 0;
        }
    }
}
