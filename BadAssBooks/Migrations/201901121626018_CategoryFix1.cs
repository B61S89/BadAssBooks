namespace BadAssBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryFix1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Category", new[] { "BookId" });
            CreateIndex("dbo.Category", "BookID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Category", new[] { "BookID" });
            CreateIndex("dbo.Category", "BookId");
        }
    }
}
