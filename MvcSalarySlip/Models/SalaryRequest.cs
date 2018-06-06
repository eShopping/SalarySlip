using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSalarySlip.Models
{
    public class SalaryRequest
    {
        #region properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PayPeriod { get; set; }
        public decimal GrossIncome { get; set; }
        public decimal SuperRate { get; set; }
        #endregion
    }
}