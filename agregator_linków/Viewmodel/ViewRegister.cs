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
        [Required(ErrorMessage="Pole jest obowiąskowe")]
        [EmailAddress(ErrorMessage = "Błędny Email")]
        public string email { get; set; }
        [Required(ErrorMessage = "Pole jest obowiąskowe")]
        public string password { get; set; }
        [Required(ErrorMessage = "Pole jest obowiąskowe")]
        public string checkpassword { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
            if (password != checkpassword)
            {
                yield return new ValidationResult(
                       $"Hasła się nie zgadzają", new[] { "checkpassword" });
            }
    }


}
}
