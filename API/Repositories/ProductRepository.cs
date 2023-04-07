using System;
using API.DAL;
using API.Domain;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
	public class ProductRepository : IProductRepository
	{
        private readonly DatabaseContext _dbContext;

        public ProductRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void Create(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public void Delete(Product product)
        {
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
        }

        public List<Product> FindAll()
        {
            return _dbContext.Products
                   .Include(product => product.Category)
                   .ToList();
        }

        public Product? FindById(int id)
        {
            return _dbContext.Products.Find(id);
        }

        public List<Product> FindByUser(int userId)
        {
            return _dbContext.Products
                .Include(product => product.Category)
                .Where(product => product.OwnerId == userId).ToList();
        }

        public void Update(Product product)
        {
            _dbContext.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}