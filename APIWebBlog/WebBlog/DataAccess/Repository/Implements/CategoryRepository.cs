using BusinessObject.Models;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Implements
{
    public class CategoryRepository : ICategoryRepository
    {
        public void Add(Category category) => CategoryDAO.Instance.Add(category);

        public void Delete(int id) => CategoryDAO.Instance.Delete(id);

        public IEnumerable<Category> GetAll() => CategoryDAO.Instance.GetAll();

        public Category GetCategoryById(int id) => CategoryDAO.Instance.GetCategoryById(id);

        public void Update(Category category) => CategoryDAO.Instance.Update(category);
    }
}
