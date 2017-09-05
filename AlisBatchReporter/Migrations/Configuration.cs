using System.Collections.Generic;
using AlisBatchReporter.Models;

namespace AlisBatchReporter.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.AlisDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Models.AlisDbContext context)
        {
            List<SavedCredentials> initDbList = new List<SavedCredentials>()
            {
                new SavedCredentials(){ Name = "Prod", Host = "10.134.5.30" },
                new SavedCredentials(){Name = "Uat", Host = "10.134.8.10"},
                new SavedCredentials(){Name = "White/Red/Blue", Host = "876630-sqldev.fblife.com"},
                new SavedCredentials(){Name = "Sapiens", Host = "alis-db-sql3"}
            };
            context.SavedCredentialses.AddRange(initDbList);
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
