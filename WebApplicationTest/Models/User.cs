using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationOrigin.Models
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификатор пользователя для бд.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        [Required(ErrorMessage ="Пожалуйста, введите имя.")]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        [Required(ErrorMessage ="Пожалуйста, введите фамилию.")]
        public string LastName { get; set; }

        /// <summary>
        /// Логин.
        /// </summary>
        [Required(ErrorMessage ="Пожалуйста, введите логин.")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        [Required(ErrorMessage ="Пожалуйста, введите пароль.")]
        [MinLength(8,ErrorMessage ="Длина пароля должна составлять минимум 8 символов.")]
        public string Password { get; set; }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}
