using System;

namespace PayRollWithDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to pay roll service");
            EmployeeRepo repo = new EmployeeRepo();
            bool Continue = true;
            while(Continue)
            {
                Console.WriteLine("1.Retrive all data\n2.Update salary\n3.Display result between dates\n4.Exit");
                Console.Write("Enter your choice:");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch(choice)
                {
                    case 1:
                        repo.GetAllData();
                        break;
                    case 2:
                        repo.UpdateSalary();
                        break;
                    case 3:
                        repo.DisplayDataBasedOnDate();
                        break;
                    case 4:
                        Continue = false;
                        break;
                    default:
                        break;
                }
            }
            Console.Read();

        }
    }
}
