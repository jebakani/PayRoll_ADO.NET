using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRollWithDatabase
{
    public class PayRollJsonServer
    {
        RestClient client;
        public PayRollJsonServer()
        {
            client = new RestClient("http://localhost:3000");
        }
        IRestResponse GetAllEmployee()
        {
            RestRequest request = new RestRequest("/employees");
            IRestResponse response = client.Execute(request);
            return response;
        }
        public List<EmployeeDetailWithOnlySalary> ReadFromServer()
        {
            IRestResponse response = GetAllEmployee();
            var res = JsonConvert.DeserializeObject<List<EmployeeDetailWithOnlySalary>>(response.Content);
            return res;
        }
    }
}
