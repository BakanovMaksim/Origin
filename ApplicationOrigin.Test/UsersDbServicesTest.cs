using ApplicationOrigin.Data;
using ApplicationOrigin.Models;
using ApplicationOrigin.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationOrigin.Test
{
    [TestFixture]
    public class UsersDbServicesTest
    {
        [Test]
        public void GetUsersInputArgumentReturnValue()
        {
            //Arrange
            IQueryable<User> users = new List<User>
            {
                new User
                {
                    Id = 1, FirstName = "Максим", LastName = "Баканов", BirthDate=new DateTime(1998,05,07), Login = "maks", Password = "batlenax", Role = Roles.Administrator, Culture = "en"
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var mockContext = new Mock<UsersDbContext>();
            mockContext.Setup(c => c.People).Returns(mockSet.Object);

            //Act
            var repository = new UsersDbServices(mockContext.Object, new Logger<UsersDbServices>(new LoggerFactory()));
            var actualGetUsers = repository.GetUsers();
            var actualGetUserId = repository.GetUserId(1);
            var actualGetUserLogin = repository.GetUserLogin("maks");
            var result = repository.CheckNewUser(new User { Id = 3, FirstName = "Максим", LastName = "Баканов", BirthDate = new DateTime(1998, 05, 07), Login = "makssg", Password = "batlenaxfgf", Role = Roles.Administrator, Culture = "en" });

            //Assert
            Assert.AreEqual(1, actualGetUsers.Count());
            Assert.IsNotNull(actualGetUserId);
            Assert.IsNotNull(actualGetUserLogin);
            Assert.True(result);
        }

        [Test]
        public void AddEditRemoveInputArgumentResultedValue()
        {
            //Arrange
            DbContextOptions<UsersDbContext> options;
            var builder = new DbContextOptionsBuilder<UsersDbContext>();
            builder.UseInMemoryDatabase(databaseName: "users");
            options = builder.Options;

            var context = new UsersDbContext(options);
            var repository = new UsersDbServices(context, new Logger<UsersDbServices>(new LoggerFactory()));

            //Act
            repository.Add(new User { Id = 1, FirstName = "Максим", LastName = "Баканов", BirthDate = new DateTime(1998, 05, 07), Login = "maks", Password = "batlenax", Role = Roles.Administrator, Culture = "en" });
            repository.Add(new User { Id = 2, FirstName = "Дмитрий", LastName = "Широков", BirthDate = new DateTime(2002, 09, 15), Login = "sgdgds", Password = "gsdgsd", Role = Roles.Visitor, Culture = "ru" });
            repository.Add(new User { Id = 3, FirstName = "Сергей", LastName = "Ореховский", BirthDate = new DateTime(2005, 11, 25), Login = "oreh", Password = "gsdgsdggsdgs", Role = Roles.Visitor, Culture = "ru" });

            var userRemove = repository.GetUserId(3);
            if (userRemove != null) repository.Remove(userRemove);

            if (repository.GetUserId(2) != null)
            {
                var userEdit = repository.GetUserId(2);
                userEdit.FirstName = "Евгений";
                repository.Edit(userEdit);
            }

            var actual = repository.GetUsers();
            var actualEdit = repository.GetUserId(2);

            //Assert
            Assert.AreEqual(2, actual.Count());
            Assert.AreEqual("Евгений", actualEdit.FirstName);
        }
    }
}
