namespace BadAssBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        AuthorID = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstName = c.String(),
                    })
                .PrimaryKey(t => t.AuthorID);
            
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        BookID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ISBN = c.String(),
                        PageCount = c.String(),
                        PublishedDate = c.DateTime(nullable: false),
                        ThumbnailURL = c.String(),
                        ShortDescription = c.String(),
                        LongDescription = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.BookID);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Classification = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        BookID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        CommentText = c.String(),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Book", t => t.BookID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.BookID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstName = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.MyBook",
                c => new
                    {
                        MyBookID = c.Int(nullable: false, identity: true),
                        BookID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        ReadStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MyBookID)
                .ForeignKey("dbo.Book", t => t.BookID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.BookID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Rating",
                c => new
                    {
                        RatingID = c.Int(nullable: false, identity: true),
                        BookID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        GivenRating = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.RatingID)
                .ForeignKey("dbo.Book", t => t.BookID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.BookID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.BookAuthor",
                c => new
                    {
                        Book_BookID = c.Int(nullable: false),
                        Author_AuthorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Book_BookID, t.Author_AuthorID })
                .ForeignKey("dbo.Book", t => t.Book_BookID, cascadeDelete: true)
                .ForeignKey("dbo.Author", t => t.Author_AuthorID, cascadeDelete: true)
                .Index(t => t.Book_BookID)
                .Index(t => t.Author_AuthorID);
            
            CreateTable(
                "dbo.CategoryBook",
                c => new
                    {
                        Category_CategoryID = c.Int(nullable: false),
                        Book_BookID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_CategoryID, t.Book_BookID })
                .ForeignKey("dbo.Category", t => t.Category_CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Book", t => t.Book_BookID, cascadeDelete: true)
                .Index(t => t.Category_CategoryID)
                .Index(t => t.Book_BookID);
            
            CreateTable(
                "dbo.Recommendation",
                c => new
                    {
                        BookID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookID, t.UserID })
                .ForeignKey("dbo.Book", t => t.BookID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.BookID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recommendation", "UserID", "dbo.User");
            DropForeignKey("dbo.Recommendation", "BookID", "dbo.Book");
            DropForeignKey("dbo.Rating", "UserID", "dbo.User");
            DropForeignKey("dbo.Rating", "BookID", "dbo.Book");
            DropForeignKey("dbo.MyBook", "UserID", "dbo.User");
            DropForeignKey("dbo.MyBook", "BookID", "dbo.Book");
            DropForeignKey("dbo.Comment", "UserID", "dbo.User");
            DropForeignKey("dbo.Comment", "BookID", "dbo.Book");
            DropForeignKey("dbo.CategoryBook", "Book_BookID", "dbo.Book");
            DropForeignKey("dbo.CategoryBook", "Category_CategoryID", "dbo.Category");
            DropForeignKey("dbo.BookAuthor", "Author_AuthorID", "dbo.Author");
            DropForeignKey("dbo.BookAuthor", "Book_BookID", "dbo.Book");
            DropIndex("dbo.Recommendation", new[] { "UserID" });
            DropIndex("dbo.Recommendation", new[] { "BookID" });
            DropIndex("dbo.CategoryBook", new[] { "Book_BookID" });
            DropIndex("dbo.CategoryBook", new[] { "Category_CategoryID" });
            DropIndex("dbo.BookAuthor", new[] { "Author_AuthorID" });
            DropIndex("dbo.BookAuthor", new[] { "Book_BookID" });
            DropIndex("dbo.Rating", new[] { "UserID" });
            DropIndex("dbo.Rating", new[] { "BookID" });
            DropIndex("dbo.MyBook", new[] { "UserID" });
            DropIndex("dbo.MyBook", new[] { "BookID" });
            DropIndex("dbo.Comment", new[] { "UserID" });
            DropIndex("dbo.Comment", new[] { "BookID" });
            DropTable("dbo.Recommendation");
            DropTable("dbo.CategoryBook");
            DropTable("dbo.BookAuthor");
            DropTable("dbo.Rating");
            DropTable("dbo.MyBook");
            DropTable("dbo.User");
            DropTable("dbo.Comment");
            DropTable("dbo.Category");
            DropTable("dbo.Book");
            DropTable("dbo.Author");
        }
    }
}
