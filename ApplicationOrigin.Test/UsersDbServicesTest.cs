using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Microsoft.Extensions.Logging;
using ApplicationOrigin.Models;
using ApplicationOrigin.Data;
using Microsoft.EntityFrameworkCore;
using ApplicationOrigin.Services;

namespace ApplicationOrigin.Test
{
    [TestFixture]
    public class UsersDbServicesTest
    {
        [Test]
        public void GetUsers_InputArgument_ReturnValue()
        {
            //Arrange
            IQueryable<User> users = new List<User>
            {
                new User
                {
                    Id = 1, FirstName = "Максим", LastName = "Баканов", BirthYear = 1998, Login = "maks", Password = "batlenax", Role = "Administrator", Culture = "en"
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
            var result = repository.CheckNewUser(new User { Id = 3, FirstName = "Максим", LastName = "Баканов", BirthYear = 1998, Login = "makssg", Password = "batlenaxfgf", Role = "Administrator", Culture = "en" });

            //Assert
            Assert.AreEqual(1, actualGetUsers.Count());
            Assert.IsNotNull(actualGetUserId);
            Assert.IsNotNull(actualGetUserLogin);
            Assert.True(result);
        }

        [Test]
        public void Add_InputArgument_AddValue()
        {
            // Arrange
            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<UsersDbContext>();

            mockContext.Setup(m => m.People).Returns(mockSet.Object);

            // Act
            var repository = new UsersDbServices(mockContext.Object, new Logger<UsersDbServices>(new LoggerFactory()));
            repository.Add(new User { Id = 1, FirstName = "Максим", LastName = "Баканов", BirthYear = 1998, Login = "maks", Password = "batlenax", Role = "Administrator", Culture = "en" });

            // Assert
            Assert.AreEqual(1, repository.GetUsers().Count());
        }
    }
}
