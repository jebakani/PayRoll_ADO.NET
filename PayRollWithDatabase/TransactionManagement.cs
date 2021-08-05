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
                    new SqlCommand(employeInsertion, sqlConnection, transaction).ExecuteNonQuery();
                    string retriveEmpId = "select emp_id from Employee where emp_name='" + employee.employeeName + "' and phoneNumber=" + employee.phoneNumber;
                    int empId = RetriveId(retriveEmpId, transaction);
                    string payRollInsertion = "insert into PayRoll(Emp_id,BasicPay,Deduction,TaxablePay,Tax,NetPay) values(" + empId + "," + payRoll.basicPay + "," + payRoll.deduction + "," + payRoll.taxablePay + "," + payRoll.tax + "," + payRoll.netPay + ")";
                    string employeeDepartmentInsertion = "insert into Employee_Department values (" + empId + "," + employee.departmentId + ")";

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
            public int RetriveId(string query, SqlTransaction transaction)
            {
                SqlDataReader reader = new SqlCommand(query, sqlConnection, transaction).ExecuteReader();
                int id = 0;
                if(reader.HasRows)
                {
                        id = Convert.ToInt32(reader["emp_id"]);
                        reader.Close();
                        return id;
                }
                throw new Exception("No data available");
            }
    }
}

