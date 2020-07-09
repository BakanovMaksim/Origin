using System;
using System.Linq;
using WebApplicationTest.Models;

namespace WebApplicationTest.DB
{
    public class DBMySQL
    {
        public UserContext UserContext { get; }

        public DBMySQL(UserContext userContext) => UserContext = userContext;

        public string Add(User user)
        {
            UserContext.People.Add(user);
            UserContext.SaveChanges();

            return "Пользователь успешно добавден";
        }

        public bool ListPeople(User user)
        {
            var list = UserContext.People.ToList();

            for (int k = 0; k < list.Count; k++)
            {
                if ((list[k].FirstName == user.FirstName) && (list[k].LastName == user.LastName)) return false;
            }

            return true;
        }
    }
}
