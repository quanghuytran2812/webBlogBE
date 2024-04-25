using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Repository.Implements;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.DAO;
using Microsoft.OData;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Authorization;

namespace blogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostCommentController : ODataController
    {
        private IPostCommentRepository postCommentRepository = new PostCommentRepository();

      
        [EnableQuery]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(postCommentRepository.GetAllPostComment());
        }
        [Authorize]
        [EnableQuery]
        [HttpPost]
        public IActionResult Post([FromBody] PostCommentDTO postCommentDTO) { 
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                var newPostComment = new Post_Comment
                {
                    content = postCommentDTO.content,
                    created_time = DateTime.Now,
                    published = 1,
                    UserId = postCommentDTO.UserId,
                    PostId = postCommentDTO.PostId,
                    
                };
                postCommentRepository.AddPostComment(newPostComment);

                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize]
        [EnableQuery]
        [HttpPut("{key}")]
        public IActionResult Put(int key, [FromBody] PostCommentDTO postCommentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            try
            {
                var existingPostComment = postCommentRepository.GetPostCommentById(key);
                
                if (existingPostComment == null)
                    return NotFound("Post Comment not found.");

                existingPostComment.postCo_id = key;               
                existingPostComment.content = postCommentDTO.content;
                existingPostComment.created_time = postCommentDTO.created_time;
                existingPostComment.published = postCommentDTO.published;
                existingPostComment.UserId = postCommentDTO.UserId;
                existingPostComment.PostId = postCommentDTO.PostId;

                postCommentRepository.UpdatePostComment(existingPostComment);

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
                var existingPostComment = postCommentRepository.GetPostCommentById(key);
                if (existingPostComment == null)
                    return NotFound("Post Comment not found.");

                postCommentRepository.DeletePostComment(key);
                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
