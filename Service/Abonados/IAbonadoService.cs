using Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Service.Abonados
{
    public interface IAbonadoService
    {
        Abonado Get(int id);

        IList<Abonado> List(Expression<Func<Abonado, bool>> filter = null);
            
        Result Add(Abonado abonado);

        Result Update(int id, Abonado abonado);

        Result Remove(Abonado abonado);

        Result Remove(int id);
    }
}
