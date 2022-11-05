namespace NGT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BDComplement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.blo_bloco",
                c => new
                    {
                        blo_codigo = c.Int(nullable: false, identity: true),
                        blo_nome = c.String(nullable: false, maxLength: 100),
                        sta_codigo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.blo_codigo)
                .ForeignKey("dbo.sta_status", t => t.sta_codigo)
                .Index(t => t.sta_codigo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.blo_bloco", "sta_codigo", "dbo.sta_status");
            DropIndex("dbo.blo_bloco", new[] { "sta_codigo" });
            DropTable("dbo.blo_bloco");
        }
    }
}
