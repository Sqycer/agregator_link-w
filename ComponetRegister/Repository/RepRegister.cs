using ComponetRegister.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComponetRegister.Repository
{
 public class RepRegister: IRepRegistercs
    {
        private List<Register> registers = new List<Register>();

        public void Add(Register register)
        {
            registers.Add(register);
        }


        public void Clear()
        {
            registers.Clear();
        }

        public IEnumerable<Register> Get()
        {
            return registers;
        }
    }
}
