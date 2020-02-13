using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace agregator_linków.Models
{
    public class UserLike:IEntit
    {
      
        public int userid { get; set; }
        public virtual User user { get; set; }

        public int linkid { get; set; }
        public virtual Link link { get; set; }
    }
}
