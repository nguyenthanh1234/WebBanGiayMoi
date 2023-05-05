namespace WebBanGiayMoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xoadl : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Giays", "Size_ID1", "dbo.Sizes");
            DropIndex("dbo.Giays", new[] { "Size_ID1" });
            AddColumn("dbo.Giays", "Size", c => c.String());
            DropColumn("dbo.Giays", "Size_ID");
            DropColumn("dbo.Giays", "Size_ID1");
            DropTable("dbo.Sizes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name_Size = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Giays", "Size_ID1", c => c.Int());
            AddColumn("dbo.Giays", "Size_ID", c => c.Int(nullable: false));
            DropColumn("dbo.Giays", "Size");
            CreateIndex("dbo.Giays", "Size_ID1");
            AddForeignKey("dbo.Giays", "Size_ID1", "dbo.Sizes", "ID");
        }
    }
}
