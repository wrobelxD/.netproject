using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Komunikator1.ViewModels
{
    public class RegisterVMcs
    {
        [Required, MinLength(5)]
        public string Username { get; set; }

        [MinLength(5), Required, DataType(DataType.Password)]
        public String Password { get; set; }
        [Compare("Password"),DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [EmailAddress, Required]
        public string Email { get; set; }
    }
}