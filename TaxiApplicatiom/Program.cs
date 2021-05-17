using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiLibrary;
using TaxiLibrary.BusData;
using TaxiLibrary.TransportationData;

namespace TaxiApplicatiom
{
    class Program
    {
         public delegate void MyDelegate(Ferryman man,Administration administration);
        static void Main(string[] args)
        {
            bool next = false;
            while (!next)
            {
                Console.WriteLine("====================Menu==========================");
                Console.WriteLine("Choose what you want to do:" +
                    "\n1)Buy a ticket" +
                    "\n2)Watch routes" +
                    "\n3)Exit" +
                    "\n==================================================");

                Administration administration;
                string str = Console.ReadLine();
                int option;
                bool isNum = int.TryParse(str, out option);
                if (!isNum)
                    throw new ArgumentException("The choice must be a number!");
                if (option < 1 || option > 3)
                    throw new ArgumentException("Write the correct option!");
                bool con = false;
                switch (option)
                {
                    case 1:
                        Console.WriteLine();
                        administration = new Administration("Forest Group Kyiv");
                        con = false;
                        while (!con)
                        {
                            try
                            {
                                OrganizeJourney(administration);
                                ObtainTransport(administration);
                                ChooseDate(administration);
                                BuyTickets(administration);
                                DriverInfo(administration);
                                Ticket(administration);
                                con = true;
                                Console.WriteLine("Do you wanna watch menu for other options? 1)yes 2)false");
                                int choice = int.Parse(Console.ReadLine());
                                if (choice < 1 || choice > 2)
                                {
                                    throw new ArgumentException("Write the correct option!");
                                }
                                if (choice == 1)
                                    next = false;
                                else
                                    Console.WriteLine("\nWe are looking forward for you!");
                                    next = true;

                            }
                            catch (Exception ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(ex.Message);
                                Console.ResetColor();
                                Console.WriteLine("============================================");
                                Console.WriteLine("Do you want to try again? 1)yes 2)false");
                                int choice = int.Parse(Console.ReadLine());
                                if (choice < 1 || choice > 2)
                                {
                                    throw new ArgumentException("Write the correct option!");
                                }
                                if (choice == 1)
                                    con = false;
                                else
                                    con = true;
                            }
                            Console.ResetColor();
                        }
                        break;
                    case 2:
                        Dictionary<string, double> dict = new Dictionary<string, double>();
                        int ind = 1;
                        using (var sr = new StreamReader("Route.txt"))
                        {
                            while (!sr.EndOfStream)
                            {
                                string[] temp = sr.ReadLine().Split();
                                dict.Add(temp[0], double.Parse(temp[1]));
                            }
                        }

                        foreach (KeyValuePair<string, double> keyValue in dict)
                        {
                            Console.WriteLine($"{ind}) " + keyValue.Key + " - " + keyValue.Value);
                            ind++;
                        }
                        break;
                    case 3:
                        Console.WriteLine("Thank you for choosing us");
                        next = true;
                        break;
                }
            }
        }
        private static void OrganizeJourney(Administration administration)
        {
            Console.WriteLine("We are greatfull to see you in our company");
            Console.WriteLine("To create your account please enter some data ");
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter your age:");
            int age = int.Parse(Console.ReadLine());
            if (age < 0 || age > 100)
                throw new AgeException($"Entered age: {age} is incorrect" , age);
            Console.WriteLine("Enter the sum:");
            string str = Console.ReadLine();
            double sum;
            bool isNum = double.TryParse(str, out sum);
            if (!isNum)
            {
                throw new ArgumentException("The sum must be a number!");
            }
            if (sum < 0)
                throw new ArgumentException("The sum must be a positive number!");
            Console.WriteLine("Are you a 1)new / 2)registered / 3)permanent customer?:");
            int type = int.Parse(Console.ReadLine());
            if(type < 1 || type > 3)
            {
                throw new ArgumentException("Write the correct option!");
            }
            Console.WriteLine("Enter your password: ");
            string pass = Console.ReadLine();
            AccountType accountType = 0;
            switch (type)
            {
                case 1:
                    accountType = AccountType.New;
                    break;
                case 2:
                    accountType = AccountType.Registered;
                    break;
                case 3:
                    accountType = AccountType.Permanent;
                    break;
            }
            try
            {
                administration.CreateAccount(accountType, sum, age, name, pass);
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        private static void ObtainTransport(Administration administration)
        {
            Console.WriteLine("You can choose whether 1)bus or 2)minivan. Which one do you prefer?");
            int type = int.Parse(Console.ReadLine());
            TransportType transportType = 0;
            if (type < 1 || type > 2)
            {
                throw new ArgumentException("Write the correct option!");
            }
            if (type == 1)
                transportType = TransportType.Bus;
            else
                transportType = TransportType.Minivan;

            administration.ChooseTransport(transportType);

        }
        private static void BuyTickets(Administration administration)
        {
            Console.WriteLine("You may choose these routes:");
            Dictionary<string, double> dict = new Dictionary<string, double>();
            Dictionary<string, double> dict2 = new Dictionary<string, double>();
            
            int ind = 1;
            using (var sr = new StreamReader("Route.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string[] temp = sr.ReadLine().Split();
                    dict.Add(temp[0], double.Parse(temp[1]));
                    dict2.Add(temp[0], double.Parse(temp[2]));
                }
            }
            

            foreach (KeyValuePair<string, double> keyValue in dict)
            {
                Console.WriteLine($"{ind}) " + keyValue.Key + " - " + keyValue.Value);
                ind++;
            }
            
            Console.WriteLine("Choose your route");
            double distance = 0;
            double duration = 0;
            int num = int.Parse(Console.ReadLine());
            if(num<1 || num > 5)
            {
                throw new ArgumentException("Write the correct option!");
            }
            switch (num)
            {
                case 1:
                    distance = dict["IvanoFrankivsk"];
                    duration = dict2["IvanoFrankivsk"];

                    break;
                case 2:
                    distance = dict["Zhytomyr"];
                    duration = dict2["Zhytomyr"];
                    break;
                case 3:
                    distance = dict["Lviv"];
                    duration = dict2["Lviv"];
                    break;
                case 4:
                    distance = dict["Troieschyna"];
                    duration = dict2["Troieschyna"];
                    break;
                case 5:
                    distance = dict["Bukovel"];
                    duration = dict2["Bukovel"];
                    break;
            }
            administration.SetDuration(duration);
            administration.PayTickets(distance);
            Console.WriteLine($"Ticket price: {administration.Price}");



        }
        private static void DriverInfo(Administration administration)
        { 
            Console.WriteLine("Do you want to read additional information about your driver? 1)yes / 2)no");
            string str = Console.ReadLine();
            int choice;
            bool isNum = int.TryParse(str, out choice);
            if (!isNum)
                throw new ArgumentException("The choice must be a number!");
            if (choice <1 || choice > 2)
                throw new ArgumentException("Write the correct option!");

            if(choice == 1)
            {
                administration.DriverInfo();
                MyDelegate mydel = ShowInfo;;
                mydel?.Invoke(administration.newTransp.man, administration);

            }
        }
        private static void ChooseDate(Administration administration)
        {
            Console.WriteLine("Enter the appropriate for you day(from 1 to 7):" +
                "\nConsider that buses only drive on odd days, while minivans only on even days");
            int num = int.Parse(Console.ReadLine());
            if (num < 1 || num > 7)
            {
                throw new ArgumentException("Write the correct option!");
            }
            else
                Console.WriteLine($"[{administration.ChooseDate(num)}]");

            int time = 0;
            List<int> hours = administration.ChooseTime();
            Console.WriteLine("There are such arrival times in choosen day:" +
                "\nYou can obtain 1) or 2)");
            foreach (int i in hours)
            {
                Console.Write($"[{i}] \t");
            }
            Console.WriteLine();
            int choice = int.Parse(Console.ReadLine());
            if (choice < 1 || choice > 2)
            {
                throw new ArgumentException("Write the correct option!");
            }
            switch (choice)
            {
                case 1:
                    time = hours[0];
                    break;
                case 2:
                    time = hours[1];
                    break;
            }
            Console.WriteLine($"You choose {time}");
            administration.SetTime(time);
        }
        private static void ShowInfo(Ferryman man, Administration administration)
        {
            Console.WriteLine($"Your driver Name : {man.Name}");
            Console.WriteLine($"Age : {man.Age}");
            Console.WriteLine($"Working hours : {man.WorkHours}");
            Console.WriteLine($"Salary : {administration.DriverInfo()}");

        }
        private static void Ticket(Administration administration)
        {
            Thread.Sleep(3000);
            Console.Clear();
            Console.WriteLine("======================Ticket======================");
            Console.WriteLine("                 Passanger data                   ");
            Console.WriteLine($"Name : {administration.newAcc.Name} ");
            Console.WriteLine($"\nAge : {administration.newAcc.Age} ");
            Console.WriteLine($"\nDistance : {administration.JourneyDistance}");
            Console.WriteLine($"\nDriving on {administration.newTransp.Type}");
            Console.WriteLine($"\nTicket price : {administration.Price}");
            Console.WriteLine($"\nDay: {administration.day}");
            Console.WriteLine($"Time : {administration.Time}");
            Console.WriteLine($"\nTime duration {administration.Duration} hours");
            Console.WriteLine($"\nThank you for choosing <{administration.Name}> ");
            Console.WriteLine("==================================================");
            Console.WriteLine("\n");


        }

    }
}
