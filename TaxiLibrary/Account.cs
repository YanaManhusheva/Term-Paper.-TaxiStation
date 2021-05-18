using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiLibrary
{
    public abstract class Account: IAccount
    {
        public delegate void AccountStateHandler(object sender, AccountEventArgs e);
        public virtual event AccountStateHandler Payed;
        public virtual double Pay(double sum)
        {
            double result = 0;
            if (sum < _sum)
            {
                _sum -= sum;
                result = sum;
                Payed?.Invoke(this, new AccountEventArgs($"The sum {sum} was withdrawed from account ,your left money is {_sum - sum }", _sum));
                
            }
            else
            {
                throw new ArgumentException($"There is not enough money on New account. You need to pay {sum }");
            }
            return sum;
        }

        public Account(double sum, int age, string name)
        {
            _sum = sum;
            Age = age;
            Name = name;
            
        }
        public bool isRegistered { get; protected set; }
        public int Age { get; protected set; }
        public string Name { get; protected set; }
        protected double _sum;
       

    }
}
