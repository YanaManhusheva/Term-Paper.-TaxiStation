using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiLibrary
{
    public class RegisteredAccount : Account
    {
        public override event AccountStateHandler Payed;
        public RegisteredAccount(double sum, int age, string name): base(sum, age, name)
        {
        }
        
        public override double Pay(double sum)
        {
            isRegistered = true;
            double additional_sum = sum * 1.2;
            if (_sum < sum)
            {
                throw new ArgumentException($"There is not enough money on Redistered account. You need to pay {sum }");
            }
            if (isRegistered || Age < 6)
            {
                Payed?.Invoke(this, new AccountEventArgs($"The sum { sum } was withdrawed from Registered account ,your left money is { _sum - sum }", _sum));
                return base.Pay(sum);

            }
            else
            {
                Payed?.Invoke(this, new AccountEventArgs($"The sum { additional_sum } was withdrawed from Registered account ,your left money is { _sum - sum }", _sum));
                return base.Pay(additional_sum);

            }
        }
    }
}
