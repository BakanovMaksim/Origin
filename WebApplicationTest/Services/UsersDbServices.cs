using ApplicationOrigin.Data;
using ApplicationOrigin.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationOrigin.Services
{
    public class UsersDbServices : IDbLogic
    {
        private readonly ILogger<UsersDbServices> _logger;

        private readonly UsersDbContext _usersDbContext;

        public UsersDbServices(UsersDbContext usersDbContext, ILogger<UsersDbServices> logger)
        {
            _usersDbContext = usersDbContext;
            _logger = logger;
        }

        public User GetUserId(int id)
        {
            _logger.LogInformation("Получен идентификатор пользователя.", id);

            return _usersDbContext.People.FirstOrDefault(p => p.Id == id);
        }

        public User GetUserLogin(string login)
        {
            _logger.LogInformation("Получен логин пользователя.", login);

            return _usersDbContext.People.FirstOrDefault(p => p.Login == login);
        }

        public IEnumerable<User> GetUsers() => _usersDbContext.People.ToList();

        public void Add(User user)
        {
            _logger.LogInformation("Данные пользователя получены.", nameof(user));

            if (CheckNewUser(user))
            {
                _usersDbContext.People.Add(user);
                _usersDbContext.SaveChanges();

                _logger.LogInformation("Пользователь добавлен в базу данных.", nameof(user));
            }

            _logger.LogWarning("Пользователь не добавлен в базу данных.", nameof(user));
        }

        public void Edit(User user)
        {
            _logger.LogInformation("Данные пользователя получены.", nameof(user));

            _usersDbContext.People.Update(user);
            _usersDbContext.SaveChanges();
        }

        public void Remove(User user)
        {
            _logger.LogInformation("Данные пользователя получены.", nameof(user));

            _usersDbContext.People.Remove(user);
            _usersDbContext.SaveChanges();
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
