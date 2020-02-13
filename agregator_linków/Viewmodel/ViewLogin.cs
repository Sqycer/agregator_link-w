using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace agregator_linków.Viewmodel
{
    public class ViewLogin
    {
        [Required()]
        [EmailAddress()]
      public  string login { get; set; }
        [Required()]
      public  string password { get; set; }

    }
}
