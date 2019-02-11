namespace BadAssBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CategoryBook", "Category_CategoryID", "dbo.Category");
            DropForeignKey("dbo.CategoryBook", "Book_BookID", "dbo.Book");
            DropIndex("dbo.CategoryBook", new[] { "Category_CategoryID" });
            DropIndex("dbo.CategoryBook", new[] { "Book_BookID" });
            AddColumn("dbo.Category", "BookId", c => c.Int(nullable: false));
            CreateIndex("dbo.Category", "BookId");
            AddForeignKey("dbo.Category", "BookId", "dbo.Book", "BookID", cascadeDelete: true);
            DropTable("dbo.CategoryBook");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CategoryBook",
                c => new
                    {
                        Category_CategoryID = c.Int(nullable: false),
                        Book_BookID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_CategoryID, t.Book_BookID });
            
            DropForeignKey("dbo.Category", "BookId", "dbo.Book");
            DropIndex("dbo.Category", new[] { "BookId" });
            DropColumn("dbo.Category", "BookId");
            CreateIndex("dbo.CategoryBook", "Book_BookID");
            CreateIndex("dbo.CategoryBook", "Category_CategoryID");
            AddForeignKey("dbo.CategoryBook", "Book_BookID", "dbo.Book", "BookID", cascadeDelete: true);
            AddForeignKey("dbo.CategoryBook", "Category_CategoryID", "dbo.Category", "CategoryID", cascadeDelete: true);
        }
    }
}
