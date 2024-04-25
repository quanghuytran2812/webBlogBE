using DataAccess.Repository.Implements;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using BusinessObject.Models;
using DataAccess.DTO;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace blogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ODataController
    {
        private IPostRepository postRepository = new PostRepository();


        [EnableQuery]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(postRepository.GetAll());
        }
        [Authorize]
        [EnableQuery]
        [HttpPost]
        public IActionResult Post([FromBody] PostDTO post)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                var newPost = new Posts
                {
                    thumb = post.thumb,
                    title = post.title,
                    content = post.content,
                    summary = post.summary,
                    published = 1,
                    createdDate= DateTime.Now,
                    UserId = post.UserId,
                    TabId = post.TabId,
                    CategoryId = post.CategoryId
                };
                postRepository.Add(newPost);

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
        public IActionResult Put(int key, [FromBody] PostDTO post)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            try
            {
                var existingPost = postRepository.GetPostsById(key);
                if (existingPost == null)
                    return NotFound("Post not found.");
                existingPost.post_id = key;
                existingPost.thumb = post.thumb;
                existingPost.title = post.title;
                existingPost.content = post.content;
                existingPost.summary = post.summary;
                existingPost.published = post.published;
                existingPost.UserId = post.UserId;
                existingPost.TabId = post.TabId;
                existingPost.CategoryId = post.CategoryId;

                postRepository.Update(existingPost);

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
                var existingPost = postRepository.GetPostsById(key);
                if (existingPost == null)
                    return NotFound("Post not found.");

                postRepository.Delete(key);
                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
