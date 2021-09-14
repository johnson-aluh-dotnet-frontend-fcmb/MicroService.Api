using Microsoft.EntityFrameworkCore;
using ProductApi.DbContexts;
using ProductApi.Model;
using System.Collections.Generic;
using System.Linq;

namespace ProductApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        /// <summary>
        /// Add constructor and initialize the context class
        /// </summary>
        private readonly ProductContext _context;
        public ProductRepository(ProductContext context)
        {
            _context = context;
        }
        public void DeleteProduct(int Id)
        {
            var product = _context.Products.Find(Id);
            _context.Remove(product);
            _context.SaveChanges();
        }

        public Product GetProductById(int Id)
        {
            var product = _context.Products.Find(Id);
            return product;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public void InsertProduct(Product product)
        {
            _context.Add(product);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            Save();
        }
    }
}
