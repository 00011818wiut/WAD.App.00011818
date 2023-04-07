using System;
using API.Models;

namespace API.Domain
{
	public interface IUserRepository
	{
		public void CreateUser(User user);
		public User? FindUserByEmail(string email);
        public User? FindUserByToken(string token);
        public List<User> FindAllUsers();
	}
}

