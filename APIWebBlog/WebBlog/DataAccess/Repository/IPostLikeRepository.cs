using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IPostLikeRepository
    {
        IEnumerable<Post_Like> GetAllPostLike();
        void AddPostLike(Post_Like post_Like);
        void DeletePostLike(int id);
    }
}
