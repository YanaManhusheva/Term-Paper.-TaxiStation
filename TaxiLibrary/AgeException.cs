using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiLibrary
{
    public  class AgeException : Exception
    {
        public int Age { get;  }
        public AgeException() : base() { }
        public AgeException(string str) : base(str) { }
        public AgeException(string str, Exception inner) : base(str,inner) { }
        public AgeException(string str, int age) : this(str) 
        {
            Age = age;
        }
        protected AgeException(System.Runtime.Serialization.SerializationInfo si,
        System.Runtime.Serialization.StreamingContext sc) : base(si, sc) { }

    }
}
