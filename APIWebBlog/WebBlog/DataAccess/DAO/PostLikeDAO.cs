using BusinessObject.Data;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class PostLikeDAO
    {
        private static PostLikeDAO instance = null!;
        private static readonly object iLock = new object();
        public PostLikeDAO() { }

        public static PostLikeDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new PostLikeDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Post_Like> GetAll()
        {
            List<Post_Like> post_Like = null!;
            try
            {
                var context = new MyDbContext();
                post_Like = (from pc in context.Post_Likes
                                 join u in context.Users on pc.UserId equals u.user_id
                                 join p in context.Posts on pc.PostId equals p.post_id
                                 select new Post_Like
                                 {
                                     postL_id = pc.postL_id,
                                     PostId = pc.PostId,
                                     Posts = p,
                                     UserId = pc.UserId,
                                     Users = u,                                 
                                     created_time = pc.created_time,
                                 }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return post_Like;
        }

        public void Add(Post_Like post_Like)
        {
            try
            {
                var context = new MyDbContext();
                context.Post_Likes.Add(post_Like);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var context = new MyDbContext();
                var postLikeP = context.Post_Likes.FirstOrDefault(p => p.postL_id == id);
                if (postLikeP != null)
                {
                    
                    context.Post_Likes.Remove(postLikeP);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Post Like does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
