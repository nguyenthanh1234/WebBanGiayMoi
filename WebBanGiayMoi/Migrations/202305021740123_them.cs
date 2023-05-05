namespace WebBanGiayMoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class them : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "PhuongThucTT", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "PhuongThucTT");
        }
    }
}
