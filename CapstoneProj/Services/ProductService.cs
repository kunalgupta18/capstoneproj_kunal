using CapstoneProj.Models;
using CapstoneProj.Repository.Interface;
using CapstoneProj.Services.Interface;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace CapstoneProj.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepository;

        public ProductService(IProductRepo productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
        }

        public Product AddProduct(Product product)
        {

            return _productRepository.AddProduct(product);
        }

        public Product UpdateProduct(Product product)
        {

            return _productRepository.UpdateProduct(product);
        }

        public Product DeleteProduct(int id)
        {
            return _productRepository.DeleteProduct(id);
        }
    }
}