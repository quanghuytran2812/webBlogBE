using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Repository;
using DataAccess.Repository.Implements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace blogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostLikeController : ODataController
    {
        private IPostLikeRepository repository = new PostLikeRepository();
        [EnableQuery]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repository.GetAllPostLike());
        }
        [Authorize]
        [EnableQuery]
        [HttpPost]
        public IActionResult Post([FromBody] PostLikeDTO postLikeDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                var newPostlike = new Post_Like
                {
                    created_time = DateTime.Now,
                    UserId = postLikeDTO.UserId,
                    PostId = postLikeDTO.PostId
                };
                repository.AddPostLike(newPostlike);

                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize]
        [EnableQuery]
        [HttpDelete("{key}")]
        public IActionResult Delete(int key)
        {
            try
            {
                repository.DeletePostLike(key);
                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
