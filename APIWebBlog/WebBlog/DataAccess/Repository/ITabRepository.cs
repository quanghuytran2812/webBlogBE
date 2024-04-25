using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ITabRepository
    {
        IEnumerable<Tab> GetAll();
        Tab GetTabById(int id);
        void Add(Tab tab);
        void Update(Tab tab);
        void Delete(int id);
    }
}
