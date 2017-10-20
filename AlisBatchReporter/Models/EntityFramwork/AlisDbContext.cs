using System.Collections.Generic;
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

        public AlisDbContext() : this("CompactDBContext")
        {
        }

        public AlisDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Database.SetInitializer(new AlisDbInitializer());
            Database.SetInitializer(new AlisDbNewDbIfModelChanged());
        }

        public void Seeder()
        {
            List<SavedCredentials> initDbList = new List<SavedCredentials>()
            {
                new SavedCredentials(){ Name = "Prod", Host = "10.134.5.30" },
                new SavedCredentials(){Name = "Uat", Host = "10.134.8.10"},
                new SavedCredentials(){Name = "White/Red/Blue", Host = "876630-sqldev.fblife.com"},
                new SavedCredentials(){Name = "Sapiens", Host = "alis-db-sql3"}
            };
            SavedCredentialses.AddRange(initDbList);
            SaveChanges();
        }

        public DbSet<SavedCredentials> SavedCredentialses { get; set; }
        public DbSet<Distribution> Distributions { get; set; }
    }
}
