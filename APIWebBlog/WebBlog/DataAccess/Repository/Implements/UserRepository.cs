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
    public class UserRepository : IUserRepository
    {
        public void Add(SignUpModel user) => UserDAO.Instance.Add(user);

        public void Delete(int id) => UserDAO.Instance.Delete(id);

        public IEnumerable<Users> GetAllUser() => UserDAO.Instance.GetAll();

        public Users GetUsersById(int id) => UserDAO.Instance.GetUserById(id);

        public Users Login(SignInModel model) => UserDAO.Instance.Login(model);

        public void Update(Users users) => UserDAO.Instance.Update(users);
    }
}
