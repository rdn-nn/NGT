namespace NGT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BDCOmplement8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.oso_ordservicoocorrencia",
                c => new
                    {
                        oso_codigo = c.Int(nullable: false, identity: true),
                        ods_codigo = c.Int(nullable: false),
                        oco_codigo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.oso_codigo)
                .ForeignKey("dbo.oco_ocorrencia", t => t.oco_codigo)
                .ForeignKey("dbo.ods_ordservico", t => t.ods_codigo)
                .Index(t => t.ods_codigo)
                .Index(t => t.oco_codigo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.oso_ordservicoocorrencia", "ods_codigo", "dbo.ods_ordservico");
            DropForeignKey("dbo.oso_ordservicoocorrencia", "oco_codigo", "dbo.oco_ocorrencia");
            DropIndex("dbo.oso_ordservicoocorrencia", new[] { "oco_codigo" });
            DropIndex("dbo.oso_ordservicoocorrencia", new[] { "ods_codigo" });
            DropTable("dbo.oso_ordservicoocorrencia");
        }
    }
}
