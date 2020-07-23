using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ApplicationOrigin.Services;
using ApplicationOrigin.Data;
using ApplicationOrigin.Models;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Linq;
using Moq;

namespace ApplicationOrigin.Tests
{
    [TestFixture]
    public class UsersDbServiceTests
    {
        [Test]
        public void GetUsers_InputArgument_ReturnValue()
        {
            var users = new List<User>
            {
                new User { Id=1, FirstName="Максим",LastName="Баканов",BirthYear=1998,Login="maks",Password="batlenax",Role="Administrator",Culture="en"},
                new User { Id=2, FirstName="Дмитрий",LastName="Широков",BirthYear=2002,Login="dmitr",Password="afasfmasgfja",Role="Visitor",Culture="ru"},
                new User { Id=3, FirstName="Сергей",LastName="Ореховский",BirthYear=2005,Login="oreh",Password="sdgdfhsdhgsd",Role="Visitor",Culture="ru"},
                new User { Id=4, FirstName="Евгений",LastName="Козлов",BirthYear=2004,Login="sdfs",Password="farfarfarfarfar",Role="Visitor",Culture="ru"}
            };

            var mock = new Mock<UsersDbContext>();
            var db = new UsersDbServices(mock.Object, new Logger<UsersDbServices>(new LoggerFactory()));

            db.Add(new User { Id = 1, FirstName = "Максим", LastName = "Баканов", BirthYear = 1998, Login = "maks", Password = "batlenax", Role = "Administrator", Culture = "en" });

            Assert.AreEqual(1, db.GetUsers().Count());
        }
    }
}



