using System.Collections.Generic;
using System.Data.Entity;

namespace AlisBatchReporter.Models.EntityFramwork
{
    class AlisDbInitializer : CreateDatabaseIfNotExists<AlisDbContext>
    {
        protected sealed override void Seed(AlisDbContext context)
        {
            context.Seeder();
            base.Seed(context);
        }
    }
}
