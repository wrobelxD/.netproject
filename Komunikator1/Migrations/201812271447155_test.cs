namespace Komunikator1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.Replies", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Replies", "CreatedOn", c => c.DateTime());
            CreateIndex("dbo.Replies", "UserId");
            AddForeignKey("dbo.Replies", "UserId", "dbo.Users", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Replies", "UserId", "dbo.Users");
            DropIndex("dbo.Replies", new[] { "UserId" });
            DropColumn("dbo.Replies", "CreatedOn");
            DropColumn("dbo.Replies", "UserId");
            DropColumn("dbo.Comments", "CreatedOn");
        }
    }
}
