using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLegacyApp.Core.Model
{
    public class LoanRequest
    {
        public decimal Principal { get; set; }
        public decimal AnnualInterestRate { get; set; }
        public int NumberOfPayments { get; set; }
        public LoanRequest(decimal principal, decimal annualInterestRate, int numberOfPayments)
        {
            Principal = principal;
            AnnualInterestRate = annualInterestRate;
            NumberOfPayments = numberOfPayments;
        }
    }
}
