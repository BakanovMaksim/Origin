using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationTest.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public override string ToString() => FirstName;
    }
}
