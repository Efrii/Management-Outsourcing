using System;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels
{
    public class EmployeeVM
    {
        [Required]
        public string Email { get; set; }
    }
}
