using System;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels
{
    public class AcceptmentVM
    {
        [Required]
        public int Id_employee { get; set; }

        [Required]
        public string Name_Company { get; set; }

        [Required]
        public string Job_position { get; set; }

        [Required]
        public bool Is_place { get; set; }
    }
}
