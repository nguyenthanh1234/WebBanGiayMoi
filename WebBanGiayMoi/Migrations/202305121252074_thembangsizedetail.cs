namespace WebBanGiayMoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thembangsizedetail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Giays", "SizeId", "dbo.Sizes");
            DropIndex("dbo.Giays", new[] { "SizeId" });
            CreateTable(
                "dbo.SizeDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrangThai = c.Boolean(nullable: false),
                        SizeId = c.Int(nullable: false),
                        GiayId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Giays", t => t.GiayId, cascadeDelete: true)
                .ForeignKey("dbo.Sizes", t => t.SizeId, cascadeDelete: true)
                .Index(t => t.SizeId)
                .Index(t => t.GiayId);
            
            DropColumn("dbo.Giays", "SizeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Giays", "SizeId", c => c.Int(nullable: false));
            DropForeignKey("dbo.SizeDetails", "SizeId", "dbo.Sizes");
            DropForeignKey("dbo.SizeDetails", "GiayId", "dbo.Giays");
            DropIndex("dbo.SizeDetails", new[] { "GiayId" });
            DropIndex("dbo.SizeDetails", new[] { "SizeId" });
            DropTable("dbo.SizeDetails");
            CreateIndex("dbo.Giays", "SizeId");
            AddForeignKey("dbo.Giays", "SizeId", "dbo.Sizes", "Id", cascadeDelete: true);
        }
    }
}
