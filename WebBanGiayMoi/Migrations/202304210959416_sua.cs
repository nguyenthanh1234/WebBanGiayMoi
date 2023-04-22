namespace WebBanGiayMoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sua : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Giays", "Gia", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Giays", "Gia", c => c.Single(nullable: false));
        }
    }
}
