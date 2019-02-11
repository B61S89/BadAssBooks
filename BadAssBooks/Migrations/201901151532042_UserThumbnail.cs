namespace BadAssBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserThumbnail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "ImageThumbnail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "ImageThumbnail");
        }
    }
}
