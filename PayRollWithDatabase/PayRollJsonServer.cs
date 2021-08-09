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
        //writing into the server 
        public void WriteIntoJsonServer(EmployeeDetailWithOnlySalary employee)
        {
            RestRequest request = new RestRequest("/employees", Method.POST);
            //creating the json object
            JsonObject json = new JsonObject();
            //adding the data to json object
            json.Add("id", employee.id);
            json.Add("name", employee.name);
            json.Add("salary", employee.salary);
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var res = JsonConvert.DeserializeObject<EmployeeDetailWithOnlySalary>(response.Content);
            Console.WriteLine("" + res.id+"Added");
        }
        //Adding multiple data to the server
        public void AddingMultipleContactToServer(List<EmployeeDetailWithOnlySalary> employees)
        {
            foreach (var employee in employees)
            {
                WriteIntoJsonServer(employee);
            }
        }
    }
}
