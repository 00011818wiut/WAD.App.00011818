using System;
namespace API.Models
{
	public class User : BaseModel
    {
		public string Name { get; set; }

		public string Surname { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		public string Token { get; set; }

		public List<Product> Products { get; set; }
	}
}