using MongoDbDemo.Model;

namespace MongoDbDemo.Services
{
    public interface ICategoryServices
    {
         Task<IEnumerable<Category>> GetAllAsync();
         Task<Category> GetById(string id);
         Task Create(Category category);
         Task Update(string id, Category category);
         Task Delete(string id);
    }
}
