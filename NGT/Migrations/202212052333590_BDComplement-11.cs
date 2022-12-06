namespace NGT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BDComplement11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.oco_ocorrencia", "oco_imgocorrencia", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.oco_ocorrencia", "oco_imgocorrencia");
        }
    }
}
