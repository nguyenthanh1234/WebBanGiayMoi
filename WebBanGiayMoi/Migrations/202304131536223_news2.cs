namespace WebBanGiayMoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class news2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.News", "Topic_Id", "dbo.Topic");
            DropIndex("dbo.News", new[] { "Topic_Id" });
            RenameColumn(table: "dbo.News", name: "Topic_Id", newName: "TopicID");
            AlterColumn("dbo.News", "TopicID", c => c.Int(nullable: false));
            CreateIndex("dbo.News", "TopicID");
            AddForeignKey("dbo.News", "TopicID", "dbo.Topic", "Id", cascadeDelete: true);
            DropColumn("dbo.News", "ID_topic");
        }
        
        public override void Down()
        {
            AddColumn("dbo.News", "ID_topic", c => c.Int(nullable: false));
            DropForeignKey("dbo.News", "TopicID", "dbo.Topic");
            DropIndex("dbo.News", new[] { "TopicID" });
            AlterColumn("dbo.News", "TopicID", c => c.Int());
            RenameColumn(table: "dbo.News", name: "TopicID", newName: "Topic_Id");
            CreateIndex("dbo.News", "Topic_Id");
            AddForeignKey("dbo.News", "Topic_Id", "dbo.Topic", "Id");
        }
    }
}
