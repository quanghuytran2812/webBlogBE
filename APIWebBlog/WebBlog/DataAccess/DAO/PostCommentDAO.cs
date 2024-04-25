using BusinessObject.Data;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class PostCommentDAO
    {
        private static PostCommentDAO instance = null!;
        private static readonly object iLock = new object();
        public PostCommentDAO() { }

        public static PostCommentDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new PostCommentDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Post_Comment> GetAll()
        {
            List<Post_Comment> post_Comments = null!;
            try
            {
                var context = new MyDbContext();
                post_Comments = (from pc in context.Post_Comments
                                 join u in context.Users on pc.UserId equals u.user_id
                                 join p in context.Posts on pc.PostId equals p.post_id
                                 where pc.published == 1
                                 select new Post_Comment
                                 {
                                     postCo_id = pc.postCo_id,
                                     content = pc.content,
                                     published = pc.published,
                                     created_time = pc.created_time,
                                     UserId = pc.UserId,
                                     Users = u,
                                     PostId = pc.PostId,
                                     Posts = p
                                 }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return post_Comments;
        }

        public Post_Comment GetPostCommentById(int id)
        {
            Post_Comment post_Comment = null!;
            try
            {
                var context = new MyDbContext();
                post_Comment = (from pc in context.Post_Comments
                                join u in context.Users on pc.UserId equals u.user_id
                                join p in context.Posts on pc.PostId equals p.post_id
                                where pc.published == 1 && pc.postCo_id == id
                                select new Post_Comment
                                {
                                    postCo_id = pc.postCo_id,
                                    content = pc.content,
                                    published = pc.published,
                                    created_time = pc.created_time,
                                    UserId = pc.UserId,
                                    Users = u,
                                    PostId = pc.PostId,
                                    Posts = p
                                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return post_Comment;
        }
        public void Add(Post_Comment post_Comment)
        {
            try
            {
                var context = new MyDbContext();
                context.Post_Comments.Add(post_Comment);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(Post_Comment post_Comment)
        {
            try
            {
                var post_CommentU = GetPostCommentById(post_Comment.postCo_id);
                if (post_Comment != null)
                {
                    var context = new MyDbContext();
                    context.Entry<Post_Comment>(post_Comment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Post Comment does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Delete(int postCommentId)
        {
            try
            {
                var postcommentD = GetPostCommentById(postCommentId);
                if (postcommentD != null)
                {
                    var context = new MyDbContext();
                    context.Post_Comments.Remove(postcommentD);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Post Comment does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
