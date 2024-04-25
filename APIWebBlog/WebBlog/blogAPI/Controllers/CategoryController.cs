using BusinessObject.Data;
using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Repository;
using DataAccess.Repository.Implements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Data;

namespace blogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ODataController
    {
        private ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        [EnableQuery]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(categoryRepository.GetAll());
        }
        //[Authorize(Roles = "Admin")]
        [EnableQuery]
        [HttpPost]
        public IActionResult Post([FromBody]  CategoryDTO category)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                var newCategory = new Category {
                    categoryName = category.categoryName,
                    published = 1
                };
                categoryRepository.Add(newCategory);

                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //[Authorize(Roles = "Admin")]
        [EnableQuery]
        [HttpPut("{key}")]
        public IActionResult Put(int key, [FromBody] CategoryDTO category)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            try
            {
                var existingCategory = categoryRepository.GetCategoryById(key);
                if (existingCategory == null)
                    return NotFound("Category not found.");

                existingCategory.category_id = key;
                existingCategory.categoryName = category.categoryName;
                existingCategory.published = 1;

                categoryRepository.Update(existingCategory);

                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //[Authorize(Roles = "Admin")]
        [EnableQuery]
        [HttpDelete("{key}")]
        public IActionResult Delete(int key)
        {
            try
            {
                var existingCategory = categoryRepository.GetCategoryById(key);
                if (existingCategory == null)
                    return NotFound("Category not found.");

                categoryRepository.Delete(key);
              
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}

