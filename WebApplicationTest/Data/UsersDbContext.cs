﻿using ApplicationOrigin.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationOrigin.Data
{
    /// <summary>
    /// Контекст пользователя.
    /// </summary>
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

        public UsersDbContext() { }

        /// <summary>
        /// Доступ к таблице people.
        /// </summary>
        public virtual DbSet<User> People { get; set; }
    }
}
