namespace NGT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BDComplement9 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.blo_bloco", "blo_nome", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.blo_bloco", new[] { "blo_nome" });
        }
    }
}
