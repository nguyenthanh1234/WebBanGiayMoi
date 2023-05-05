namespace WebBanGiayMoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class themsize : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Giays", "Size", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Giays", "Size");
        }
    }
}
