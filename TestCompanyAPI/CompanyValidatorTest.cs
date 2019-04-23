using System;
using System.Collections.Generic;
using System.Text;
using CompanyAPI.Services;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace TestCompanyAPI
{
    /// <summary>
    /// Class that tests the functionality of <see cref="CompanyValidator"/>
    /// </summary>
    [TestClass]
    public class CompanyValidatorTest
    {
        #region TestMethods

        /// <summary>
        /// Test that the correct format of Isin returns true after validations
        /// </summary>
        [TestMethod]
        public void Test_ValidateIsin()
        {
            // Arrange
            string message;
            const string isin = "AB1234";

            //Act
            var isValid = CompanyValidator.ValidateIsin(isin, out message);

            //Assert
            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Test that the incorrect format of Isin returns false with a proper error message
        /// </summary>
        [TestMethod]
        public void Test_ValidateIsin_returnErroMessage()
        {
            // Arrange
            string message;
            const string isin = "A1234";

            //Act
            var isValid = CompanyValidator.ValidateIsin(isin, out message);

            //Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(CompanyValidator.IsinErrorMessage, message, nameof(message));
        }

        /// <summary>
        /// Test that the incorrect format of Isin returns false with a proper error message
        /// </summary>
        [TestMethod]
        public void Test_Validate_returnsFlase_IfInvalidObjectWasPassed()
        {
            // Arrange
            string message;
            JObject CompanyObject = new JObject();
            CompanyObject.Add("Isin","R1234");
            CompanyObject.Add("Name", "Res=nault inc");
            CompanyObject.Add("StockTicker", "ren");
            CompanyObject.Add("Exchange", "cars");
            CompanyObject.Add("Website", "www.renault.com");

            //Act
            var isValid = CompanyValidator.Validate(CompanyObject, out message);

            //Assert
            Assert.IsFalse(isValid, nameof(isValid));
            Assert.AreEqual(CompanyValidator.IsinErrorMessage, message, nameof(message));
        }

        /// <summary>
        /// Test that the incorrect format of Isin returns false with a proper error message
        /// </summary>
        [TestMethod]
        public void Test_Validate_returnsFlase_IfNullObjectWasPassed()
        {
            // Arrange
            string message;
            JObject CompanyObject = new JObject();
            CompanyObject.Add("Isin", "R1234");
            CompanyObject.Add("Name", "Res=nault inc");
            CompanyObject.Add("StockTicker", null);
            CompanyObject.Add("Exchange", "cars");
            CompanyObject.Add("Website", "www.renault.com");

            //Act
            var isValid = CompanyValidator.Validate(CompanyObject, out message);

            //Assert
            Assert.IsFalse(isValid, nameof(isValid));
            Assert.AreEqual(CompanyValidator.IsinErrorMessage, message, nameof(message));
        }

        #endregion

    }
}
