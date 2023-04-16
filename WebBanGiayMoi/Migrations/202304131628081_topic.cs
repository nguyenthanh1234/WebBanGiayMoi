namespace WebBanGiayMoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class topic : DbMigration
    {
        public override void Up()
        {
            Sql("insert into topic(NameTopic) values(N'Cẩm Nang Chơi Giày')");
            Sql("insert into topic(NameTopic) values(N'Life Style')");
            Sql("insert into topic(NameTopic) values(N'Kiến Thức Thời Trang')");
        }

        public override void Down()
        {
        }
    }
}
