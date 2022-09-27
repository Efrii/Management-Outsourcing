using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("TB_Employee")]
    public class Employee
    {
        [Key]
        public int Id_employee { get; set; }

        [ForeignKey("Class")]
        public int? Id_class { get; set; }
        public Class Class { get; set; }

        [ForeignKey("Employees")]
        public int? Id_manager { get; set; }
        public Employee Employees { get; set; }

        [ForeignKey("Employeess")]
        public int? Id_trainner { get; set; }
        public Employee Employeess { get; set; }

        [Required]
        public string Name_employee { get; set; }

        [Required]
        public string Email_employee { get; set; }

        [Required]
        public string Nik_employee { get; set; }

        [Required]
        public DateTime Datebirth { get; set; }

        [Required]
        public string Age_employee { get; set; }

        [Required]
        public string Gender_Employee { get; set; }

        [Required]
        public string Phone_number { get; set; }

        public bool? Is_place { get; set; }

        public Employee()
        {
            Is_place = false;
        }
    }
}
