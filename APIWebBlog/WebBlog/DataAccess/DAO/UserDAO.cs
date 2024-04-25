using BusinessObject.Data;
using BusinessObject.Models;
using DataAccess.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class UserDAO
    {
        private static UserDAO instance = null!;
        private static readonly object iLock = new object();
        public UserDAO() { }

        public static UserDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Users> GetAll()
        {
            List<Users> users = null!;
            try
            {
                var context = new MyDbContext();
                users = (from u in context.Users
                         join r in context.Roles on u.RoleId equals r.role_id                  
                         select new Users
                         {
                             user_id = u.user_id,
                             thumb = u.thumb,
                             email_address = u.email_address,
                             fullName = u.fullName,
                             phone = u.phone,
                             password= u.password,
                             salt= u.salt,
                             active = u.active,
                             created_time = u.created_time,
                             lastLogin = u.lastLogin,
                             RoleId = u.RoleId,
                             Role = r
                         }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return users;
        }

        public Users GetUserById(int id)
        {
            Users user = null!;
            try
            {
                var context = new MyDbContext();
                user = (from u in context.Users
                        join r in context.Roles on u.RoleId equals r.role_id
                        where u.user_id == id
                        select new Users
                        {
                            user_id = u.user_id,
                            thumb = u.thumb,
                            email_address = u.email_address,
                            fullName = u.fullName,
                            phone = u.phone,
                            password = u.password,
                            salt = u.salt,
                            active = u.active,
                            created_time = u.created_time,
                            lastLogin = u.lastLogin,
                            RoleId = u.RoleId,
                            Role = r
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }
        public void Add(SignUpModel model)
        {
            try
            {
                var context = new MyDbContext();
                if (context.Users.Any(u => u.email_address == model.Email))
                {
                    throw new Exception("The email already exist.");
                }
                else
                {
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+-={}[]|\\:;\"'<>,.?/";
                    var saltR = new char[32];
                    using (var rng = RandomNumberGenerator.Create())
                    {
                        byte[] bytes = new byte[32];
                        rng.GetBytes(bytes);
                        for (int i = 0; i < saltR.Length; i++)
                        {
                            saltR[i] = chars[bytes[i] % chars.Length];
                        }
                    }
                    var newUser = new Users
                    {
                        thumb = "face1.jpg",
                        email_address = model.Email,
                        fullName = model.FullName,
                        phone = model.Phone,
                        password = ComputeHmacSha512(model.Password.Trim(), new string(saltR)),
                        salt = new string(saltR),
                        active = 1,
                        created_time = DateTime.Now,
                        lastLogin = DateTime.Now,
                        RoleId = 3
                    };
                    context.Users.Add(newUser);
                    context.SaveChanges();
                }              
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Users Login(SignInModel model)
        {
            Users user = null!;
            try
            {
                var context = new MyDbContext();
                user = context.Users.Include(u => u.Role).FirstOrDefault(u => u.email_address == model.Email);
                if (user == null)
                {
                    throw new Exception("This account doesn't exist.");
                }
                string pass = ComputeHmacSha512(model.Password.Trim(), user.salt.Trim());
                if (user.password != pass)
                {
                    throw new Exception("This account doesn't exist.");
                }

                user.lastLogin= DateTime.Now;
                context.Update(user);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public void Update(Users users)
        {
            try
            {
                var id = GetUserById(users.user_id);
                if (id != null)
                {
                    var context = new MyDbContext();
                    context.Entry<Users>(users).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The User does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Delete(int userId)
        {
            try
            {
                var id = GetUserById(userId);
                if (id != null)
                {
                    var context = new MyDbContext();
                    context.Users.Remove(id);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The User does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string ComputeHmacSha512(string message, string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                var messageBytes = Encoding.UTF8.GetBytes(message);
                var hash = hmac.ComputeHash(messageBytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
