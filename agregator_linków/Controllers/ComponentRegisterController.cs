using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComponetRegister.Model;
using ComponetRegister.Repository;
using Microsoft.AspNetCore.Mvc;

namespace agregator_linków.Controllers
{
    [Route("api/[controller]")]
    public class ComponentRegisterController : Controller
    {
        private IRepRegistercs repRegisters;

        public ComponentRegisterController(IRepRegistercs repRegisters)
        {
           
            this.repRegisters = repRegisters;
        }

        [HttpGet]
        public IEnumerable<Register> Get()
        {
          return  repRegisters.Get();
        }


        [HttpDelete]
        public IActionResult Delete()
        {
            repRegisters.Clear();
            return new OkResult();
        }


    }
}