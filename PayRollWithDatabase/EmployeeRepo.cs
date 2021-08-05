using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRollWithDatabase
{
    public class EmployeeRepo
    {
        List<EmployeeDetails> employeeList = new List<EmployeeDetails>();
        /// <summary>
        /// Creating the connection
        /// </summary>
        public static string connectionString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=Employee_Payroll_Database";
        //creating the object for sql connection class
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public List<EmployeeDetails>  GetAllData(string query)
        {
            //opening the sql connection
            sqlConnection.Open();
            //create the query to display data
            
            //create object for employee detail class
            EmployeeDetails employee = new EmployeeDetails();
            try
            {
                //create the sql command object nd pass the querry and connection
                SqlCommand command = new SqlCommand(query, sqlConnection);
                //create data reader 
                SqlDataReader reader = command.ExecuteReader();
                //if it has data
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        //store each data in the employee details properties 
                        employee.employeeId = Convert.ToInt32(reader["id"]);
                        employee.employeeName = reader["name"].ToString();
                        employee.gender = reader["gender"].ToString();
                        employee.startDate = reader.GetDateTime(2).ToString(); ;
                        employee.phoneNumber = Convert.ToDouble(reader["phoneNumber"]);
                        employee.address = reader.GetString(5);
                        employee.netPay= Convert.ToDouble(reader["netPay"]);
                        //display the result
                        Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} ",employee.employeeId,employee.employeeName,employee.gender,employee.startDate,employee.phoneNumber,employee.address,employee.netPay);
                        employeeList.Add(employee);
                    }
                    reader.Close();
                    return employeeList;
                }
                else
                {
                    reader.Close();
                    return employeeList;
                }
                
            }
            //if any exception occurs catch and display exception message
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            //finally close the connection
            finally
            {
                sqlConnection.Close();
            }
        }
        public int UpdateSalary(int basePay,int id,string procedureName)
        {
          
            using (sqlConnection)
                try
                {
                    //passing query in terms of stored procedure
                    SqlCommand sqlCommand = new SqlCommand(procedureName, sqlConnection);
                    //passing command type as stored procedure
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    //adding the parameter to the strored procedure
                    sqlCommand.Parameters.AddWithValue("@id", id);
                    sqlCommand.Parameters.AddWithValue("@basicPay", basePay);
                    //checking the result 
                    int result = sqlCommand.ExecuteNonQuery();
                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally 
                {
                    sqlConnection.Close();
                }
        }
        public List<EmployeeDetails> DisplayDataBasedOnDate(DateTime startdate, DateTime dateTime,string procedureName)
        {
            EmployeeDetails employee = new EmployeeDetails();
            

            using (sqlConnection)
                try
                {
                    //passing query in terms of stored procedure
                    SqlCommand sqlCommand = new SqlCommand(procedureName, sqlConnection);
                    //passing command type as stored procedure
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    //adding the parameter to the strored procedure
                    sqlCommand.Parameters.AddWithValue("@startDate",startdate);
                    sqlCommand.Parameters.AddWithValue("@endDate",dateTime);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    //if it has data
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //store each data in the employee details properties 
                            employee.employeeId = Convert.ToInt32(reader["emp_id"]);
                            employee.employeeName = reader["emp_name"].ToString();
                            employee.gender = reader["gender"].ToString();
                            employee.startDate = reader.GetDateTime(2).ToString();
                            employee.phoneNumber = Convert.ToDouble(reader["phoneNumber"]);
                            employee.address = reader.GetString(5);
                            //display the result
                            Console.WriteLine("{0} {1} {2} {3} {4} {5}", employee.employeeId, employee.employeeName, employee.gender, employee.startDate, employee.phoneNumber, employee.address);
                            employeeList.Add(employee);
                        }
                        reader.Close();
                        return employeeList;
                    }
                    else
                    {
                        reader.Close();
                        return employeeList;
                    }
                    
                }
                //if any exception occurs catch and display exception message
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                //finally close the connection
                finally
                {
                    sqlConnection.Close();
                }
        }
        public string AggregareteFunction(string procedureName)
        {
            string data = new SqlAggregate(sqlConnection).AgregateFunctionCalculate(procedureName);
            return data;
        }

        public int InsertIntotable(EmployeeDetails employee)
        {
            using (sqlConnection)
                try
                {
                    //passing query in terms of stored procedure
                    SqlCommand sqlCommand = new SqlCommand("dbo.InsertIntoTable1", sqlConnection);
                    //passing command type as stored procedure
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    //adding the parameter to the strored procedure
                    sqlCommand.Parameters.AddWithValue("@employeeName", employee.employeeName);
                    sqlCommand.Parameters.AddWithValue("@startDate", employee.startDate);
                    sqlCommand.Parameters.AddWithValue("@gender", employee.gender);
                    sqlCommand.Parameters.AddWithValue("@phonenumber", employee.phoneNumber);
                    sqlCommand.Parameters.AddWithValue("@address", employee.address);
                    sqlCommand.Parameters.AddWithValue("@department", employee.department);
                    sqlCommand.Parameters.AddWithValue("@Deduction", employee.deduction);
                    sqlCommand.Parameters.AddWithValue("@Tax", employee.tax);
                    sqlCommand.Parameters.AddWithValue("@TaxablePay", employee.taxablePay);
                    sqlCommand.Parameters.AddWithValue("@basicPay", employee.basicPay);
                    //checking the result 
                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                        return 1;
                    else
                        return 0;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
        }


    }
}
