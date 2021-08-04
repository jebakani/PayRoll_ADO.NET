using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRollWithDatabase
{
   public class SqlAggregate
    {
        SqlConnection sqlConnection;
        public SqlAggregate(SqlConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
        }
        public string AgregateFunctionCalculate(string query)
        {
            //passing the query
            string Salary = "";
            using (this.sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    //passing command type as stored procedur
                    this.sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    //if the query has row 
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Salary += "" + reader[0] + " ";
                        }
                    }
                    return Salary;
                }
                catch (Exception e)
                {
                    return e.Message;
                }
                finally
                {
                    this.sqlConnection.Close();
                }
        }
    }
}
