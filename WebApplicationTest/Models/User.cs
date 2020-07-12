using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationTest.Models
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

        public override string ToString() => FirstName;
    }
}
