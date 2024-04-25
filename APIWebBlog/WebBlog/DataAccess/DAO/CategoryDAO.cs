using BusinessObject.Data;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance = null!;
        private static readonly object iLock = new object();
        public CategoryDAO() { }

        public static CategoryDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Category> GetAll()
        {
            List<Category> categories;
            try
            {
                var context = new MyDbContext();
                categories = context.Categories.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return categories;
        }
        public Category GetCategoryById(int id)
        {
            Category category = null!;
            try
            {
                var context = new MyDbContext();
                category = context.Categories.FirstOrDefault(b => b.category_id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return category;
        }
        public void Add(Category category)
        {
            try
            {
                var context = new MyDbContext();
                context.Categories.Add(category);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(Category categorys)
        {
            try
            {
                var category = GetCategoryById(categorys.category_id);
                if (category != null)
                {
                    var context = new MyDbContext();
                    context.Entry<Category>(categorys).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Category does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Delete(int categoryid)
        {
            try
            {
                var category = GetCategoryById(categoryid);
                if (category != null)
                {
                    var context = new MyDbContext();
                    context.Categories.Remove(category);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Category does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
