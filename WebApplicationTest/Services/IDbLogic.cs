using System;
using WebApplicationTest.Models;

namespace WebApplicationTest.Services
{
    /// <summary>
    /// Общая логика для бд.
    /// </summary>
    public interface IDbLogic
    {
        /// <summary>
        /// Добавление пользователя.
        /// </summary>
        /// <param name="user"> Пользователь. </param>
        void Add(User user);

        /// <summary>
        /// Проверка на наличие оригнальных данных.
        /// </summary>
        /// <param name="user"> Пользователь. </param>
        /// <returns></returns>
        bool CheckNewUser(User user);
    }
}
