using BusinessObject.Models;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IUserRepository
    {
        IEnumerable<Users> GetAllUser();
        Users GetUsersById(int id);
        public void Add(SignUpModel model);
        public Users Login(SignInModel model);
        void Update(Users users);
        void Delete(int id);
    }
}
