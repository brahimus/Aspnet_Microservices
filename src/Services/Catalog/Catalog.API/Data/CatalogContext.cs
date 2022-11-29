using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        private readonly IConfiguration _config;

        public CatalogContext(IConfiguration config)
        {
            this._config = config;
            var client = new MongoClient(_config.GetValue<string>("DatabaseSettings:ConnectionString"));

            var database = client.GetDatabase(_config.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(_config.GetValue<string>("DatabaseSettings:CollectionName"));

            CatalogContextSeed.SeedData(Products);

        }
        public IMongoCollection<Product> Products { get; }
    }
}
