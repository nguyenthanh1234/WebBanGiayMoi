namespace WebBanGiayMoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doigia1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderDetails", "UnitPriceSale", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderDetails", "UnitPriceSale", c => c.Int(nullable: false));
        }
    }
}
