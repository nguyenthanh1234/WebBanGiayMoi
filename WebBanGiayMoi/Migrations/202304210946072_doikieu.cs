namespace WebBanGiayMoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doikieu : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Giays", "Gia", c => c.Single(nullable: false));
            AlterColumn("dbo.OrderDetails", "UnitPriceSale", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderDetails", "UnitPriceSale", c => c.Int(nullable: false));
            AlterColumn("dbo.Giays", "Gia", c => c.Int(nullable: false));
        }
    }
}
