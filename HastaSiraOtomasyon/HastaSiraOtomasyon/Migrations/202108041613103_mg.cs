namespace HastaSiraOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Doktors",
                c => new
                    {
                        id = c.Int(nullable: false),
                        adi_soyadi = c.String(),
                        unvani = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Odas", t => t.id)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.Odas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        oda_no = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Hastas",
                c => new
                    {
                        tc = c.Long(nullable: false, identity: true),
                        adi_soyadi = c.String(),
                        dogum_tarihi = c.String(),
                    })
                .PrimaryKey(t => t.tc);
            
            CreateTable(
                "dbo.ilaclars",
                c => new
                    {
                        ilac_id = c.Int(nullable: false, identity: true),
                        ilac_adi = c.String(),
                        kullanim_zamani = c.String(),
                        muayene_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ilac_id);
            
            CreateTable(
                "dbo.Muayenes",
                c => new
                    {
                        muayene_id = c.Int(nullable: false, identity: true),
                        saati = c.String(),
                        muayene_durumu = c.Boolean(nullable: false),
                        Doktor_id = c.Int(),
                        Hasta_tc = c.Long(),
                        ilaclars_ilac_id = c.Int(),
                    })
                .PrimaryKey(t => t.muayene_id)
                .ForeignKey("dbo.Doktors", t => t.Doktor_id)
                .ForeignKey("dbo.Hastas", t => t.Hasta_tc)
                .ForeignKey("dbo.ilaclars", t => t.ilaclars_ilac_id)
                .Index(t => t.Doktor_id)
                .Index(t => t.Hasta_tc)
                .Index(t => t.ilaclars_ilac_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Muayenes", "ilaclars_ilac_id", "dbo.ilaclars");
            DropForeignKey("dbo.Muayenes", "Hasta_tc", "dbo.Hastas");
            DropForeignKey("dbo.Muayenes", "Doktor_id", "dbo.Doktors");
            DropForeignKey("dbo.Doktors", "id", "dbo.Odas");
            DropIndex("dbo.Muayenes", new[] { "ilaclars_ilac_id" });
            DropIndex("dbo.Muayenes", new[] { "Hasta_tc" });
            DropIndex("dbo.Muayenes", new[] { "Doktor_id" });
            DropIndex("dbo.Doktors", new[] { "id" });
            DropTable("dbo.Muayenes");
            DropTable("dbo.ilaclars");
            DropTable("dbo.Hastas");
            DropTable("dbo.Odas");
            DropTable("dbo.Doktors");
        }
    }
}
