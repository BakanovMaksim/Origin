using System;
using System.Linq;
using System.Collections.Generic;
using ApplicationOrigin.Models;
using ApplicationOrigin.Data;
using Microsoft.EntityFrameworkCore;

namespace ApplicationOrigin.Services
{
    public class UsersDbServices : IDbLogic
    {
        public UsersDbContext UsersDbContext { get; set; }

        public IEnumerable<User> Users { get; }

        public UsersDbServices(UsersDbContext usersDbContext)
        {
            UsersDbContext = usersDbContext;
            Users = UsersDbContext.People.ToList();
        }

        public void Add(User user)
        {
            if (CheckNewUser(user))
            {
                UsersDbContext.People.Add(user);
                UsersDbContext.SaveChanges();
            }
        }

        public void Edit(User user)
        {
            UsersDbContext.Entry(user).State = EntityState.Modified;
            UsersDbContext.SaveChanges();
        }

        public void Remove(User user)
        {
            UsersDbContext.People.Remove(user);
            UsersDbContext.SaveChanges();
        }

        public bool CheckNewUser(User user)
        {
            var count = 0;

            foreach(var item in Users)
                if ((item.Login == user.Login) && (item.Password == user.Password)) ++count;

            return count > 0 ? throw new ArgumentException("Такой пользователь уже существует.", nameof(user)) : true;
        }
    }
}
