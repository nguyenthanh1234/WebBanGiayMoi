namespace WebBanGiayMoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xoanull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sizes", "name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sizes", "name", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
