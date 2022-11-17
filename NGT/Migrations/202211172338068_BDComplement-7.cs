namespace NGT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BDComplement7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ods_ordservico",
                c => new
                    {
                        ods_codigo = c.Int(nullable: false, identity: true),
                        oco_codigo = c.Int(nullable: false),
                        ods_patrimonio = c.Int(nullable: false),
                        blo_codigo = c.Int(nullable: false),
                        loc_codigo = c.Int(nullable: false),
                        cat_codigo = c.Int(nullable: false),
                        ite_codigo = c.Int(nullable: false),
                        mot_codigo = c.Int(nullable: false),
                        ods_obs = c.String(maxLength: 250),
                        for_codigo = c.Int(nullable: false),
                        ods_notafiscal = c.String(maxLength: 11),
                        ods_centrocusto = c.String(maxLength: 20),
                        ods_valor = c.Double(nullable: false),
                        ods_desconto = c.Double(nullable: false),
                        ods_dataentregaprevista = c.DateTime(nullable: false),
                        ods_dataentregareal = c.DateTime(nullable: false),
                        ods_datacriacao = c.DateTime(nullable: false),
                        ods_dataatualizacao = c.DateTime(),
                        ods_numticketos = c.String(nullable: false),
                        sti_codigo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ods_codigo)
                .ForeignKey("dbo.blo_bloco", t => t.blo_codigo)
                .ForeignKey("dbo.cat_categoria", t => t.cat_codigo)
                .ForeignKey("dbo.for_fornecedor", t => t.for_codigo)
                .ForeignKey("dbo.ite_item", t => t.ite_codigo)
                .ForeignKey("dbo.loc_local", t => t.loc_codigo)
                .ForeignKey("dbo.mot_motivo", t => t.mot_codigo)
                .ForeignKey("dbo.oco_ocorrencia", t => t.oco_codigo)
                .ForeignKey("dbo.sti_statusticket", t => t.sti_codigo)
                .Index(t => t.oco_codigo)
                .Index(t => t.blo_codigo)
                .Index(t => t.loc_codigo)
                .Index(t => t.cat_codigo)
                .Index(t => t.ite_codigo)
                .Index(t => t.mot_codigo)
                .Index(t => t.for_codigo)
                .Index(t => t.sti_codigo);
            
            CreateTable(
                "dbo.for_fornecedor",
                c => new
                    {
                        sta_codigo = c.Int(nullable: false, identity: true),
                        for_razaosoc = c.String(nullable: false, maxLength: 250),
                        for_nomefantasia = c.String(),
                        for_cnpj = c.Int(nullable: false),
                        for_ie = c.Int(nullable: false),
                        for_endereco = c.String(nullable: false, maxLength: 250),
                        for_telefone = c.String(maxLength: 250),
                        for_email = c.String(nullable: false, maxLength: 100),
                        for_servprestado = c.String(maxLength: 100),
                        for_responsavel = c.String(nullable: false, maxLength: 150),
                        for_cargoresp = c.String(maxLength: 100),
                        for_ramoativ = c.String(maxLength: 100),
                        for_obs = c.String(maxLength: 250),
                        StatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.sta_codigo)
                .ForeignKey("dbo.sta_status", t => t.StatusId)
                .Index(t => t.StatusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ods_ordservico", "sti_codigo", "dbo.sti_statusticket");
            DropForeignKey("dbo.ods_ordservico", "oco_codigo", "dbo.oco_ocorrencia");
            DropForeignKey("dbo.ods_ordservico", "mot_codigo", "dbo.mot_motivo");
            DropForeignKey("dbo.ods_ordservico", "loc_codigo", "dbo.loc_local");
            DropForeignKey("dbo.ods_ordservico", "ite_codigo", "dbo.ite_item");
            DropForeignKey("dbo.for_fornecedor", "StatusId", "dbo.sta_status");
            DropForeignKey("dbo.ods_ordservico", "for_codigo", "dbo.for_fornecedor");
            DropForeignKey("dbo.ods_ordservico", "cat_codigo", "dbo.cat_categoria");
            DropForeignKey("dbo.ods_ordservico", "blo_codigo", "dbo.blo_bloco");
            DropIndex("dbo.for_fornecedor", new[] { "StatusId" });
            DropIndex("dbo.ods_ordservico", new[] { "sti_codigo" });
            DropIndex("dbo.ods_ordservico", new[] { "for_codigo" });
            DropIndex("dbo.ods_ordservico", new[] { "mot_codigo" });
            DropIndex("dbo.ods_ordservico", new[] { "ite_codigo" });
            DropIndex("dbo.ods_ordservico", new[] { "cat_codigo" });
            DropIndex("dbo.ods_ordservico", new[] { "loc_codigo" });
            DropIndex("dbo.ods_ordservico", new[] { "blo_codigo" });
            DropIndex("dbo.ods_ordservico", new[] { "oco_codigo" });
            DropTable("dbo.for_fornecedor");
            DropTable("dbo.ods_ordservico");
        }
    }
}
