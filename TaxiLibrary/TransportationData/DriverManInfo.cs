using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiLibrary.TransportationData
{
    public class Ferryman
    {
        public Ferryman(string name, int age, double hours)
        {
            Name = name;
            Age = age;
            WorkHours = hours;
        }

        public decimal CalculateSalary()
        {
            if (WorkHours < 0 || WorkHours > 220)
                throw new ArgumentException("Working hours are incorrect");

            if (WorkHours >= 198 || WorkHours < 220)
                Salary = Convert.ToDecimal(WorkHours * 90);
            if (WorkHours >= 110 || WorkHours < 198)
                Salary = Convert.ToDecimal(WorkHours * 80);
            else
                Salary = Convert.ToDecimal(WorkHours * 60);
            return Salary;
        }
       
        public decimal Salary { get; private set; }
        public string Name { get; private set; }
        public int Age { get; private set; }
        public double WorkHours { get; private set; }
    }

}
