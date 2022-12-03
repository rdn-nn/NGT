namespace NGT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BDComplement10 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.loc_local", "loc_nome", unique: true);
            CreateIndex("dbo.ite_item", "ite_nome", unique: true);
            CreateIndex("dbo.cat_categoria", "cat_nome", unique: true);
            CreateIndex("dbo.mot_motivo", "mot_nome", unique: true);
            CreateIndex("dbo.for_fornecedor", "for_cnpj", unique: true);
            CreateIndex("dbo.usu_usuario", "usu_email", unique: true);
            CreateIndex("dbo.per_perfil", "per_nome", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.per_perfil", new[] { "per_nome" });
            DropIndex("dbo.usu_usuario", new[] { "usu_email" });
            DropIndex("dbo.for_fornecedor", new[] { "for_cnpj" });
            DropIndex("dbo.mot_motivo", new[] { "mot_nome" });
            DropIndex("dbo.cat_categoria", new[] { "cat_nome" });
            DropIndex("dbo.ite_item", new[] { "ite_nome" });
            DropIndex("dbo.loc_local", new[] { "loc_nome" });
        }
    }
}
