using System;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels
{
    public class ScoreVM
    {
        public int? Id_score { get; set; }
        public int Id_trainner { get; set; }
        [Required]
        public int Id_employee { get; set; }
        [Required]
        public int Id_class { get; set; }

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
