using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace QuizApps.Models
{

    public class SignUp
    {
        public int userId { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "More Than 2 Characters please!", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Email Id")]
       
        [System.Web.Mvc.Remote("CheckExistingEmail", "RemoteValidation", ErrorMessage = "Email already exists!")]
        [DataType(DataType.EmailAddress)]
        //[CustomValidator(ErrorMessage="Name contains invalid character.")]
        public string EmailId { get; set; }

        [Required]
        [Display(Name = "Roll No.")]
        public string Rollno { get; set; }

        [Required]
        [Display(Name = "Branch")]
        public string Branch { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "More Than 6 Characters please!", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirm password donot match.")]
        public string ConfirmPassword { get; set; }
    }
}