using System;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels
{
    public class CompanyVM
    {
        [Required]
        public string Email { get; set; }
    }
}
