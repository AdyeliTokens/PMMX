using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Account
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Email")]
        [StringLength(250)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(250)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        [StringLength(250)]
        [Display(Name = "Llave")]
        public string Llave { get; set; }
    }
}
