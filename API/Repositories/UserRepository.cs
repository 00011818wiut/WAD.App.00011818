using System;
using API.DAL;
using API.Domain;
using API.Models;

namespace API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DatabaseContext _dbContext;

        public UserRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public User? FindUserByEmail(string email)
        {
            return _dbContext.Users
                .Where(user => user.Email == email)
                .FirstOrDefault();
        }

        public List<User> FindAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public User? FindUserByToken(string token)
        {
            return _dbContext.Users
                .Where(user => user.Token == token)
                .FirstOrDefault();
        }
    }
}