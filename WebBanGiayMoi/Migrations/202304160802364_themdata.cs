namespace WebBanGiayMoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class themdata : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "ShortContent", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "ShortContent");
        }
    }
}
