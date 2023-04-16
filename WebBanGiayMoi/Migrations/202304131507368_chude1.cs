namespace WebBanGiayMoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chude1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Topics", newName: "Topic");

        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Topic", newName: "Topics");
        }
    }
}
