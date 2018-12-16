using Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Service.Users
{
    public interface IUserService
    {
        User Get(int id);

        IList<User> List(Expression<Func<User, bool>> filter = null);
            
        Result Add(User service);

        Result Update(int id, User service);

        Result Remove(User service);

        Result Remove(int id);

        string Login(User user);

        bool ValidateToken(string username, string token);
    }
}
