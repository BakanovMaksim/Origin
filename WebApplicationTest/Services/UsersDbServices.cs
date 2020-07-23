using System;
using System.Linq;
using System.Collections.Generic;
using ApplicationOrigin.Models;
using ApplicationOrigin.Data;
using Microsoft.Extensions.Logging;

namespace ApplicationOrigin.Services
{
    public class UsersDbServices : IDbLogic
    {
        private readonly ILogger<UsersDbServices> _logger;

        public UsersDbContext UsersDbContext { get; }

        public UsersDbServices(UsersDbContext usersDbContext,ILogger<UsersDbServices> logger)
        {
            UsersDbContext = usersDbContext;
            _logger = logger;
        }

        public User GetUserId(int id)
        {
            _logger.LogInformation("Получен идентификатор пользователя.", id);

            return UsersDbContext.People.FirstOrDefault(p => p.Id == id);
        }

        public User GetUserLogin(string login)
        {
            _logger.LogInformation("Получен логин пользователя.", login);

            return UsersDbContext.People.FirstOrDefault(p => p.Login == login);
        }

        public IEnumerable<User> GetUsers() => UsersDbContext.People.ToList();

        public void Add(User user)
        {
            _logger.LogInformation("Данные пользователя получены.", nameof(user));

            if (CheckNewUser(user))
            { 
                UsersDbContext.People.Add(user);
                UsersDbContext.SaveChanges();

                _logger.LogInformation("Пользователь добавлен в базу данных.", nameof(user));
            }

            _logger.LogWarning("Пользователь не добавлен в базу данных.", nameof(user));
        }

        public void Edit(User user)
        {
            _logger.LogInformation("Данные пользователя получены.", nameof(user));

            UsersDbContext.People.Update(user);
            UsersDbContext.SaveChanges();
        }

        public void Remove(User user)
        {
            _logger.LogInformation("Данные пользователя получены.", nameof(user));

            UsersDbContext.People.Remove(user);
            UsersDbContext.SaveChanges();
        }

        public bool CheckNewUser(User user)
        {
            _logger.LogInformation("Данные пользователя получены.", nameof(user));

            var count = 0;

            foreach (var item in GetUsers())
                if ((item.Login == user.Login) && (item.Password == user.Password)) ++count;

            return count > 0 ? throw new ArgumentException("Такой пользователь уже существует.", nameof(user)) : true;
        }
    }
}
