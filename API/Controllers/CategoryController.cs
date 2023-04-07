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
    [Route("api/categories")]
    public class CategoryController : Controller
	{
        private readonly IHttpContextAccessor _accessor;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(
            IHttpContextAccessor accessor,
            ICategoryRepository categoryRepository)
		{
            _accessor = accessor;
            _categoryRepository = categoryRepository;
		}

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _categoryRepository.FindAll();
            return new OkObjectResult(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _categoryRepository.FindById(id);

            if (category == null)
            {
                return new NotFoundObjectResult("Category not found");
            }

            return new OkObjectResult(category);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Category category, [FromHeader] string accessToken)
        {
            using (var scope = new TransactionScope())
            {
                _categoryRepository.Create(category);
                scope.Complete();

                return new OkObjectResult("Category created");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Category category, [FromHeader] string accessToken)
        {
            if (category != null)
            {
                var exists = _categoryRepository.FindById(id);

                if (exists == null)
                {
                    return new NotFoundResult();
                }

                exists.Name = category.Name;

                using (var scope = new TransactionScope())
                {
                    _categoryRepository.Update(exists);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromHeader] string accessToken)
        {
            var category = _categoryRepository.FindById(id);
            if (category != null)
            {
                _categoryRepository.Delete(category);
                return new OkResult();
            }

            return new NoContentResult();
        }
    }
}

