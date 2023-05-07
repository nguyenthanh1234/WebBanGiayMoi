namespace WebBanGiayMoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class themsize2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "Size", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "Size");
        }
    }
}
