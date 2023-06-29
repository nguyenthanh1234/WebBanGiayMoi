namespace WebBanGiayMoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class themcotsoluong : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Giays", "SoLuong", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Giays", "SoLuong");
        }
    }
}
