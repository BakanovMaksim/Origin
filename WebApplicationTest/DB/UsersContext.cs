using System;
using Microsoft.EntityFrameworkCore;
using WebApplicationTest.Models;

namespace WebApplicationTest.DB
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) => Database.EnsureCreated();

        public DbSet<User> People { get; set; }
    }
}
