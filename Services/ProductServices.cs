using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbDemo.Model;

namespace MongoDbDemo.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IOptions<DatabaseSettings> _dbsetting;

        public ProductServices(IOptions<DatabaseSettings> dbsetting)
        {
            _dbsetting = dbsetting;
            var mongoClient = new MongoClient(dbsetting.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dbsetting.Value.DatabaseName);
            _productCollection = mongoDatabase.GetCollection<Product>
                (dbsetting.Value.ProductsCollectionName);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            //return await _CategoryCollection.Find(_ => true).ToListAsync();
            var pipeLine = new BsonDocument[]
            {
                new BsonDocument("$lookup",new BsonDocument
                {
                    {"from","categorycollection" },
                    {"localField","CategoryId" },
                    {"foreignField","_id" },
                    {"as","product_category" }
                }),
                new BsonDocument("$unwind","$product_category"),
                new BsonDocument("$project",new BsonDocument
                {
                    {"_id",1 },
                    {"CategoryId",1 },
                    {"ProductName",1 },
                    {"CategoryName","$product_category.CategoryName" }
                })
            };
            var results = await _productCollection.Aggregate<Product >(pipeLine).ToListAsync();
            return results;
        }
        public async Task<Product> GetById(string id)
        {
            var product = await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return product;
        }
        public async Task Create(Product product)
        {
            await _productCollection.InsertOneAsync(product);
        }
        public async Task Update(string id, Product product)
        {
            await _productCollection.ReplaceOneAsync(a => a.Id == id, product);
        }
        public async Task Delete(string id)
        {
            await _productCollection.DeleteOneAsync(a => a.Id == id);
        }
    }
}
