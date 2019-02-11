namespace BadAssBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MyBook", "ReadStatus", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MyBook", "ReadStatus", c => c.Int(nullable: true));
        }
    }
}
