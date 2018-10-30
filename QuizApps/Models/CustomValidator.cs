using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizApps.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class CustomValidator: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            mocktestEntities1 db = new mocktestEntities1();
            user newUser = new user();
             List<user> userList = new List<user>();
            int count = 0;
            userList = db.users.ToList();
            if(value!=null)
            {
                string email = value.ToString();
                if (userList.Count > 0)
                {
                    foreach (var item in userList)
                    {
                        if (item.EmailId.Equals(email))
                        {
                            count++;
                        }
                    }
                }
                if (count != 0)
                {
                    return new ValidationResult(ErrorMessage); 
                }
                else
                {
                    return ValidationResult.Success;
                }  
               
            }
            else
            {
                    return ValidationResult.Success; 
            }  
        }
    }
}