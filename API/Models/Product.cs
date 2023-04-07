using System;
namespace API.Models
{
	public class Product : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int OwnerId { get; set; }
    }
}