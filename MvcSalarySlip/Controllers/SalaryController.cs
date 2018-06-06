using System;
using System.Web.Mvc;
using MvcSalarySlip.Models;

namespace MvcSalarySlip.Controllers
{
    public class SalaryController : Controller
    {
        //
        // GET: /Salary/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Salary/Details/5

        /// <summary>
        ///     Action method to calculate salary slip based upon parameters.
        /// </summary>
        /// <param name="objSalaryRequest"></param>
        /// <returns></returns>
        public JsonResult CalculateSalary(SalaryRequest objSalaryRequest)
        {
            SalaryResponse objSalaryResponse = null;

            try
            {
                decimal tax, taxableIncome, salaryPerMonth;
                tax = taxableIncome = salaryPerMonth = 0;
                CalculateTax(objSalaryRequest.GrossIncome, ref tax, ref taxableIncome);
                salaryPerMonth = (objSalaryRequest.GrossIncome - tax) / 12;
                objSalaryResponse = new SalaryResponse();
                objSalaryResponse.GrossIncome = objSalaryRequest.GrossIncome / 12;
                objSalaryResponse.IncomeTax = tax / 12;
                objSalaryResponse.NetIncome = objSalaryResponse.GrossIncome - objSalaryResponse.IncomeTax;
                objSalaryResponse.FullName = objSalaryRequest.FirstName + " " + objSalaryRequest.LastName;

                objSalaryResponse.SuperAmount =
                    GetSuperAmount(objSalaryRequest.GrossIncome, objSalaryRequest.SuperRate);

                return Json(objSalaryResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objSalaryResponse = null;
            }
        }

        /// <summary>
        ///     Method is to calculate super amount based upon super rate.
        /// </summary>
        /// <param name="totalSalary"></param>
        /// <param name="superRate"></param>
        /// <returns></returns>
        public decimal GetSuperAmount(decimal totalSalary, decimal superRate)
        {
            var superAmount = totalSalary / 12 * superRate / 100;
            return superAmount;
        }

        /// <summary>
        ///     It define the logic of calculating tax based upon different slabs.
        /// </summary>
        /// <param name="totalSalary"></param>
        /// <param name="tax"></param>
        /// <param name="taxIncome"></param>
        public void CalculateTax(decimal totalSalary, ref decimal tax, ref decimal taxIncome)
        {
            decimal baseTax = 0;
            decimal taxableIncome = 0;
            decimal totalTax = 0;

            if (totalSalary <= 18200)
            {
                //do nothing
            }
            else if (totalSalary >= 18201 && totalSalary <= 37000)
            {
                taxableIncome = totalSalary - 18200;
                totalTax = taxableIncome / 100;
            }
            else if (totalSalary >= 37001 && totalSalary <= 87000)
            {
                baseTax = 3572;
                taxableIncome = totalSalary - 37000;
                totalTax = taxableIncome / 100 * (decimal) 32.5 + baseTax;
            }
            else if (totalSalary >= 87001 && totalSalary <= 180000)
            {
                baseTax = 19822;
                taxableIncome = totalSalary - 87000;
                totalTax = taxableIncome / 100 * 37 + baseTax;
            }
            else if (totalSalary >= 180001)
            {
                baseTax = 54232;
                taxableIncome = totalSalary - 180000;
                totalTax = taxableIncome / 100 * 45 + baseTax;
            }

            tax = totalTax;
            taxIncome = taxableIncome;
        }
    }
}