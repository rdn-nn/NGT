namespace NGT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BDComplement6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.oco_ocorrencia",
                c => new
                    {
                        oco_codigo = c.Int(nullable: false, identity: true),
                        oco_obs = c.String(nullable: false, maxLength: 250),
                        oco_img = c.String(),
                        oco_datacriacao = c.DateTime(nullable: false),
                        oco_dataatualizacao = c.DateTime(),
                        oco_numticket = c.String(),
                        blo_codigo = c.Int(nullable: false),
                        loc_codigo = c.Int(nullable: false),
                        cat_codigo = c.Int(nullable: false),
                        ite_codigo = c.Int(nullable: false),
                        mot_codigo = c.Int(nullable: false),
                        sti_codigo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.oco_codigo)
                .ForeignKey("dbo.blo_bloco", t => t.blo_codigo)
                .ForeignKey("dbo.cat_categoria", t => t.cat_codigo)
                .ForeignKey("dbo.ite_item", t => t.ite_codigo)
                .ForeignKey("dbo.loc_local", t => t.loc_codigo)
                .ForeignKey("dbo.mot_motivo", t => t.mot_codigo)
                .ForeignKey("dbo.sti_statusticket", t => t.sti_codigo)
                .Index(t => t.blo_codigo)
                .Index(t => t.loc_codigo)
                .Index(t => t.cat_codigo)
                .Index(t => t.ite_codigo)
                .Index(t => t.mot_codigo)
                .Index(t => t.sti_codigo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.oco_ocorrencia", "sti_codigo", "dbo.sti_statusticket");
            DropForeignKey("dbo.oco_ocorrencia", "mot_codigo", "dbo.mot_motivo");
            DropForeignKey("dbo.oco_ocorrencia", "loc_codigo", "dbo.loc_local");
            DropForeignKey("dbo.oco_ocorrencia", "ite_codigo", "dbo.ite_item");
            DropForeignKey("dbo.oco_ocorrencia", "cat_codigo", "dbo.cat_categoria");
            DropForeignKey("dbo.oco_ocorrencia", "blo_codigo", "dbo.blo_bloco");
            DropIndex("dbo.oco_ocorrencia", new[] { "sti_codigo" });
            DropIndex("dbo.oco_ocorrencia", new[] { "mot_codigo" });
            DropIndex("dbo.oco_ocorrencia", new[] { "ite_codigo" });
            DropIndex("dbo.oco_ocorrencia", new[] { "cat_codigo" });
            DropIndex("dbo.oco_ocorrencia", new[] { "loc_codigo" });
            DropIndex("dbo.oco_ocorrencia", new[] { "blo_codigo" });
            DropTable("dbo.oco_ocorrencia");
        }
    }
}
