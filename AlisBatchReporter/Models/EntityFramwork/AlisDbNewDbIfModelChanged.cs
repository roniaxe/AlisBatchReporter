using System.Collections.Generic;
using System.Data.Entity;

namespace AlisBatchReporter.Models.EntityFramwork
{
    class AlisDbNewDbIfModelChanged : DropCreateDatabaseIfModelChanges<AlisDbContext>
    {
        protected override void Seed(AlisDbContext context)
        {
            context.Seeder();
            base.Seed(context);
        }
    }
}
