using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CompanyAPI.Models
{
    public class Company
    {
        #region PublicProperties

        /// <summary>
        /// 
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("Isin")]
        public string Isin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("Name")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("StockTicker")]
        public string StockTicker { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("Exchange")]
        public string Exchange { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("Website")]
        public string Website { get; set; }
        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public Company()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isin"></param>
        /// <param name="name"></param>
        /// <param name="stockTicker"></param>
        /// <param name="exchange"></param>
        /// <param name="website"></param>
        public Company(string isin, string name, string stockTicker, string exchange, string website)
        {
            Isin = isin;
            Name = name;
            StockTicker = stockTicker;
            Exchange = exchange;
            Website = website;
        }

        #endregion
        /*
         db.Companies.insertMany([{'Isin':'AB12345','Name':'Orange Inc','StockTicker':'Org','Exchange':'Computers','Website':'www.orange.org'}, {'Isin':'AB123467','Name':'Glass Lewis','StockTicker':'GLewis','Exchange':'Computers'}])
         
         
         */
    }
}
