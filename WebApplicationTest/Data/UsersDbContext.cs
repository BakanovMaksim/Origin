﻿using System;
using Microsoft.EntityFrameworkCore;
using WebApplicationTest.Models;

namespace WebApplicationTest.Data
{
    /// <summary>
    /// Контекст пользователя.
    /// </summary>
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) => Database.EnsureCreated();

        /// <summary>
        /// Доступ к таблице people.
        /// </summary>
        public DbSet<User> People { get; set; }
    }
}
