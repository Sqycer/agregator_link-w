using agregator_linków.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agregator_linków.Repository
{
    public  class MainRepository
    {
        public RepUser user;
        public RepLink link;

        public MainRepository(Dbcontext dbcontext)
        {
            user = new RepUser(dbcontext);
            link = new RepLink(dbcontext);
        }
    }
}
