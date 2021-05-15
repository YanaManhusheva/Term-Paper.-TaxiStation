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
        public abstract void CreateDriver(List<int> hours, int Time);
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
        public override void CreateDriver(List<int> hours, int Time)
        {
            if (Time == hours[0])
                man = new Ferryman("Sasha", 24, 160);
            else
                man = new Ferryman("Oleg", 39, 200);


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

        public override void CreateDriver(List<int> hours, int Time)
        {
            if (Time == hours[0])
                man = new Ferryman("Nazar", 29, 174);
            else
                man = new Ferryman("Ivan", 48, 110);
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
