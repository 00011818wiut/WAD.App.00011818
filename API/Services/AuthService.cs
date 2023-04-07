using System;
using System.Security.Cryptography;
using System.Text;

using API.Domain;

namespace API.Services
{
	public class AuthService : IAuthService
	{
		public AuthService()
		{
		}

        public string GenerateToken(string email, string password)
        {
            var data = email + ":" + password;

			using (var md5 = MD5.Create())
			{
                var sourceBytes = Encoding.UTF8.GetBytes(data);

                var hashBytes = md5.ComputeHash(sourceBytes);

                var hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

                return hash;
            }
        }
    }
}

