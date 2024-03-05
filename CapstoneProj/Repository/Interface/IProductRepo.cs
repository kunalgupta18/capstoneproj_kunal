using CapstoneProj.Models;

namespace CapstoneProj.Repository.Interface
{
    public interface IProductRepo
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        Product AddProduct(Product product);
        Product UpdateProduct(Product product);
        Product DeleteProduct(int id);
    }
}
