using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Repositorio;
using Model;

namespace Service.Abonados
{
    public class AbonadoService : IAbonadoService
    {
        private readonly IRepositoryService repositorio;

        public AbonadoService(IRepositoryService repositorio)
        {
            this.repositorio = repositorio;
        }

        public Abonado Get(int id)
        {
            return repositorio.Get<Abonado>(id);
        }

        public IList<Abonado> List(Expression<Func<Abonado, bool>> filter = null)
        {
            return repositorio.List<Abonado>(filter);
        }

        public Result Add(Abonado abonado)
        {
            var result = repositorio.Add<Abonado>(abonado);
            repositorio.SaveChanges();
            return new Result { Object = result };
        }

        public Result Update(int id, Abonado abonado)
        {
            var oldAbonado = repositorio.Get<Abonado>(id);
            if (oldAbonado != null)
            {
                oldAbonado = abonado;
                repositorio.SaveChanges();
                return new Result { Object = abonado };
            }
            return new Result { Error = "No se encontro el abonado" };
        }

        public Result Remove(Abonado abonado)
        {
            var result = repositorio.Remove<Abonado>(abonado);
            if (result != null)
            {
                repositorio.SaveChanges();
                return new Result { Object = result };
            }
            return new Result { Error = "No se pudo eliminar el abonado" };
        }

        public Result Remove(int id)
        {
            var result = repositorio.Remove<Abonado>(id);
            if (result != null)
            {
                repositorio.SaveChanges();
                return new Result { Object = result };
            }
            return new Result { Error = "No se pudo eliminar el abonado" };
        }
    }
}
