namespace NGT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BDComplement4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.loc_local",
                c => new
                    {
                        loc_codigo = c.Int(nullable: false, identity: true),
                        loc_nome = c.String(nullable: false, maxLength: 250),
                        blo_codigo = c.Int(nullable: false),
                        sta_codigo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.loc_codigo)
                .ForeignKey("dbo.blo_bloco", t => t.blo_codigo)
                .ForeignKey("dbo.sta_status", t => t.sta_codigo)
                .Index(t => t.blo_codigo)
                .Index(t => t.sta_codigo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.loc_local", "sta_codigo", "dbo.sta_status");
            DropForeignKey("dbo.loc_local", "blo_codigo", "dbo.blo_bloco");
            DropIndex("dbo.loc_local", new[] { "sta_codigo" });
            DropIndex("dbo.loc_local", new[] { "blo_codigo" });
            DropTable("dbo.loc_local");
        }
    }
}
