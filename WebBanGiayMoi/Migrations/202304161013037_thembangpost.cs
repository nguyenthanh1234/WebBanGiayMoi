namespace WebBanGiayMoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thembangpost : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Image = c.String(),
                        ShortContent = c.String(),
                        Content = c.String(),
                        TopicID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Topic", t => t.TopicID, cascadeDelete: true)
                .Index(t => t.TopicID);
            
            CreateTable(
                "dbo.Topic",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameTopic = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "TopicID", "dbo.Topic");
            DropIndex("dbo.News", new[] { "TopicID" });
            DropTable("dbo.Topic");
            DropTable("dbo.News");
        }
    }
}
