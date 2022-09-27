using System;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Usernamer is required")]
        [MinLength(5, ErrorMessage = "Usernamer cannot be less than 5 characters")]
        [StringLength(12)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password cannot be less than 8 characters")]
        [StringLength(12)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
