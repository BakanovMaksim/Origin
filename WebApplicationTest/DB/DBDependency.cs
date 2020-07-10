using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using WebApplicationTest.Models;

namespace WebApplicationTest.DB
{
    public class DBDependency : IDBDependency
    {
        public UserContext UserContext { get; }

        public DBDependency(ILogger<DBDependency> logger, UserContext userContext) => UserContext = userContext;

        public void Add(User user)
        {
            UserContext.People.Add(user);
            UserContext.SaveChanges();
        }

        public bool CheckNewUser(User user)
        {
            var list = UserContext.People.ToList();

            for (int k = 0; k < list.Count; k++)
                if ((list[k].FirstName == user.FirstName) && (list[k].LastName == user.LastName)) return false;

            return true;
        }
    }
}
