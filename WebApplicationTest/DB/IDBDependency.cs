using System;
using WebApplicationTest.Models;

namespace WebApplicationTest.DB
{
    public interface IDBDependency
    {
        void Add(User user);

        bool CheckNewUser(User user);
    }
}
