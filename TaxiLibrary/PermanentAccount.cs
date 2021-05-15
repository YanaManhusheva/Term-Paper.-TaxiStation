using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiLibrary
{
    public class PermanentAccount : Account
    {
        public override event AccountStateHandler Payed;
        public PermanentAccount (double sum, int age, string name) : base(sum, age, name)
        {
        }
        public override double Pay(double sum)
        {
            isRegistered = true;
            double discount = sum * 0.8;
            if (_sum < sum)
            {
                throw new ArgumentException($"There is not enough money on Permanent account. You need to pay {sum }");
            }
            if (isRegistered || Age < 6 )
            {
                Payed?.Invoke(this, new AccountEventArgs($"The discount sum { discount }, unless the full price {sum} was withdrawed from Permanent account, your left money is { _sum - sum}", _sum));
                return base.Pay(discount);
            }
            else
            {
                Payed?.Invoke(this, new AccountEventArgs($"The sum { sum } was withdrawed from Permanent account, your left money is { _sum-sum }", _sum));
                return base.Pay(sum);
            }
        }
    }
}
