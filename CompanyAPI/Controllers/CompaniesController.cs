using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CompanyAPI.Models;
using CompanyAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly CompanyService _companyService;

        /// <summary>
        /// initializer for company controller
        /// </summary>
        /// <param name="companyService"></param>
        public CompaniesController(CompanyService companyService)
        {
            _companyService = companyService;
        }

        /// <summary>
        /// Gets all instances of companies from database
        /// </summary>
        /// <returns>company list</returns>
        [HttpGet]
        public ActionResult<List<Company>> Get()
        {
            return _companyService.Get();
        }

        /// <summary>
        /// Gets company infomration against given isin
        /// </summary>
        /// <param name="isin"></param>
        /// <returns>ActionResult</returns>
        [HttpGet("GetCompanyByIsin")]
        public ActionResult<Company> GetCompanyByIsin([RegularExpression(@"^[a-zA-Z]{2}[A-Za-z0-9]*$", ErrorMessage = CompanyValidator.IsinErrorMessage)] string isin)
        {
            var company = _companyService.GetCompanyByIsin(isin);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        /// <summary>
        /// Gets company infomration against given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ActionResult</returns>
        [HttpGet("GetCompanyById")]
        public ActionResult<Company> GetCompanyById([RegularExpression(@"^[a-zA-Z0-9]{24}$", ErrorMessage = CompanyValidator.IdErrorMessage)] string id)
        {
            var company = _companyService.GetCompanyById(id);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        /// <summary>
        /// creates a new company object
        /// </summary>
        /// <param name="companyJObject"></param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public string CreateCompany([FromBody]JObject companyJObject)
        {
            var message = string.Empty;
            if (!CompanyValidator.Validate(companyJObject, out message))
            {
                return message;
            }

            string isin = companyJObject["Isin"].ToObject<string>();

            // return if Isin already exists
            if (_companyService.GetCompanyByIsin(isin) != null)
            {
                return "ISIN already exists";
            }

            string name = companyJObject["Name"].ToObject<string>();
            string stock = companyJObject["StockTicker"].ToObject<string>();
            string exchange = companyJObject["Exchange"].ToObject<string>();
            string website = companyJObject["Website"].ToObject<string>() ?? string.Empty;

            var company = new Company(isin, name, stock, exchange, website);
            _companyService.Create(company);

            return $"{company.Isin}: Successfully created";
        }

        /// <summary>
        /// Updates a given company object data
        /// </summary>
        /// <param name="Isin"></param>
        /// <param name="companyIn"></param>
        /// <returns>ActionResult</returns>
        [HttpPut]
        public IActionResult Update(string Isin, Company companyIn)
        {
            var message = string.Empty;
            if (!CompanyValidator.Validate(JObject.FromObject(companyIn), out message))
            {
                return BadRequest(message);
            }

            var company = _companyService.GetCompanyByIsin(Isin);

            if (company == null)
            {
                return NotFound();
            }

            _companyService.Update(Isin, companyIn);

            return Accepted();
        }

        /// <summary>
        /// deletes a company data by its isin
        /// </summary>
        /// <param name="Isin"></param>
        /// <returns>ActionResult</returns>
        [HttpDelete]
        public IActionResult Delete(string Isin)
        {
            var company = _companyService.GetCompanyByIsin(Isin);

            if (company == null)
            {
                return NotFound();
            }

            _companyService.Remove(company.Isin);

            return Accepted();
        }
    }
}