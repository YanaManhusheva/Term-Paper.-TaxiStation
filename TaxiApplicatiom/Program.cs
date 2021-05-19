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
using System.Runtime.Serialization.Formatters.Binary;

namespace TaxiApplicatiom
{
    class Program
    {
        public delegate void MyDelegate(Ferryman man,Administration administration);
        static void Main(string[] args)
        {
            #region
            //List<Ferryman> men = new List<Ferryman>();
            //men.Add(new Ferryman("Sasha", 24, 160));
            //men.Add(new Ferryman("Oleg", 39, 200));
            //men.Add(new Ferryman("Nazar", 29, 174));
            //men.Add(new Ferryman("Ivan", 48, 110));
            //foreach (var man in men)
            //{
            //    man.CalculateSalary();
            //}
            //var formatter = new BinaryFormatter();
            //using (var fs = new FileStream("Ferryman.txt", FileMode.OpenOrCreate))
            //{
            //    formatter.Serialize(fs, men);
            //}
            #endregion
            bool next = false;
            int option=0;
            bool con = false;
            while (!next)
            {
                    Administration administration;
                    Console.WriteLine("====================Menu==========================");
                    Console.WriteLine("Choose what you want to do:" +
                        "\n1)Plan journey" +
                        "\n2)Watch routes" +
                        "\n3)Exit" +
                        "\n==================================================");
                    ChooseLimits(1, 3, "Choose your option", out option);
                    switch (option)
                    {
                        case 1:
                            Console.WriteLine();
                            administration = new Administration("Forest Group Kyiv");
                            while (!con)
                            {
                                try
                                {
                                    OrganizeJourney(administration);
                                    if (!administration.IsAdmin)
                                    {
                                        ObtainTransport(administration);
                                        ChooseDate(administration);
                                        BuyTickets(administration);
                                        DriverInfo(administration);
                                        Ticket(administration);
                                        con = true;
                                        ChooseLimits(1, 2, "Do you wanna watch menu for other options? 1)yes 2)false", out int choice);
                                        if (choice == 1)
                                            next = false;
                                        else
                                        {
                                            Console.WriteLine("\nWe are looking forward to you!");
                                            next = true;
                                        }
                                    }
                                    else
                                    {
                                        AdminMethods(administration, next);
                                        con = true;
                                        ChooseLimits(1, 2, "\nDo you wanna watch general menu? 1)yes 2)false", out int choice);
                                        if (choice == 1)
                                            next = false;
                                        else
                                        {
                                            next = true;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(ex.Message);
                                    Console.ResetColor();
                                    Console.WriteLine("============================================");
                                    ChooseLimits(1, 2, "\nDo you want to try again? 1)yes 2)false", out int choice);
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
               
                {

                }
            }
        }
        private static void OrganizeJourney(Administration administration)
        {
            Console.WriteLine("We are greatfull to see you in our company");
            Console.WriteLine("To create your account please enter some data ");
            ChooseLimits(1, 4, "Are you a 1)new / 2)registered / 3)permanent customer / 4)admin?:", out int type);
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();
            foreach (var ch in name)
            {
                if (Char.IsDigit(ch))
                    throw new ArgumentException("Incorrect name");
            }
            int age = 0;
            int sum = 0;
            if (type < 4)
            {
                Console.WriteLine("Enter your age:");
                age = int.Parse(Console.ReadLine());
                if (age < 0 || age > 100)
                    throw new AgeException($"Entered age: {age} is incorrect", age);
                ChooseLimits(-1, int.MaxValue, "Enter the sum", out sum);
            }
            
            AccountType accountType = 0;
            string pass = "";
            switch (type)
            {
                case 1:
                    accountType = AccountType.New;
                    break;
                case 2:
                    accountType = AccountType.Registered;
                    Console.WriteLine("Enter your password: ");
                    pass = Console.ReadLine();
                    break;
                case 3:
                    accountType = AccountType.Permanent;
                    Console.WriteLine("Enter your password: ");
                    pass = Console.ReadLine();
                    break;
                case 4:
                    accountType = AccountType.Admin;
                    Console.WriteLine("Enter your password: ");
                    pass = Console.ReadLine();
                    break;
            }
            if (type > 0 && type < 4)
                administration.CreateAccount(accountType, sum, age, name, pass);
            else
               administration.CreateAccount(accountType, name, pass);
        }

        private static void ObtainTransport(Administration administration)
        {
            TransportType transportType = 0;
            ChooseLimits(1,2, "You can choose whether 1)bus or 2)minivan. Which one do you prefer?", out int type);
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
            
            double distance = 0;
            double duration = 0;
            ChooseLimits(1, 5, "Choose your route", out int num);
            if (num<1 || num > 5)
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
            ChooseLimits(1, 2, "Do you want to read additional information about your driver? 1)yes / 2)no", out int choice);
            if(choice == 1)
            {
                administration.DriverInfo();
                MyDelegate mydel = ShowInfo;;
                mydel?.Invoke(administration.newTransp.man, administration);

            }
        }
        private static void ChooseDate(Administration administration)
        {
            ChooseLimits(1, 7, "Enter the appropriate for you day(from 1 to 7):" +
                "\nConsider that buses only drive on odd days, while minivans only on even days", out int num);
            Console.WriteLine($"[{administration.ChooseDate(num)}]");

            int time = 0;
            List<int> hours = administration.ChooseTime();
            Console.WriteLine("There are such arrival times in choosen day:" +
                "\nYou can obtain 1) or 2)");
            foreach (int i in hours)
            {
                Console.Write($"[{i}] \t");
            }
            ChooseLimits(1, 2, "\nChoose one option", out int choice);
            switch (choice)
            {
                case 1:
                    time = hours[0];
                    break;
                case 2:
                    time = hours[1];
                    break;
            }
            Console.WriteLine($"You chose {time}");
            administration.SetTime(time);
        }
        private static void ShowInfo(Ferryman man, Administration administration)
        {
            Console.WriteLine($"Your driver Name : {man.Name}");
            Console.WriteLine($"Age : {man.Age}");
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
        private static void AdminMenu()
        {
            Console.WriteLine("\nHello Admin))))");
            Console.WriteLine("Choose option from mentioned below" +
                "\n1)Find out adittional information about drivers" +
                "\n2)Watch available transports" +
                "\n3)Watch timetables" +
                "\n4)Exit");

        }
        private static void MenInfo()
        {
            foreach (var men in Administration.men)
            {
                Console.WriteLine($"Your driver Name : {men.Name}");
                Console.WriteLine($"Age : {men.Age}");
                Console.WriteLine($"Working hours : {men.WorkHours}");
                Console.WriteLine($"Salary : {men.Salary}");
                Console.WriteLine("==================================");
            }

        }
        private static void TransportInfo()
        {
            int ind = 1;
            foreach (var transp in (TransportType[])Enum.GetValues(typeof(TransportType)))
            {
                Console.WriteLine($"{ind}) {transp}");
            }
                
        }
        private static void WatchTimetables(Administration administration)
        {

            Dictionary<string, List<int>> diction = new Dictionary<string, List<int>>();
            using (var sr = new StreamReader("Hours.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string[] temp = sr.ReadLine().Split();
                    int temp1 = int.Parse(temp[1]);
                    int temp2 = int.Parse(temp[2]);
                    List<int> filedata = new List<int>();
                    filedata.Add(temp1);
                    filedata.Add(temp2);
                    diction.Add(temp[0], filedata);

                }
            }
            foreach (KeyValuePair<string, List<int>> keyValue in diction)
            {
                Console.Write(keyValue.Key + "     \t");
                foreach (var item in keyValue.Value)
                {
                    Console.Write(item + "   \t");
                }
                Console.WriteLine();
            }
        }
        private static void AdminMethods(Administration administration, bool next)
        {
            bool con = false;
            while (!con)
            {
                try
                {
                    AdminMenu();
                    ChooseLimits(1, 4,"Choose one of the options ", out int num);
                    Console.WriteLine();
                    switch (num)
                    {
                        case 1:
                            MenInfo();
                            con = true;
                            break;
                        case 2:
                            TransportInfo();
                            break;
                        case 3:
                            WatchTimetables(administration);
                            break;
                        case 4:
                            Console.WriteLine("Thank you for choosing us");
                            next = true;
                            break;
                    }
                    if(num>0 && num < 4)
                    {

                        ChooseLimits(1, 2, "Do you wanna watch menu for other options? 1)yes 2)false", out int choice);
                        if (choice == 1)
                            con = false;
                        else
                        {
                            Console.WriteLine("Admin, we are waiting to you!");
                            con = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Admin, we are waiting to you!");
                        con = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                    Console.WriteLine("============================================");
                    ChooseLimits(1, 2, "Do you want to try again? 1)yes 2)false", out int choice);
                    if (choice == 1)
                        con = false;
                    else
                        con = true;
                }
                Console.ResetColor();
            }
        }
        static void ChooseLimits(int low, int high, string message, out int choose)
        {
            do
            {
                ChooseOption(out choose, message);
                if (choose < low || choose > high)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please type correct option!");
                    Console.ResetColor();
                }

            } while (choose < low || choose > high);
        }
        static void ChooseOption(out int choose, string message)
        {
            Console.WriteLine(message);
            while (!int.TryParse(Console.ReadLine(), out choose))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please type number symbol!");
                Console.ResetColor();
                Console.WriteLine(message);
            }
        }
    }
}
