using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IPostCommentRepository
    {
        IEnumerable<Post_Comment> GetAllPostComment();
        Post_Comment GetPostCommentById(int id);
        void AddPostComment(Post_Comment post_Comment);
        void UpdatePostComment(Post_Comment post_Comment);
        void DeletePostComment(int id);
    }
}
