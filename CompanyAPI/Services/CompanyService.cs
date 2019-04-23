using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyAPI.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CompanyAPI.Services
{
    /// <summary>
    /// Service class that provides functionality for CRUD operation of a <see cref="Company"/> class
    /// </summary>
    /// <author>Muhammad Abubaker</author>
    public class CompanyService
    {
        private readonly IMongoCollection<Company> _companies;

        /// <summary>
        /// Creates an instance of Company service
        /// </summary>
        /// <param name="config"></param>
        public CompanyService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("CompanyStoreDb"));
            var database = client.GetDatabase("CompanyStoreDb");
            _companies = database.GetCollection<Company>("Companies");
        }

        /// <summary>
        /// List all available companies from Database
        /// </summary>
        /// <returns>list of company</returns>
        public List<Company> Get()
        {
            return _companies.Find(Company => true).ToList();
        }

        /// <summary>
        /// retrieve a company by its Isin number
        /// </summary>
        /// <param name="isin"></param>
        /// <returns></returns>
        public Company GetCompanyByIsin(string isin)
        {
            return _companies.Find<Company>(Company => Company.Isin == isin).FirstOrDefault();
        }

        /// <summary>
        /// retrieve a company by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Company GetCompanyById(string id)
        {
            return _companies.Find<Company>(Company => Company.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// creates a new company record
        /// </summary>
        /// <param name="Company"></param>
        /// <returns></returns>
        public Company Create(Company Company)
        {
            _companies.InsertOne(Company);
            return Company;
        }

        /// <summary>
        /// updates a company by its isin number
        /// </summary>
        /// <param name="isin"></param>
        /// <param name="CompanyIn"></param>
        public void Update(string isin, Company CompanyIn)
        {
            _companies.ReplaceOne(Company => Company.Isin == isin, CompanyIn);
        }

        /// <summary>
        /// removes a company object
        /// </summary>
        /// <param name="CompanyIn"></param>
        public void Remove(Company CompanyIn)
        {
            _companies.DeleteOne(Company => Company.Isin == CompanyIn.Isin);
        }

        /// <summary>
        /// removes a company by its isin
        /// </summary>
        /// <param name="isin"></param>
        public void Remove(string isin)
        {
            _companies.DeleteOne(Company => Company.Isin == isin);
        }
    }
}
