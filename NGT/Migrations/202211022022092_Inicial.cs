namespace NGT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.per_perfil",
                c => new
                    {
                        per_codigo = c.Int(nullable: false, identity: true),
                        per_nome = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.per_codigo);
            
            CreateTable(
                "dbo.usu_usuario",
                c => new
                    {
                        usu_codigo = c.Int(nullable: false, identity: true),
                        usu_nome = c.String(nullable: false, maxLength: 200),
                        usu_email = c.String(nullable: false, maxLength: 100),
                        usu_senha = c.String(nullable: false),
                        usu_hash = c.String(),
                        usu_imgperfil = c.String(),
                        per_codigo = c.Int(nullable: false),
                        sta_codigo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.usu_codigo)
                .ForeignKey("dbo.per_perfil", t => t.per_codigo)
                .ForeignKey("dbo.sta_status", t => t.sta_codigo)
                .Index(t => t.per_codigo)
                .Index(t => t.sta_codigo);
            
            CreateTable(
                "dbo.sta_status",
                c => new
                    {
                        sta_codigo = c.Int(nullable: false, identity: true),
                        sta_nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.sta_codigo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.usu_usuario", "sta_codigo", "dbo.sta_status");
            DropForeignKey("dbo.usu_usuario", "per_codigo", "dbo.per_perfil");
            DropIndex("dbo.usu_usuario", new[] { "sta_codigo" });
            DropIndex("dbo.usu_usuario", new[] { "per_codigo" });
            DropTable("dbo.sta_status");
            DropTable("dbo.usu_usuario");
            DropTable("dbo.per_perfil");
        }
    }
}
