using System;
using System.Collections.Generic;
using ApplicationOrigin.Models;

namespace ApplicationOrigin.Services
{
    /// <summary>
    /// Общая логика для бд.
    /// </summary>
    public interface IDbLogic
    {
        /// <summary>
        /// Список пользователей.
        /// </summary>
        IEnumerable<User> Users { get; }

        /// <summary>
        /// Добавление пользователя.
        /// </summary>
        /// <param name="user"> Пользователь. </param>
        void Add(User user);

        /// <summary>
        /// Изменение данных пользователя.
        /// </summary>
        /// <param name="user"> Пользователь. </param>
        void Edit(User user);

        /// <summary>
        /// Удаление пользователя.
        /// </summary>
        /// <param name="user"> Пользователь. </param>
        void Remove(User user);

        /// <summary>
        /// Проверка на наличие оригнальных данных.
        /// </summary>
        /// <param name="user"> Пользователь. </param>
        /// <returns></returns>
        bool CheckNewUser(User user);
    }
}
