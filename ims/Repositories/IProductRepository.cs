using ims.Models;

namespace ims.Repositories;

public interface IProductRepository
{   
    bool Create(Product product);
    bool Update(Product product);
    Product? Get(string productName);
    bool Delete(string productName);
    bool IsExists(string productName);
    ICollection<Product> GetAll();
}
