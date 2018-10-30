using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace QuizApps.Models
{
    
    public class users
    {
        [NotMapped]
        [Required]
        [Key]
        public int UserId { get; set; }

        [NotMapped]
        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "More Than 6 Characters please!", MinimumLength = 6)]
        public string Name { get; set; }

        [NotMapped]
        [Display(Name = "Email Id")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        [NotMapped]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "More Than 6 Characters please!", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirm password donot match.")]
        [DataType(DataType.Password)]
        public bool ConfirmPassword { get; set; }
    }
}
