using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CompanyAPI.Services
{
    /// <summary>
    /// Validator for company class data
    /// </summary>
    /// <author>Muhammad Abubaker</author>
    public static class CompanyValidator
    {
        public const string IsinErrorMessage = "First two letters of isin must be no-numeric";
        public const string IdErrorMessage = "Id must be 24 alpha numeric digits";
        public const string ElementNullErrorMessage = "Element must be non null";
        private const string Regex_Isin = @"^[a-zA-Z]{2}[A-Za-z0-9]*$";

        /// <summary>
        /// Validates a company object
        /// </summary>
        /// <returns>true if data can be used to create a new insatnce</returns>
        public static bool Validate(JObject companyJObject, out string message)
        {
            message = string.Empty;
            //var IsCompanyDataValid = false;
            string isin = companyJObject["Isin"].ToObject<string>();
            string name = companyJObject["Name"].ToObject<string>();
            string stock = companyJObject["StockTicker"].ToObject<string>();
            string exchnage = companyJObject["Exchange"].ToObject<string>();

            if (!ValidateIsin(isin, out message))
            {
                return false;
            }

            if ( isin == null || name == null || stock == null || exchnage == null)
            {
                message = ElementNullErrorMessage;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validates Isin of a company against pre-defined rules/conventions
        /// </summary>
        /// <param name="isin"></param>
        /// <param name="message"></param>
        /// <returns>true if valid</returns>
        public static bool ValidateIsin(string isin, out string message)
        {
            message = string.Empty;

            if (isin.Equals(string.Empty) || !System.Text.RegularExpressions.Regex.IsMatch(isin,Regex_Isin))
            {
                message = IsinErrorMessage;
                return false;
            }
            return true;
        }
    }
}
