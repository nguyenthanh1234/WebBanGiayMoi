namespace WebBanGiayMoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class themkhoangoai : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Giays", "SizeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Giays", "SizeId");
            AddForeignKey("dbo.Giays", "SizeId", "dbo.Sizes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Giays", "SizeId", "dbo.Sizes");
            DropIndex("dbo.Giays", new[] { "SizeId" });
            DropColumn("dbo.Giays", "SizeId");
        }
    }
}
