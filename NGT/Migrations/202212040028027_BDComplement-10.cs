namespace NGT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BDComplement10 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.loc_local", new[] { "loc_nome" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.loc_local", "loc_nome", unique: true);
        }
    }
}
