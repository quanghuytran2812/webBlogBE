using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAll();
        Role GetRoleById(int id);
        void Add(Role role);
        void Update(Role role);
        void Delete(int id);

    }
}
