using Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Service.Services
{
    public interface IServiceService
    {
        Model.Service Get(int id);

        IList<Model.Service> List(Expression<Func<Model.Service, bool>> filter = null);
            
        Result Add(Model.Service service);

        Result Update(int id, Model.Service service);

        Result Remove(Model.Service service);

        Result Remove(int id);
    }
}
