using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRollWithDatabase
{
   public class EmployeeDetails
    {
        public int employeeId{get;set;}
        public int companyId { get; set; }
        public int departmentId { get; set; }
        public string employeeName { get; set; }
        public string startDate { get; set; }
        public string gender { get; set; }
        public double phoneNumber { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public string city { get; set; }

        public string department { get; set; }
        public double basicPay { get; set; }
        public double deduction { get; set; }
        public double taxablePay { get; set; }
        public double tax { get; set; }
        public double netPay { get; set; }

        //transaction
        public int isActive { get; set; }

    }
}
