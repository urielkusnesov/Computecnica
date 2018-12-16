using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Repositorio;
using Model;

namespace Service.Users
{
    public class UserService : IUserService
    {
        private readonly IRepositoryService repositorio;

        public UserService(IRepositoryService repositorio)
        {
            this.repositorio = repositorio;
        }

        public User Get(int id)
        {
            return repositorio.Get<User>(id);
        }

        public IList<User> List(Expression<Func<User, bool>> filter = null)
        {
            return repositorio.List<User>(filter);
        }

        public Result Add(User user)
        {
            var result = repositorio.Add<User>(user);
            repositorio.SaveChanges();
            return new Result { Object = result };
        }

        public Result Update(int id, User user)
        {
            var oldUser = repositorio.Get<User>(id);
            if (oldUser != null)
            {
                oldUser = user;
                repositorio.SaveChanges();
                return new Result { Object = user };
            }
            return new Result { Error = "No se encontro el usuario" };
        }

        public Result Remove(User user)
        {
            var result = repositorio.Remove<User>(user);
            if (result != null)
            {
                repositorio.SaveChanges();
                return new Result { Object = result };
            }
            return new Result { Error = "No se pudo eliminar el usuario" };
        }

        public Result Remove(int id)
        {
            var result = repositorio.Remove<User>(id);
            if (result != null)
            {
                repositorio.SaveChanges();
                return new Result { Object = result };
            }
            return new Result { Error = "No se pudo eliminar el usuario" };
        }

        public string Login(User user)
        {
            try
            {
                User currentUser = repositorio.Get<User>(x => x.Username == user.Username);
                if (currentUser != null && currentUser.Password == user.Password)
                {
                    string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                    currentUser.Token = token;
                    repositorio.SaveChanges();
                    return token;
                }
                return "";
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }

        public bool ValidateToken(string username, string token)
        {
            try
            {
                User currentUser = repositorio.Get<User>(x => x.Username == username);
                if (currentUser != null && currentUser.Token == token)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
