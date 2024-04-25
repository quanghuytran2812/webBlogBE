using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Implements
{
    public class RoleRepository : IRoleRepository
    {
        public void Add(Role role) => RoleDAO.Instance.Add(role);

        public void Update(Role role) => RoleDAO.Instance.Update(role);

        public IEnumerable<Role> GetAll() => RoleDAO.Instance.GetAll();

        public void Delete(int id) => RoleDAO.Instance.Delete(id);

        public Role GetRoleById(int id) => RoleDAO.Instance.GetRoleById(id);
    }
}
