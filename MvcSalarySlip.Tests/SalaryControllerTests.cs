using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcSalarySlip.Controllers;

namespace MvcSalarySlip.Tests
{
    [TestClass]
    public class SalaryControllerTests
    {
        private readonly SalaryController _controller = new SalaryController();
        private decimal _grossIncome = Decimal.Zero;

        [TestMethod]
        public void CalculateTaxNullSlab()
        {
            //Arrange
            _grossIncome = 18200;
            decimal tax = 0;
            decimal taxableIncome = 0;

            //Act
            _controller.CalculateTax(_grossIncome, ref tax, ref taxableIncome);

            //Assert
            Assert.AreEqual(tax, taxableIncome);
        }

        [TestMethod]
        public void CalculateTaxNullSlabAndGetTax()
        {
            //Arrange
            _grossIncome = 18200;
            decimal tax = 0;
            decimal taxableIncome = 0;

            //Act
            _controller.CalculateTax(_grossIncome, ref tax, ref taxableIncome);

            //Assert
            Assert.AreEqual(tax, taxableIncome);
            Assert.AreNotEqual(1, tax);
        }

        /// <summary>
        ///     Slab between $18,201 – $37,000
        ///     19c for each $1 over $18,200
        /// </summary>
        [TestMethod]
        public void CalculateTaxTaxableIncomeBetweenSecondSlabReturnNotNull()
        {
            //Arrange
            _grossIncome = 18500;
            decimal tax = 0;
            decimal taxableIncome = 0;

            //Act
            _controller.CalculateTax(_grossIncome, ref tax, ref taxableIncome);

            //Assert
            Assert.IsNotNull(tax);
        }

        [TestMethod]
        public void CalculateTaxTaxableIncomeBetweenSecondSlabReturnTax()
        {
            //Arrange
            _grossIncome = 18500;
            decimal tax = 0;
            decimal taxableIncome = 0;

            //Act
            _controller.CalculateTax(_grossIncome, ref tax, ref taxableIncome);

            //Assert
            Assert.AreEqual(3, tax);
        }

        /// <summary>
        ///     $37,001 – $87,000
        ///     $3,572 plus 32.5c for each $1 over $37,000
        /// </summary>
        [TestMethod]
        public void CalculateTaxThirdSlab()
        {
            //Arrange
            _grossIncome = 35000;
            decimal tax = 0;
            decimal taxableIncome = 0;
            decimal baseTax = 3572;

            //Act
            _controller.CalculateTax(_grossIncome, ref tax, ref taxableIncome);

            //Assert
            Assert.AreNotEqual(tax, baseTax);
        }

        [TestMethod]
        public void CalculateTaxThirdSlabReturnTax()
        {
            //Arrange
            _grossIncome = 35000;
            decimal tax = 0;
            decimal taxableIncome = 0;
            decimal baseTax = 3572;

            //Act
            _controller.CalculateTax(_grossIncome, ref tax, ref taxableIncome);

            //Assert
            Assert.AreNotEqual(tax, baseTax);
            Assert.AreEqual(168, tax);
        }

        /// <summary>
        ///     $87,001 – $180,000
        ///     $19,822 plus 37c for each $1 over $87,000
        /// </summary>
        [TestMethod]
        public void CalculateTaxForthSlab()
        {
            //Arrange
            _grossIncome = 86000;
            decimal tax = 0;
            decimal taxableIncome = 0;
            decimal baseTax = 19822;

            //Act
            _controller.CalculateTax(_grossIncome, ref tax, ref taxableIncome);

            //Assert
            Assert.AreNotEqual(tax, baseTax);
        }

        [TestMethod]
        public void CalculateTaxForthSlabReturnTax()
        {
            //Arrange
            _grossIncome = 86000;
            decimal tax = 0;
            decimal taxableIncome = 0;
            decimal baseTax = 19822;

            //Act
            _controller.CalculateTax(_grossIncome, ref tax, ref taxableIncome);

            //Assert
            Assert.AreNotEqual(tax, baseTax);
            Assert.AreEqual(19497.0M, tax);
        }

        /// <summary>
        ///     $87,001 – $180,000
        ///     $19,822 plus 37c for each $1 over $87,000
        /// </summary>
        [TestMethod]
        public void CalculateTaxFifthSlab()
        {
            //Arrange
            _grossIncome = 190000;
            decimal tax = 0;
            decimal taxableIncome = 0;
            decimal baseTax = 54232;

            //Act
            _controller.CalculateTax(_grossIncome, ref tax, ref taxableIncome);

            //Assert
            Assert.AreNotEqual(tax, baseTax);
        }

        [TestMethod]
        public void CalculateTaxFifthSlabReturnTax()
        {
            //Arrange
            _grossIncome = 190000;
            decimal tax = 0;
            decimal taxableIncome = 0;
            decimal baseTax = 54232;

            //Act
            _controller.CalculateTax(_grossIncome, ref tax, ref taxableIncome);

            //Assert
            Assert.AreNotEqual(tax, baseTax);
            Assert.AreEqual(58732M, tax);
        }

        [TestMethod]
        public void CalculateSuperAmount()
        {
            //Arrange
            _grossIncome = 190000;
            decimal superRate = 9;
            var result = _controller.GetSuperAmount(_grossIncome, superRate);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CalculateSuperAmountReturnValue()
        {
            //Arrange
            _grossIncome = 190000;
            decimal superRate = 9;
            var result = _controller.GetSuperAmount(_grossIncome, superRate);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1425.00000000000000000000000M, result);
        }

        [TestMethod]
        public void CheckSuperRateZero()
        {
            //Arrange
            _grossIncome = 190000;
            decimal superRate = 0;

            //Act
            var result = _controller.GetSuperAmount(_grossIncome, superRate);

            //Assert
            Assert.IsTrue(result == 0);
        }
    }
}