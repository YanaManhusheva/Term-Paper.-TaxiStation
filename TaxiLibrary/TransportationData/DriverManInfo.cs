using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiLibrary.TransportationData
{
    [Serializable]
    public class Ferryman
    {
        public Ferryman(string name, int age)
        {
            Name = name;
            Age = age;
           
        }

        public decimal CalculateSalary()
        {
            Salary =(decimal)(WorkHours / WorkDays) * 30 * 65;  
            return Salary;
        }

        public decimal Salary { get; private set; } 
        public string Name { get; private set; }
        public int Age { get; private set; }
        public int WorkDays { get; set; }

        public double WorkHours { get; set; } 
    }

}
