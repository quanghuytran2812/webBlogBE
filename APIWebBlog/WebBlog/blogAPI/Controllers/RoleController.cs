using DataAccess.Repository.Implements;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessObject.Models;
using DataAccess.DTO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using DataAccess.DAO;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace blogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ODataController
    {
        private IRoleRepository roleRepository = new RoleRepository();

        //Get All
        [EnableQuery]
        [HttpGet]
        public IActionResult Get()
        {
            {
                return Ok(roleRepository.GetAll());
            }
        }

        //Add
        [Authorize(Roles = "Admin")]
        [EnableQuery]
        [HttpPost]
        public IActionResult Post([FromBody] RoleDTO roleDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                var newRole = new Role
                {
                    role_id = roleDTO.role_id,
                    roleName = roleDTO.roleName
                };
                roleRepository.Add(newRole);
                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Update
        [Authorize(Roles = "Admin")]
        [EnableQuery]
        [HttpPut("{key}")]
        public IActionResult Put(int key, [FromBody] RoleDTO roleDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            try
            {
                var existingRole = roleRepository.GetRoleById(key);
                if (existingRole == null)
                    return NotFound("Role not found.");
                existingRole.role_id = key;
                existingRole.roleName = roleDTO.roleName;


                roleRepository.Update(existingRole);

                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [EnableQuery]
        [HttpDelete("{key}")]
        public IActionResult Delete(int key)
        {
            try
            {
                var existingRole = roleRepository.GetRoleById(key);
                if (existingRole == null)
                    return NotFound("Role not found.");

                roleRepository.Delete(key);
                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
