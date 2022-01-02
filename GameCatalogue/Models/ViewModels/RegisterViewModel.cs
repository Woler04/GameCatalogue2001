using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameCatalogue.ViewModels
{
    public class RegisterViewModel
    {
        [EmailAddress, Required]
        public string Email { get; set; }
        [DataType(DataType.Password), Required]
        public string Password { get; set; }

        [DataType(DataType.Password), Required]
        [Display(Name ="Confirm Password")]
        [Compare("Password", ErrorMessage ="Passwords not matching")]
        public string ConfirmPassword { get; set; }
    }
}
