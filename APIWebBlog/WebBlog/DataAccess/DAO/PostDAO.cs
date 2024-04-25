using BusinessObject.Data;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class PostDAO
    {
        private static PostDAO instance = null!;
        private static readonly object iLock = new object();
        public PostDAO() { }

        public static PostDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new PostDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Posts> GetAll()
        {
            List<Posts> posts = null!;
            try
            {
                var context = new MyDbContext();
                posts = (from p in context.Posts
                         join u in context.Users on p.UserId equals u.user_id
                         join c in context.Categories on p.CategoryId equals c.category_id
                         join t in context.Tabs on p.TabId equals t.tab_id
                         where p.published == 1
                         select new Posts
                         {
                             post_id = p.post_id,
                             thumb = p.thumb,
                             title = p.title,
                             content = p.content,
                             summary = p.summary,
                             published = p.published,
                             createdDate = p.createdDate,
                             UserId = p.UserId,
                             Users = u,
                             TabId = p.TabId,
                             Tab = t,
                             CategoryId = p.CategoryId,
                             Category = c
                         }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return posts;
        }
        public Posts GetPostsById(int id)
        {
            Posts posts = null!;
            try
            {
                var context = new MyDbContext();
                posts = (from p in context.Posts
                         join u in context.Users on p.UserId equals u.user_id
                         join c in context.Categories on p.CategoryId equals c.category_id
                         join t in context.Tabs on p.TabId equals t.tab_id
                         where p.published == 1 && p.post_id == id
                         select new Posts
                         {
                             post_id = p.post_id,
                             thumb = p.thumb,
                             title = p.title,
                             content = p.content,
                             summary = p.summary,
                             published = p.published,
                             createdDate = p.createdDate,
                             UserId = p.UserId,
                             Users = u,
                             TabId = p.TabId,
                             Tab = t,
                             CategoryId = p.CategoryId,
                             Category = c
                         }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return posts;
        }

        public void Add(Posts post)
        {
            try
            {
                var context = new MyDbContext();
                context.Posts.Add(post);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Posts posts)
        {
            try
            {
                var post = GetPostsById(posts.post_id);
                if (posts != null)
                {
                    var context = new MyDbContext();
                    context.Entry<Posts>(posts).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Post does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Delete(int postId)
        {
            try
            {
                var post = GetPostsById(postId);
                if (post != null)
                {
                    var context = new MyDbContext();
                    context.Posts.Remove(post);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Post does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
