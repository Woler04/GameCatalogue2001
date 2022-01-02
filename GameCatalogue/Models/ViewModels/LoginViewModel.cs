using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GameCatalogue.ViewModels
{
    public class LoginViewModel
    {
        [EmailAddress, Required]
        public string Email { get; set; }

        [DataType(DataType.Password), Required]
        public string Password { get; set; }

        [Display(Name ="Remeber Me")]
        public bool RememberMe { get; set; }
    }
}
