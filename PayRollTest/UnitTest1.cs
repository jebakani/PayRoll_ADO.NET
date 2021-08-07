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
            List<EmployeeDetails> list = repo.GetAllData("hello");
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
            int actual = repo.UpdateSalary(2,24000,"query");
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
            List<EmployeeDetails> list = repo.DisplayDataBasedOnDate(startdate,dateTime,"query");
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
            string actual = repo.AggregareteFunction("query");
            //Assert
            Assert.AreEqual(expected, actual);
        }

        //implementing the insert into table method
        [TestMethod]
        public void InsertIntoTableTest()
        {
            int expected = 1;
            //Assign
            EmployeeDetails employee = new EmployeeDetails();
            employee.employeeName = "joyal";
            employee.address = "madurai";
            employee.gender = "male";
            employee.department = "HR";
            employee.startDate = new DateTime(2021, 07, 27).ToLongDateString();
            employee.basicPay = 110000;
            employee.deduction = 2040;
            employee.taxablePay = 1456;
            employee.tax = 2345;
            employee.phoneNumber = 9839883929;
            int actual = repo.InsertIntotable(employee);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        //UC8-Insert into PayrollTable
        [TestMethod]
        public void InsertIntoPayrollTable()
        {
            int expected = 1;
            //Assign
            EmployeeDetails employee = new EmployeeDetails();
            employee.basicPay = 18000;
            employee.employeeId = 11;
            TransactionManagement transaction = new TransactionManagement();
            int actual = transaction.AddingRecord(employee);
            Assert.AreEqual(expected, actual);

        }
        //UC8-Insert into Tables with transaction
        [TestMethod]
        public void InsertIntoTables()
        {
            int expected = 1;
            //Assign
            EmployeeDetails employee = new EmployeeDetails { employeeId=27,employeeName = "Tim", companyId = 1, departmentId = 3, phoneNumber = 8655535615, address = "MGRNagar", city = "madurai", state = "TamilNadu", startDate = "2017-12-05", gender = "M", basicPay = 34000 };
            TransactionManagement transaction = new TransactionManagement();
            int actual = transaction.AddingRecord(employee);
            Assert.AreEqual(expected, actual);

        }

        //--------------Uc10- checking U2-6 in new tables
        //implementing the parameterized test cases
        [DataRow("1 3 6 ", "dbo.countEmployee")] //Count of employee test
        [DataRow( "3000012 73000 ", "dbo.MaximumSalary1")] //maximum salary test
        [DataRow( "10000 15000 ", "dbo.MinimumSalary1")] // minimum salary test
        [DataRow( "770003 44000 ", "dbo.AverageSalary")] // Avg salary test
        [DataRow( "3080012 88000 ", "dbo.SumofSalary1")] //sum of salary test
        [DataTestMethod]
        public void AggregatefunctionTest2( string expected,string procedure)
        {

            //Act
            string actual = repo.AggregareteFunction(procedure);
            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void DisplayDataBasedOnData1()
        {
            int expected = 3;
            DateTime startdate = new DateTime(2020, 07, 20);
            DateTime dateTime = new DateTime(2021, 07, 30);
            List<EmployeeDetails> list = repo.DisplayDataBasedOnDate(startdate, dateTime, "dbo.RetriveBasedOnDate");
            int actual = list.Count();
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        //checking whether the update is passed or not
        public void UpdateSalaryTest1()
        {
            //Assign
            int expected = 1;
            //Act
            int actual = repo.UpdateSalary(2, 24000, "dbo.updatePayroll");
            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        //UC12- Cascading delete
        public void DeleteRecord()
        {
            int expected = 1;
            int actual = new TransactionManagement().DeleteUsingCasadeDelete(4);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        //UC12- Cascading delete
        public void AddindIsActiveField()
        {
            int expected = 1;
            int actual = new TransactionManagement().AddIsActiveColumn();
            Assert.AreEqual(expected, actual);   
        }
        [TestMethod]
        public void RetrivingDataBasedOnIsActiveField()
        {
            int expected = 8;
            List<EmployeeDetails> actual = new TransactionManagement().RetriveDataForAudit("dbo.RetriveAllData");
            Assert.AreEqual(expected, actual.Count);
        }

    }
}
