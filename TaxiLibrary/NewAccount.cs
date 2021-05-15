using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiLibrary
{
    public class NewAccount : Account
    {
        public override event AccountStateHandler Payed;
        public NewAccount(double sum, int age, string name) : base(sum, age, name)
        {
        }
        public override double Pay(double sum)
        {
            isRegistered = false;
            double additional_sum = sum * 1.2 ;
            if (_sum < sum)
            {
                throw new ArgumentException($"There is not enough money on New account. You need to pay {sum }");
            }
            if (isRegistered || Age < 6)
            {
               Payed?.Invoke(this, new AccountEventArgs($"The sum { sum } was withdrawed from New account ,your left money is { _sum - sum }", _sum));
               return base.Pay(sum);
            }
            else
            {
                Payed?.Invoke(this, new AccountEventArgs($"The sum { additional_sum } was withdrawed from New account ,your left money is { _sum - sum }", _sum));
                return base.Pay(additional_sum);
            }  
        }

    }
}
