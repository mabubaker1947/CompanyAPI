using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyAPI.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CompanyAPI.Services
{
    public class CompanyService
    {
        private readonly IMongoCollection<Company> _companies;

        public CompanyService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("CompanyStoreDb"));
            var database = client.GetDatabase("CompanyStoreDb");
            _companies = database.GetCollection<Company>("Companies");
        }

        public List<Company> Get()
        {
            return _companies.Find(Company => true).ToList();
        }

        public Company GetCompanyByIsin(string isin)
        {
            return _companies.Find<Company>(Company => Company.Isin == isin).FirstOrDefault();
        }

        public Company GetCompanyById(string id)
        {
            return _companies.Find<Company>(Company => Company.Id == id).FirstOrDefault();
        }

        public Company Create(Company Company)
        {
            _companies.InsertOne(Company);
            return Company;
        }

        public void Update(string id, Company CompanyIn)
        {
            _companies.ReplaceOne(Company => Company.Id == id, CompanyIn);
        }

        public void Remove(Company CompanyIn)
        {
            _companies.DeleteOne(Company => Company.Isin == CompanyIn.Isin);
        }

        public void Remove(string isin)
        {
            _companies.DeleteOne(Company => Company.Isin == isin);
        }
    }
}
