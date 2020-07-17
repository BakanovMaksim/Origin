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
        [Display(Name ="Имя")]
        [Required(ErrorMessage ="Пожалуйста, введите имя.")]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        [Display(Name ="Фамилия")]
        [Required(ErrorMessage ="Пожалуйста, введите фамилию.")]
        public string LastName { get; set; }

        /// <summary>
        /// Логин.
        /// </summary>
        [Display(Name ="Логин")]
        [Required(ErrorMessage ="Пожалуйста, введите логин.")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        [Display(Name ="Пароль")]
        [Required(ErrorMessage ="Пожалуйста, введите пароль.")]
        [MinLength(8,ErrorMessage ="Длина пароля должна составлять минимум 8 символов.")]
        public string Password { get; set; }

        /// <summary>
        /// Роль.
        /// </summary>
        [Display(Name ="Роль")]
        [Required(ErrorMessage ="Пожалуйста, выберете роль.")]
        public Roles Role { get; set; }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}
