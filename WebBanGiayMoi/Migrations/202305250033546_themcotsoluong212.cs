namespace WebBanGiayMoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class themcotsoluong212 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Giays", "SoLuong", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Giays", "SoLuong", c => c.String());
        }
    }
}
