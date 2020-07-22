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
        [Display(Name = "FirstName")]
        [Required(ErrorMessage = "FirstNameRequired")]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        [Display(Name = "LastName")]
        [Required(ErrorMessage = "LastNameRequired")]
        public string LastName { get; set; }

        /// <summary>
        /// Год рождения.
        /// </summary>
        [Display(Name ="Год рождения")]
        [Required(ErrorMessage ="Пожалуйста, введите год рождения.")]
        public int BirthYear { get; set; }

        /// <summary>
        /// Логин.
        /// </summary>
        [Display(Name = "Login")]
        [Required(ErrorMessage = "LoginRequired")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        [Display(Name = "Password")]
        [Required(ErrorMessage = "PasswordRequired")]
        [MinLength(8,ErrorMessage = "PasswordMinLength")]
        public string Password { get; set; }

        /// <summary>
        /// Культура.
        /// </summary>
        [Display(Name ="Культура")]
        public string Culture { get; set; }

        /// <summary>
        /// Роль.
        /// </summary>
        [Display(Name = "Role")]
        [Required(ErrorMessage = "RoleRequired")]
        public string Role { get; set; }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}
