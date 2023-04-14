using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbDemo.Model;

namespace MongoDbDemo.Services
{
    public class CategoryServices:ICategoryServices
    {
        private readonly IMongoCollection<Category> _CategoryCollection;
        private readonly IOptions<DatabaseSettings> _dbsetting;

        public CategoryServices(IOptions<DatabaseSettings >dbsetting)
        {
            _dbsetting = dbsetting;
            var mongoClient = new MongoClient(dbsetting.Value.ConnectionString );
            var mongoDatabase =mongoClient.GetDatabase (dbsetting.Value.DatabaseName );
            _CategoryCollection = mongoDatabase.GetCollection<Category>
                (dbsetting.Value.CategoriesCollectionName);
        }
        public async Task <IEnumerable <Category>> GetAllAsync()
        {
           return  await _CategoryCollection.Find (_=>true ).ToListAsync ();
        }
        public async Task<Category >GetById(string  id)
        {
            var category = await _CategoryCollection.Find (x=>x.Id ==id).FirstOrDefaultAsync ();
            return category;
        }
        public async Task  Create (Category category)
        {
             await  _CategoryCollection.InsertOneAsync (category);
        }
        public async Task Update (string id, Category category)
        {
            await _CategoryCollection.ReplaceOneAsync(a => a.Id == id, category);
        }
        public async Task Delete (string id)
        {
            await _CategoryCollection.DeleteOneAsync (a => a.Id == id);
        }
    }
}
