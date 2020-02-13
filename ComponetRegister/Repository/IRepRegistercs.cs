using ComponetRegister.Model;
using System.Collections.Generic;


namespace ComponetRegister.Repository
{
   public interface IRepRegistercs
    {

     void Add(Register register);
     void Clear();
     IEnumerable<Register> Get();

   }
}
