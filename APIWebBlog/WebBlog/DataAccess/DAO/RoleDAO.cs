using BusinessObject.Data;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class RoleDAO
    {
        private static RoleDAO instance = null!;
        private static readonly object iLock = new object();
        public RoleDAO() { }

        public static RoleDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new RoleDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Role> GetAll()
        {
            List<Role> roles;
            try
            {
                var context = new MyDbContext();
                roles = context.Roles.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return roles;
        }
        public Role GetRoleById(int id)
        {
            Role role = null!;
            try
            {
                var context = new MyDbContext();
                role = context.Roles.FirstOrDefault(b => b.role_id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return role;
        }
        public void Add(Role role)
        {
            try
            {
                var context = new MyDbContext();
                context.Roles.Add(role);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(Role role)
        {
            try
            {
                var id = GetRoleById(role.role_id);
                if (id != null)
                {
                    var context = new MyDbContext();
                    context.Entry<Role>(role).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Role does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int roleid)
        {
            try
            {
                var id = GetRoleById(roleid);
                if (id != null)
                {
                    var context = new MyDbContext();
                    context.Roles.Remove(id);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Role does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
