using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace agregator_linków.Viewmodel
{
    public class ViewRegister: IValidatableObject
    {
        [Remote(action: "VerifyEmail", controller: "User")]
        [Required()]
        [EmailAddress()]
        public string email { get; set; }
        [Required()]
        [RegularExpression(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$",
            ErrorMessage = @"Password need UpperCase, LowerCase, Number/SpecialChar and min 8 Chars") ]
        public string password { get; set; }
        [Required()]
        public string checkpassword { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
            if (password != checkpassword)
            {
                yield return new ValidationResult(
                       $"Passwords do not match", new[] { "checkpassword" });
            }
    }


}
}
