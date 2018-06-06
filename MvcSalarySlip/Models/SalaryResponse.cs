using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSalarySlip.Models
{
    public class SalaryResponse
    {
        #region properties
        public string FullName { get; set; }
        public string PayPeriod { get; set; }
        public decimal GrossIncome { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal NetIncome { get; set; }
        public decimal SuperAmount { get; set; }
        #endregion
    }
}