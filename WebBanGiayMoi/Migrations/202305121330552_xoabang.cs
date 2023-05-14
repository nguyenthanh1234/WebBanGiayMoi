namespace WebBanGiayMoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xoabang : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SizeDetails", "GiayId", "dbo.Giays");
            DropForeignKey("dbo.SizeDetails", "SizeId", "dbo.Sizes");
            DropIndex("dbo.SizeDetails", new[] { "SizeId" });
            DropIndex("dbo.SizeDetails", new[] { "GiayId" });
            DropTable("dbo.SizeDetails");
            DropTable("dbo.Sizes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SizeDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrangThai = c.Boolean(nullable: false),
                        SizeId = c.Int(nullable: false),
                        GiayId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.SizeDetails", "GiayId");
            CreateIndex("dbo.SizeDetails", "SizeId");
            AddForeignKey("dbo.SizeDetails", "SizeId", "dbo.Sizes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SizeDetails", "GiayId", "dbo.Giays", "Id", cascadeDelete: true);
        }
    }
}
