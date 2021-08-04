using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayRollWithDatabase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PayRollTest
{
    [TestClass]
    public class PayrollTestClass
    {
        EmployeeRepo repo;
       [TestInitialize]
        public void setup()
        {
            repo = new EmployeeRepo();
        }
        //checking whether all data is retrived
        [TestMethod]
        public void RetriveAllDetails()
        {
            //Assign
            int expected = 6 ;
            //Act
            List<EmployeeDetails> list = repo.GetAllData();
            int actual = list.Count();
            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        //checking whether the update is passed or not
        public void UpdateSalaryTest()
        {
            //Assign
            int expected = 1;
            //Act
            int actual = repo.UpdateSalary();
            //Assert
            Assert.AreEqual(expected, actual);
        }
        //Checking the result of the retrival based on date 
        [TestMethod]
        public void DisplayDataBasedOnData()
        {
            int expected = 3;
            DateTime startdate = new DateTime(2020, 07, 20);
            DateTime dateTime = new DateTime(2021, 07, 30);
            List<EmployeeDetails> list = repo.DisplayDataBasedOnDate(startdate,dateTime);
            int actual = list.Count();
            Assert.AreEqual(expected, actual);

        }
        //implementing the parameterized test cases
        [DataRow(1,"4 2 ")] //Count of employee test
        [DataRow(2,"3000012 73000 ")] //maximum salary test
        [DataRow(3,"10000 15000 ")] // minimum salary test
        [DataRow(4, "770003 44000 ")] // Avg salary test
        [DataRow(5, "3080012 88000 ")] //sum of salary test
        [DataTestMethod]
        public void AggregatefunctionTest(int choice,string expected)
        {
           
            //Act
            string actual = repo.AggregareteFunction(choice);
            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
