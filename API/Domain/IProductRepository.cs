using System;
using API.Models;

namespace API.Domain
{
	public interface IProductRepository
	{
        public void Create(Product product);

        public void Update(Product product);

        public void Delete(Product product);

        public Product? FindById(int id);

        public List<Product> FindAll();

        public List<Product> FindByUser(int userId);
    }
}

