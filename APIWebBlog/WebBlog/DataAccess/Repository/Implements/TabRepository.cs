using BusinessObject.Models;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Implements
{
    public class TabRepository : ITabRepository
    {
        public void Add(Tab tab) => TabDAO.Instance.Add(tab);

        public void Delete(int id) => TabDAO.Instance.Delete(id);

        public IEnumerable<Tab> GetAll() => TabDAO.Instance.GetAll();

        public Tab GetTabById(int id) => TabDAO.Instance.GetTabsById(id);

        public void Update(Tab tab) => TabDAO.Instance.Update(tab);
    }
}
