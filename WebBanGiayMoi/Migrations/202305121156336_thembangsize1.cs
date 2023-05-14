namespace WebBanGiayMoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thembangsize1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Giays", "Size_id", "dbo.Sizes");
            DropIndex("dbo.Giays", new[] { "Size_id" });
            DropColumn("dbo.Giays", "SizeId");
            DropColumn("dbo.Giays", "Size_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Giays", "Size_id", c => c.Int());
            AddColumn("dbo.Giays", "SizeId", c => c.String(nullable: false));
            CreateIndex("dbo.Giays", "Size_id");
            AddForeignKey("dbo.Giays", "Size_id", "dbo.Sizes", "id");
        }
    }
}
