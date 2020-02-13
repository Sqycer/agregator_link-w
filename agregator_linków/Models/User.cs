using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agregator_linków.Models
{
    public class User:IEntit
    {
     public string eamil { get; set; }
     public string password { get; set; }
     public string salt { get; set; }
     public virtual ICollection<Link> links { get; set; }
     public virtual ICollection<UserLike> userLike { get; set; }

    }
}

