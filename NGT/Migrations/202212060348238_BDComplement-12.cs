namespace NGT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BDComplement12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.oco_ocorrencia", "oco_emailcriador", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.oco_ocorrencia", "oco_emailcriador");
        }
    }
}
