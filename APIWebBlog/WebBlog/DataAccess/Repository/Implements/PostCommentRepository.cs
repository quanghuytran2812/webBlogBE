using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Implements
{
    public class PostCommentRepository : IPostCommentRepository
    {
        public void DeletePostComment(int id) => PostCommentDAO.Instance.Delete(id);
        public void UpdatePostComment(Post_Comment post_Comment) => PostCommentDAO.Instance.Update(post_Comment);
        public void AddPostComment(Post_Comment post_Comment) => PostCommentDAO.Instance.Add(post_Comment);

        public IEnumerable<Post_Comment> GetAllPostComment() => PostCommentDAO.Instance.GetAll();

        public Post_Comment GetPostCommentById(int id) => PostCommentDAO.Instance.GetPostCommentById(id);
    }
}
