namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_id_review : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.tb_Review");
            AlterColumn("dbo.tb_Review", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.tb_Review", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.tb_Review");
            AlterColumn("dbo.tb_Review", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.tb_Review", "Id");
        }
    }
}
