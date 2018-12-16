using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Repositorio;
using Model;

namespace Service.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IRepositoryService repositorio;

        public ServiceService(IRepositoryService repositorio)
        {
            this.repositorio = repositorio;
        }

        public Model.Service Get(int id)
        {
            return repositorio.Get<Model.Service>(id);
        }

        public IList<Model.Service> List(Expression<Func<Model.Service, bool>> filter = null)
        {
            return repositorio.List<Model.Service>(filter);
        }

        public Result Add(Model.Service service)
        {
            var result = repositorio.Add<Model.Service>(service);
            repositorio.SaveChanges();
            return new Result { Object = result };
        }

        public Result Update(int id, Model.Service service)
        {
            var oldService = repositorio.Get<Model.Service>(id);
            if (oldService != null)
            {
                oldService = service;
                repositorio.SaveChanges();
                return new Result { Object = service };
            }
            return new Result { Error = "No se encontro el servicio" };
        }

        public Result Remove(Model.Service service)
        {
            var result = repositorio.Remove<Model.Service>(service);
            if (result != null)
            {
                repositorio.SaveChanges();
                return new Result { Object = result };
            }
            return new Result { Error = "No se pudo eliminar el servicio" };
        }

        public Result Remove(int id)
        {
            var result = repositorio.Remove<Model.Service>(id);
            if (result != null)
            {
                repositorio.SaveChanges();
                return new Result { Object = result };
            }
            return new Result { Error = "No se pudo eliminar el servicio" };
        }
    }
}
