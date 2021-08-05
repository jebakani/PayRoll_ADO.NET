using System;

namespace PayRollWithDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to pay roll service");
            EmployeeRepo repo = new EmployeeRepo();
            Console.WriteLine("1.Retrive all data\n2.Update salary\n3.Display result between dates\n4.Aggregate Function\n5.Exit");
            Console.Write("Enter your choice:");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    repo.GetAllData();
                    break;
                case 2:
                    repo.UpdateSalary();
                    break;
                case 3:
                    DateTime startdate = new DateTime(2020, 07, 20);
                    DateTime dateTime = new DateTime(2021, 07, 30);
                    repo.DisplayDataBasedOnDate(startdate,dateTime);
                    break;
                case 4:
                    Console.WriteLine(  ""+ repo.AggregareteFunction(1));
                    break;

                default:
                    break;
            }
            Console.Read();
        }
    }
}
