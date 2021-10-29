namespace HastaSiraOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Muayenes", "Hasta_tc", "dbo.Hastas");
            DropPrimaryKey("dbo.Hastas");
            AlterColumn("dbo.Hastas", "tc", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.Hastas", "tc");
            AddForeignKey("dbo.Muayenes", "Hasta_tc", "dbo.Hastas", "tc");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Muayenes", "Hasta_tc", "dbo.Hastas");
            DropPrimaryKey("dbo.Hastas");
            AlterColumn("dbo.Hastas", "tc", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.Hastas", "tc");
            AddForeignKey("dbo.Muayenes", "Hasta_tc", "dbo.Hastas", "tc");
        }
    }
}
