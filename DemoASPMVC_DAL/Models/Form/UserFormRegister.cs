using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoASPMVC_DAL.Models.Form {
    public class UserFormRegister {

        [Required]
        [MinLength(3)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType( DataType.Password )]
        [RegularExpression( "^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$" )]
        public string Password { get; set; }

        [Required]
        [DataType( DataType.Password )]
        [Compare( nameof( Password ), ErrorMessage = "Les 2 mots de passe doivent correspondre" )]
        public string ConfirmPassword { get; set; }
    }
}
