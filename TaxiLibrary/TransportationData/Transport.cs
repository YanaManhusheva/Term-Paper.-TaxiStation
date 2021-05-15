using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaxiLibrary.TransportationData;
using System.Threading.Tasks;

namespace TaxiLibrary.BusData
{
    public abstract class Transport
    {
        public Transport(string type)
        {
            Type = type;
        }
        public virtual double TicketPrice(Account account, double route)
        {
            if (route > 100 && route < 300)
            {
                JourneyCost = route * 1.9;
            }
            return JourneyCost;
        }
        public abstract void CreateDriver();
        public virtual decimal DriverSalary()
        {
            decimal salary = 10000;
            return salary;
        }

        protected double JourneyCost;
        public string Type { get; private set; }
        public Ferryman man { get; protected set; }

    }
    public class Bus : Transport
    {
        public Bus(string _type) : base(_type)
        {
        }
        public override void CreateDriver()
        {
            man = new Ferryman("Oleg", 38, 199);
        }
        public override decimal DriverSalary()
        {
            decimal salary = man.CalculateSalary();
            return salary;
        }
        public override double TicketPrice(Account account, double route)
        {
            if (route > 100 && route < 300 && (account.isRegistered || account.Age < 6))
            {
                JourneyCost = route * 1.5;
            }
            else
            {
                JourneyCost = route * 1.8;
            }
            return JourneyCost;
        }
    }

    public class Minivan : Transport
    {
        public Minivan(string _type) : base(_type)
        {
        }

        public override void CreateDriver()
        {
            man = new Ferryman("Sahsa", 29, 112);

        }
        public override decimal DriverSalary()
        {
            decimal salary = man.CalculateSalary();
            return salary;
        }
       
        public override double TicketPrice(Account account, double route)
        {
            if (route > 20 && route < 100 && (account.isRegistered || account.Age < 6))
            {
                JourneyCost = route * 1.5;
            }
            if (route > 100 && route < 400 && (account.isRegistered || account.Age < 6))
            {
                JourneyCost = route * 1.8;
            }
            else
            {
                JourneyCost = route * 2.1;
            }
            return JourneyCost;
        }

    }
}
