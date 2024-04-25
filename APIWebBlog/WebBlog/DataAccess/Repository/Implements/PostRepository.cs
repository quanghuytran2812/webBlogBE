using BusinessObject.Models;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Implements
{
    public class PostRepository : IPostRepository
    {
        public IEnumerable<Posts> GetAll() => PostDAO.Instance.GetAll();
        public void Delete(int id) => PostDAO.Instance.Delete(id);
        public void Update(Posts posts) => PostDAO.Instance.Update(posts);
        public void Add(Posts posts) => PostDAO.Instance.Add(posts);

        public Posts GetPostsById(int id) => PostDAO.Instance.GetPostsById(id);
    }
}
