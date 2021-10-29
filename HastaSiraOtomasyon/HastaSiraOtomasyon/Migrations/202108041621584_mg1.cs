namespace HastaSiraOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hastas", "resmi", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hastas", "resmi");
        }
    }
}
