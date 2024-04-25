using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IPostRepository
    {
        IEnumerable<Posts> GetAll();
        public Posts GetPostsById(int id);
        void Add(Posts posts);
        void Update(Posts posts);
        void Delete(int id);
    }
}
