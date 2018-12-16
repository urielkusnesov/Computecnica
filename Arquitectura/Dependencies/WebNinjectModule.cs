using System.Data.Entity;
using Model;
using Ninject.Modules;
using Ninject.Web.Common;
using Repositorio;
using Service.Abonados;
using Service.Users;
using Service.Mails;
using Service.Services;

namespace Arquitectura.Dependencies
{
    public class WebNinjectModule : NinjectModule
    {
        public override void Load()
        {
            // Comunicacion Directa
            // Cuando se solicite una implementacion de IConfiguracionProvider o ConfiguracionProvider, ninject devolverá ConfiguracionProvider
            Bind<DbContext>().To<Context>().InRequestScope();
            Bind<IRepositoryService, RepositoryService>().To<RepositoryService>().InRequestScope();
            Bind<IAbonadoService, AbonadoService>().To<AbonadoService>().InRequestScope();
            Bind<IUserService, UserService>().To<UserService>().InRequestScope();
            Bind<IMailService, MailService>().To<MailService>().InRequestScope();
            Bind<IServiceService, ServiceService>().To<ServiceService>().InRequestScope();
        }
    }
}