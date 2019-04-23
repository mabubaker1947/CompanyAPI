using CompanyAPI.Models;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCompanyAPI
{
    /// <summary>
    /// Class that tests the functionality of the <see cref="Company"/> class
    /// </summary>
    /// <author>Muhammad Abubaker</author>
    [TestClass]
    public class CompanyTest
    {
        #region PrivateFields
        private const string Isin = "AB812345";
        private const string Name = "Ornage inc";
        private const string StockTier = "ORG";
        private const string Exchange = "Cell phones";
        private const string Website = "www.orange.org";
        #endregion

        #region TestMethods
        /// <summary>
        /// Test that the constructor creates a non null instance
        /// </summary>
        [TestMethod]
        public void Test_Constructor_InstanceIsNotNull()
        {
            //Arrange & Act
            Company company = new Company(Isin, Name, StockTier, Exchange, Website);

            // Asserrt
            Assert.IsNotNull(company, nameof(company));
        }

        /// <summary>
        /// Test that the public proeprties are set properly while object creation
        /// </summary>
        [TestMethod]
        public void Test_Properties_Are_Set_OnCreation()
        {
            // Arrange & Act
            Company company = new Company(Isin, Name, StockTier, Exchange, Website);

            // Assert
            Assert.AreEqual(Isin, company.Isin, nameof(Isin));
            Assert.AreEqual(Name, company.Name, nameof(Name));
            Assert.AreEqual(StockTier, company.StockTicker, nameof(StockTier));
            Assert.AreEqual(Exchange, company.Exchange, nameof(Exchange));
            Assert.AreEqual(Website, company.Website, nameof(Website));
        }
        #endregion
    }
}
