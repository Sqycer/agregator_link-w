using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace agregator_linków.Viewmodel
{
    public class ViewLogin
    {
        [Required(ErrorMessage = "Pole jest obowiąskowe")]
        [EmailAddress(ErrorMessage = "Błędny login")]
      public  string login { get; set; }
        [Required(ErrorMessage = "Pole jest obowiąskowe")]
      public  string password { get; set; }

    }
}
