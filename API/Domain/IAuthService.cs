using System;

namespace API.Domain
{
	public interface IAuthService
	{
		public string GenerateToken(string email, string password);
	}
}