using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agregator_linków.Models
{
    public class Link : IEntit
    {
        
      public string url { get; set; }
      public string title { get; set; }
      public DateTime time { get; set; }
      public int  like { get; set;}
      public int ownerid { get; set; } 
      public virtual User owner { get; set; }
      public virtual ICollection<UserLike> userLike { get; set; }
    }
}
