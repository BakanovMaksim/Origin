using System;
using System.Linq;
using WebApplicationTest.Models;
using WebApplicationTest.Data;

namespace WebApplicationTest.Services
{
    public class UsersDbServices : IDbLogic
    {
        public UsersDbContext UsersDbContext { get; }

        public UsersDbServices(UsersDbContext usersDbContext) => UsersDbContext = usersDbContext;

        public void Add(User user)
        {
            if (CheckNewUser(user))
            {
                UsersDbContext.People.Add(user);
                UsersDbContext.SaveChanges();
            }
        }

        public bool CheckNewUser(User user)
        {
            var list = UsersDbContext.People.ToList();
            var count = 0;

            for (int k = 0; k < list.Count; k++)
                if ((list[k].FirstName == user.FirstName) && (list[k].LastName == user.LastName)) ++count;

            return count > 0 ? throw new ArgumentException("Такой пользователь уже существует.", nameof(user)) : true;
        }
    }
}
