using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mohan_CapstoneProject_SDA.LMS.Data.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "FullName")]
        [Required(ErrorMessage = "FullName is Required")]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage ="Email is Required")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Passwords don't match")]
        public string ConfirmPassword { get; set; }
    }// V.88
}
