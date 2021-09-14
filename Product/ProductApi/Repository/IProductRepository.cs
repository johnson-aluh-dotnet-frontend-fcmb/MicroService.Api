using ProductApi.Model;
using System.Collections.Generic;

namespace ProductApi.Repository
{
    interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProductById(int Id);
        void InsertProduct(Product product);
        void DeleteProduct(int Id);
        void UpdateProduct(Product product);
        void Save();
    }
}
