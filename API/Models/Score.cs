using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("TB_Score")]
    public class Score
    {
        [Key]
        public int Id_score { get; set; }

        [ForeignKey("Employees")]
        public int Id_employee { get; set; }
        public Employee Employees { get; set; }

        [Required]
        public int Segment1 { get; set; }

        [Required]
        public int Segment2 { get; set; }

        [Required]
        public int Segment3 { get; set; }

        [Required]
        public int Segment4 { get; set; }

        [Required]
        public int Total { get; set; }
    }
}
