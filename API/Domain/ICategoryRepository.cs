using System;
using API.Models;

namespace API.Domain
{
	public interface ICategoryRepository
	{
		public void Create(Category category);

        public void Update(Category category);

        public void Delete(Category category);

        public Category? FindById(int id);

        public List<Category> FindAll();
    }
}