using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace fyp_One.Models
{
    public class UserLogInVM
    {
        [Required(ErrorMessage = "Please Enter Your Email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string LogInEmail { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string LogInPassword { get; set; }

        

    }

    //public class ExternalLoginListViewModel
    //{
    //    public string ReturnUrl { get; set; }
    //}



    public class UserSignUpVM
    {
        [Required(ErrorMessage = "Please Enter Your First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter Your Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Your Email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string SignUpEmail { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string SignUpPassword { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("SignUpPassword", ErrorMessage = "Confirm password doesn't match, Type again!")]
        public string SignUpConfirmPassword { get; set; }

    }
    public class UserIndex
    {
        public UserIndex()
        {
            this.signUp = new UserSignUpVM();
            this.logIn = new UserLogInVM();
        }
        public UserSignUpVM signUp { get; set; }
        public UserLogInVM logIn { get; set; }
    }
}