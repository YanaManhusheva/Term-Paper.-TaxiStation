using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiLibrary
{
    public class AccountEventArgs
    {
        public string Message { get; private set; }
        public double Sum { get; private set; }
        public AccountEventArgs(string mes, double sum)
        {
            Message = mes;
            Sum = sum;
        }
    }
}
