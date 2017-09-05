namespace AlisBatchReporter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.saved_credentials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 4000),
                        Password = c.String(maxLength: 4000),
                        Host = c.String(maxLength: 4000),
                        Db = c.String(maxLength: 4000),
                        Name = c.String(maxLength: 4000),
                        ConnString = c.String(maxLength: 4000),
                        ChoseLast = c.Boolean(nullable: false),
                        Saved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.saved_credentials");
        }
    }
}
