using MongoDbDemo.Model;

namespace MongoDbDemo.Services
{
    public interface IProductServices
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetById(string id);
        Task Create(Product product);
        Task Update(string id, Product product);
        Task Delete(string id);
    }
}
