using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace AlisBatchReporter.Models.EntityFramwork
{
    class AlisDbContext : DbContext
    {
        public class MyContextFactory : IDbContextFactory<AlisDbContext>
        {
            public AlisDbContext Create()
            {
                return new AlisDbContext("CompactDBContext");
            }
        }

        public AlisDbContext() : base("CompactDBContext")
        {
            //Database.SetInitializer(new AlisDbInitializer(this));
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AlisDbContext>());
        }

        public AlisDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Database.SetInitializer(new AlisDbInitializer(this));
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AlisDbContext>());
        }

        public DbSet<SavedCredentials> SavedCredentialses { get; set; }
        public DbSet<Distribution> Distributions { get; set; }
    }
}
