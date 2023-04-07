using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using API.Domain;
using API.Models;
using API.Repositories;

namespace API.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
	{
        private readonly IHttpContextAccessor _accessor;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public ProductsController(
            IHttpContextAccessor accessor,
            ICategoryRepository categoryRepository,
            IProductRepository productRepository)
		{
            _accessor = accessor;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
		}

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productRepository.FindAll();
            return new OkObjectResult(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productRepository.FindById(id);

            if (product == null)
            {
                return new NotFoundObjectResult("Product not found");
            }

            return new OkObjectResult(product);
        }

        [HttpGet("user")]
        public IActionResult GetByUser([FromHeader] string accessToken)
        {
            if (_accessor.HttpContext == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            User? user = (User?)_accessor.HttpContext.Items["user"];

            if (user == null)
            {
                return new ForbidResult("User not found");
            }

            var products = _productRepository.FindByUser(user.ID);

            return new OkObjectResult(products);
        }

        [HttpPost]
        public IActionResult Create([FromHeader] string accessToken, [FromBody] Product product)
        {
            if (_accessor.HttpContext == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            User? user = (User?)_accessor.HttpContext.Items["user"];

            if (user == null)
            {
                return new ForbidResult("User not found");
            }

            var category = _categoryRepository.FindById(product.CategoryId);

            if (category == null)
            {
                return new NotFoundResult();
            }

            product.OwnerId = user.ID;
            product.CategoryId = product.CategoryId;

            using (var scope = new TransactionScope())
            {
                _productRepository.Create(product);
                scope.Complete();

                return new OkObjectResult("Product created");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromHeader] string accessToken, [FromBody] Product product)
        {
            if (product != null)
            {
                var exists = _productRepository.FindById(id);

                if (exists == null)
                {
                    return new NotFoundResult();
                }

                var category = _categoryRepository.FindById(product.CategoryId);

                if (category == null)
                {
                    return new NotFoundResult();
                }

                exists.Name = product.Name;
                exists.Description = product.Name;
                exists.Price = product.Price;
                exists.CategoryId = product.CategoryId;

                using (var scope = new TransactionScope())
                {
                    _productRepository.Update(exists);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromHeader] string accessToken)
        {
            var product = _productRepository.FindById(id);
            if (product != null)
            {
                _productRepository.Delete(product);
                return new OkResult();
            }

            return new NoContentResult();
        }
    }
}