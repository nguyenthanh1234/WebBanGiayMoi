namespace WebBanGiayMoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class themphone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "PhoneKH", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "PhoneKH");
        }
    }
}
