using agregator_linków.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agregator_linków.Viewmodel
{
    public class ViewIndexLink
    {   
        public int id { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public string user { get; set; }
        public DateTime time { get; set; }
        public int like { get; set; }        
        public string buttonlike { get; set;}
    }
}
