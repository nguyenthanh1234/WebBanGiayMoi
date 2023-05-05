namespace WebBanGiayMoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sizegi : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name_Size = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Giays", "Size_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Giays", "Size_ID1", c => c.Int());
            CreateIndex("dbo.Giays", "Size_ID1");
            AddForeignKey("dbo.Giays", "Size_ID1", "dbo.Sizes", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Giays", "Size_ID1", "dbo.Sizes");
            DropIndex("dbo.Giays", new[] { "Size_ID1" });
            DropColumn("dbo.Giays", "Size_ID1");
            DropColumn("dbo.Giays", "Size_ID");
            DropTable("dbo.Sizes");
        }
    }
}
