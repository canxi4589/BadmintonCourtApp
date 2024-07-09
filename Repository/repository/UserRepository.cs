using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.repository
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(DBContext context) : base(context)
        {
        }

        public  User Login(string username, string password)
        {
            return Context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public async Task<User> Register(User user)
        {
            await Context.Set<User>().AddAsync(user);
            await Context.SaveChangesAsync();
            return user;
        }

        public  User FindByEmail(string email)
        {
            return  Context.Users.FirstOrDefault(u => u.Gmail == email);
        }

        public void UpdatePassword(User user, string newPassword)
        {
            user.Password = newPassword;
             Context.SaveChanges();
        }
    }
}
