namespace NGT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BDComplement5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ite_item",
                c => new
                    {
                        ite_codigo = c.Int(nullable: false, identity: true),
                        ite_nome = c.String(nullable: false, maxLength: 250),
                        ite_numserie = c.String(maxLength: 25),
                        ite_patrimonio = c.Int(nullable: false),
                        ite_hasplaca = c.Boolean(nullable: false),
                        ite_ispatinterno = c.Boolean(nullable: false),
                        loc_codigo = c.Int(nullable: false),
                        cat_codigo = c.Int(nullable: false),
                        sta_codigo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ite_codigo)
                .ForeignKey("dbo.cat_categoria", t => t.cat_codigo)
                .ForeignKey("dbo.sta_status", t => t.sta_codigo)
                .ForeignKey("dbo.loc_local", t => t.loc_codigo)
                .Index(t => t.loc_codigo)
                .Index(t => t.cat_codigo)
                .Index(t => t.sta_codigo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ite_item", "loc_codigo", "dbo.loc_local");
            DropForeignKey("dbo.ite_item", "sta_codigo", "dbo.sta_status");
            DropForeignKey("dbo.ite_item", "cat_codigo", "dbo.cat_categoria");
            DropIndex("dbo.ite_item", new[] { "sta_codigo" });
            DropIndex("dbo.ite_item", new[] { "cat_codigo" });
            DropIndex("dbo.ite_item", new[] { "loc_codigo" });
            DropTable("dbo.ite_item");
        }
    }
}
