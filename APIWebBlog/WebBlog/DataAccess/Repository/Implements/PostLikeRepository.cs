using BusinessObject.Models;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Implements
{
    public class PostLikeRepository : IPostLikeRepository
    {
        public void AddPostLike(Post_Like post_Like) => PostLikeDAO.Instance.Add(post_Like);

        public void DeletePostLike(int id) => PostLikeDAO.Instance.Delete(id);

        public IEnumerable<Post_Like> GetAllPostLike() => PostLikeDAO.Instance.GetAll();
    }
}
