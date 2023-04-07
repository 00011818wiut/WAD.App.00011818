using System;
using API.Domain;
using API.Models;
using API.DAL;
using System.ComponentModel.Design;

namespace API.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
        private readonly DatabaseContext _dbContext;

        public CategoryRepository(DatabaseContext dbContext)
		{
            _dbContext = dbContext;
		}

        public void Create(Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
        }

        public void Delete(Category category)
        {
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
        }

        public List<Category> FindAll()
        {
            return _dbContext.Categories.ToList();
        }

        public Category? FindById(int id)
        {
            return _dbContext.Categories.Find(id);
        }

        public void Update(Category category)
        {
            _dbContext.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}