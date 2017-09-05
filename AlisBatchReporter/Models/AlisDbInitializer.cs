﻿using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace AlisBatchReporter.Models
{
    class AlisDbInitializer : CreateDatabaseIfNotExists<AlisDbContext>
    {
        public AlisDbInitializer(AlisDbContext alisDbContext)
        {
            if (!alisDbContext.Database.Exists())
            {
                Seed(alisDbContext);
            }          
        }

        protected sealed override void Seed(AlisDbContext context)
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
