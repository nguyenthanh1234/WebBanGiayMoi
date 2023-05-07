namespace WebBanGiayMoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class themsize1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Giays", "Size");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Giays", "Size", c => c.String());
        }
    }
}
