namespace NGT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BDComplement3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.mot_motivo",
                c => new
                    {
                        mot_codigo = c.Int(nullable: false, identity: true),
                        mot_nome = c.String(nullable: false, maxLength: 250),
                        sta_codigo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.mot_codigo)
                .ForeignKey("dbo.sta_status", t => t.sta_codigo)
                .Index(t => t.sta_codigo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.mot_motivo", "sta_codigo", "dbo.sta_status");
            DropIndex("dbo.mot_motivo", new[] { "sta_codigo" });
            DropTable("dbo.mot_motivo");
        }
    }
}
