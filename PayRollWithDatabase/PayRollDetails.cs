using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRollWithDatabase
{
    public class PayRollDetails
    {
        public double basicPay;
        public double deduction;
        public double taxablePay;
        public double tax;
        public double netPay;
        public PayRollDetails(double basicPay)
        {
            this.basicPay = basicPay;
            this.deduction = 0.2 * this.basicPay;
            this.taxablePay = this.basicPay-this.deduction;
            this.tax = 0.1 * taxablePay;
            this.netPay = this.basicPay - this.tax;
        }
    }
}
