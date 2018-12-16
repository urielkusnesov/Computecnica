using System.Data.Entity;

namespace Model
{
    public class Context: DbContext 
    {
        public Context(): base()
        {
            Database.SetInitializer<Context>(new CreateDatabaseIfNotExists<Context>());
        }
            
        public DbSet<Abonado> Abonados { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
