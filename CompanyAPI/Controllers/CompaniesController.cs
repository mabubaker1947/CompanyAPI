using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CompanyAPI.Models;
using CompanyAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly CompanyService _companyService;

        public CompaniesController(CompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public ActionResult<List<Company>> Get()
        {
            return _companyService.Get();
        }

        //[HttpGet("{Isin:length(24)}", Name = "GetCompany")]
        [HttpGet("GetCompanyByIsin")]
        public ActionResult<Company> GetCompanyByIsin([RegularExpression(@"^[a-zA-Z]{2}[A-Za-z0-9]*$")] string isin)
        {
            var company = _companyService.GetCompanyByIsin(isin);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        [HttpGet("GetCompanyById")]
        public ActionResult<Company> GetCompanyById([RegularExpression(@"^[a-zA-Z0-9]{24}$", ErrorMessage = "Id must be 24 alpha numeric digits")] string id)
        {
            var company = _companyService.GetCompanyById(id);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        //[HttpPost]
        //public ActionResult<Company> Create(Company company)
        //{
        //    // Todo: put on IsIn validations
        //    _companyService.Create(company);

        //    return CreatedAtRoute("GetCompany", new {Isin = company.Isin.ToString()}, company);
        //}

        [HttpPost]
        public ActionResult<Company> CreateCompany([FromBody]JObject comp)
        {
            // Todo: put on IsIn validations
            var company = new Company(comp["Isin"].ToObject<string>(), comp["Name"].ToObject<string>(),
            comp["StockTicker"].ToObject<string>(), comp["Exchange"].ToObject<string>(), comp["Website"].ToObject<string>());
            _companyService.Create(company);

            return company;
        }

        [HttpPut]
        public IActionResult Update(string Isin, Company companyIn)
        {
            var company = _companyService.GetCompanyByIsin(Isin);

            if (company == null)
            {
                return NotFound();
            }

            _companyService.Update(Isin, companyIn);

            return Accepted();
        }

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