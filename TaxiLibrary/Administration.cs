using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TaxiLibrary.TransportationData;
using TaxiLibrary.BusData;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace TaxiLibrary
{
    public delegate void MyDelegate(Ferryman man);
    public enum AccountType 
    {
        New,
        Registered,
        Permanent,
        Admin
    }
    public enum TransportType
    {
        Bus,
        Minivan
    }
    public enum WeekDays
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
    public class Administration
    {
        static Administration()
        {
            men = FerrymanDataFile();
        }
        public Administration(string name)
        {
            Name = name;
        }

        public void CreateAccount(AccountType accountType, double sum, int age, string name, string pass)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Null name argument!");
            }
            switch (accountType)
            {
                case AccountType.New:
                    newAcc = new NewAccount(sum, age, name);
                    break;
                case AccountType.Registered:
                    if (pass == "2468")
                        newAcc = new RegisteredAccount(sum, age, name);
                    else
                        throw new ArgumentException("Incorrect password");
                    break;
                case AccountType.Permanent:
                    if (pass == "1357")
                        newAcc = new PermanentAccount(sum, age, name);
                    else
                        throw new ArgumentException("Incorrect password");
                    break;
            }
        }
        public void CreateAccount(AccountType accountType, string name, string pass)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Null name argument!");
            }
            if (pass == "1708")
            {
                newAdmin = new AdminAccount(name);
                IsAdmin = true;
            }
            else
                throw new ArgumentException("Incorrect password");

        }
        public void ChooseTransport(TransportType transportType)
        {
            switch (transportType)
            {
                case TransportType.Bus:
                    newTransp = new Bus("<Honda Bus>");
                    break;
                case TransportType.Minivan:
                    newTransp = new Minivan("<Reno Minivan>");
                    break;
            }
        }
        public void PayTickets(double distance)
        {
            JourneyDistance = distance;
            newAcc.Payed += ShowEvent;
            Price = newTransp.TicketPrice(newAcc, distance);
            Price = newAcc.Pay(Price);

        }
        static void ShowEvent(object sender, AccountEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(e.Message);
            Console.ResetColor();
        }

        public WeekDays ChooseDate(int dateday)
        {
            if (dateday <= 0)
                throw new ArgumentException("Incorrect input! Day must be a positive number");
            if (newTransp is Bus)
            {
                if (dateday % 2 != 0)
                {
                    switch (dateday)
                    {
                        case 1:
                            day = WeekDays.Monday;
                            break;
                        case 3:
                            day = WeekDays.Wednesday;
                            break;
                        case 5:
                            day = WeekDays.Friday;
                            break;
                        case 7:
                            day = WeekDays.Sunday;
                            break;
                    }
                }
                else
                    throw new ArgumentException("There is no such days");
            }
            if (newTransp is Minivan)
            {
                if (dateday % 2 == 0)
                {
                    switch (dateday)
                    {
                        case 2:
                            day = WeekDays.Tuesday;
                            break;
                        case 4:
                            day = WeekDays.Thursday;
                            break;
                        case 6:
                            day = WeekDays.Saturday;
                            break;
                    }
                }
                else
                    throw new ArgumentException("There is no such days");
            }
            return day;
        }
        public List<int> ChooseTime()
        {
            hours = new List<int>();
            switch (day)
            {
                case WeekDays.Monday:
                    hours.AddRange(new[] { 12, 22 });
                    break;
                case WeekDays.Tuesday:
                    hours.AddRange(new[] { 10, 20 });
                    break;
                case WeekDays.Wednesday:
                    hours.AddRange(new[] { 9, 17 });
                    break;
                case WeekDays.Thursday:
                    hours.AddRange(new[] { 8, 19 });
                    break;
                case WeekDays.Friday:
                    hours.AddRange(new[] { 7, 13 });
                    break;
                case WeekDays.Saturday:
                    hours.AddRange(new[] { 11, 21 });
                    break;
                case WeekDays.Sunday:
                    hours.AddRange(new[] { 6, 15 });
                    break;
            }
            return hours.ToList();
        }
        public void DriverInfo()
        {
            if (newTransp is Bus)
            {
                newMan = newTransp.CreateDriver(hours, Time, men);
            }
            else
            {
                newMan = newTransp.CreateDriver(hours, Time, men); 
            }

        }
        public decimal DriveSalary()
        {
            decimal salary;
            salary = newTransp.DriverSalary();
            return salary;
        }

        public void SetTime(int time)
        {
            Time = time;
        }
        public void SetDuration(double dur)
        {
            Duration = dur;
            newMan.WorkHours += dur;
            newMan.WorkDays += 1;
            DriveSalary();
            SaveFerrymen();
        }

        private static List <Ferryman> FerrymanDataFile()
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream("Ferryman.txt", FileMode.OpenOrCreate))
            {
                if(fs.Length != 0)
                {
                    if (formatter.Deserialize(fs) is List<Ferryman> men)
                    {
                        return men;
                    }
                }
                return null;
            }
        }
        private static void SaveFerrymen()
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream("Ferryman.txt", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, men);
            }
        }
        
        public AdminAccount newAdmin { get; private set; }
        public Account newAcc { get; private set; }
        public Ferryman newMan { get; private set; }
        public bool IsAdmin { get; private set; }=false;
        public Transport newTransp { get; private set; }
        public double Duration { get; private set; }
        public double JourneyDistance { get; private set; }
        public WeekDays day { get; private set; }
        public int Time { get; private set; }
        public double Price { get; private set; }
        public string Name { get; private set; }
        protected List<int> hours ;
        static public List<Ferryman> men { get; private set; }
    }
    
}

