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
        [Display(Name = "NameAdmin")]
        Administrator,

        /// <summary>
        /// Посетитель.
        /// </summary>
        [Display(Name = "NameVisitor")]
        Visitor
    }
}
