﻿namespace WebBanGiayMoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xoasoluong : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Giays", "SoLuong");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Giays", "SoLuong", c => c.Int());
        }
    }
}
