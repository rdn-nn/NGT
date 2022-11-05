namespace NGT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BDComplement2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.cat_categoria",
                c => new
                    {
                        cat_codigo = c.Int(nullable: false, identity: true),
                        cat_nome = c.String(nullable: false, maxLength: 150),
                        sta_codigo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.cat_codigo)
                .ForeignKey("dbo.sta_status", t => t.sta_codigo)
                .Index(t => t.sta_codigo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.cat_categoria", "sta_codigo", "dbo.sta_status");
            DropIndex("dbo.cat_categoria", new[] { "sta_codigo" });
            DropTable("dbo.cat_categoria");
        }
    }
}
