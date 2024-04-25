using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Repository;
using DataAccess.Repository.Implements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Data;

namespace blogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TabController : ODataController
    {
        private ITabRepository repository = new TabRepository();


        //Get All

        [EnableQuery]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repository.GetAll());
        }

        //ADD
        [Authorize(Roles = "Admin")]
        [EnableQuery]
        [HttpPost]
        public IActionResult Post([FromBody] TabDTO tabDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                var newTab = new Tab
                {
                    tabName = tabDTO.tabName,
                    published = 1
                };
                repository.Add(newTab);

                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //UPDATE
        [Authorize(Roles = "Admin")]
        [EnableQuery]
        [HttpPut("{key}")]
        public IActionResult Put(int key, [FromBody] TabDTO tabDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            try
            {
                var existingTab = repository.GetTabById(key);
                if (existingTab == null) 
                    return NotFound("Tab not found.");
                    existingTab.tab_id = key;
                    existingTab.tabName = tabDTO.tabName;
                    existingTab.published = 1;

                    repository.Update(existingTab);

                    return Ok("Success");
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Delete
        [Authorize(Roles = "Admin")]
        [EnableQuery]
        [HttpDelete("{key}")]
        public IActionResult Delete(int key)
        {
            try
            {
                var existingTab = repository.GetTabById(key);
                if (existingTab == null)
                    return NotFound("Tab not found.");

                repository.Delete(key);

                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
