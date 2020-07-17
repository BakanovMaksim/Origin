using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationOrigin.Models
{
    /// <summary>
    /// Роли.
    /// </summary>
    public enum Roles
    {
        /// <summary>
        /// Администратор.
        /// </summary>
        [Display(Name = "Администратор")]
        Administrator,

        /// <summary>
        /// Посетитель.
        /// </summary>
        [Display(Name ="Посетитель")]
        Visitor
    }
}
