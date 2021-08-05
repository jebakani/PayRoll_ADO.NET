using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//insertig record in the two table without transaction 

namespace PayRollWithDatabase
{
    public class TransactionManagement
    {
        public static string connectionString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=Employee_Payroll_Database";
        //creating the object for sql connection class
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public int AddingRecord(EmployeeDetails employee)
        {
            EmployeeDetails details = employee;
            PayRollDetails payRoll = new PayRollDetails(employee.basicPay);
            using (sqlConnection)
            {
                sqlConnection.Open();
                //creating object for transaction class and begin transaction
                SqlTransaction transaction = sqlConnection.BeginTransaction();
                try
                {
                    //executing the query
                    string employeInsertion = "insert into Employee(emp_name,company_id,phoneNumber,address,city,state,startDate,gender) values ('" + employee.employeeName + "'," + employee.companyId + "," + employee.phoneNumber + ",'" + employee.address + "','" + employee.city + "','" + employee.state + "','" + employee.startDate + "','" + employee.gender + "')";
                    string payRollInsertion = "insert into PayRoll(Emp_id,BasicPay,Deduction,TaxablePay,Tax,NetPay) values(" + employee.employeeId + "," + payRoll.basicPay + "," + payRoll.deduction + "," + payRoll.taxablePay + "," + payRoll.tax + "," + payRoll.netPay + ")";
                    string employeeDepartmentInsertion = "insert into Employee_Department values (" + employee.employeeId + "," + employee.departmentId + ")";
                    new SqlCommand(employeInsertion, sqlConnection, transaction).ExecuteNonQuery();
                    new SqlCommand(payRollInsertion, sqlConnection, transaction).ExecuteNonQuery();
                    new SqlCommand(employeeDepartmentInsertion, sqlConnection, transaction).ExecuteNonQuery();
                    //if all query is successfull commit
                    transaction.Commit();
                    return 1;
                }
                catch (Exception e)
                {
                    //else roll back
                    transaction.Rollback();
                    return 0;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
           
    }
}

