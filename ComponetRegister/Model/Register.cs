using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ComponetRegister.Model
{
    public class Register
    {

        public Register(string text , string data)
        {
            this.text = text;
            this.data = data;
        }


     public   string text {get; set;}
    public    string data { get; set; }

    }
}
