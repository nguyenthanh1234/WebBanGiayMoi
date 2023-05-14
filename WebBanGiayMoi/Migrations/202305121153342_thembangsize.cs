namespace WebBanGiayMoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thembangsize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Giays", "SizeId", c => c.String(nullable: false));
            AddColumn("dbo.Giays", "Size_id", c => c.Int());
            CreateIndex("dbo.Giays", "Size_id");
            AddForeignKey("dbo.Giays", "Size_id", "dbo.Sizes", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Giays", "Size_id", "dbo.Sizes");
            DropIndex("dbo.Giays", new[] { "Size_id" });
            DropColumn("dbo.Giays", "Size_id");
            DropColumn("dbo.Giays", "SizeId");
            DropTable("dbo.Sizes");
        }
    }
}
