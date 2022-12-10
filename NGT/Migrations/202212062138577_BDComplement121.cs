namespace NGT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BDComplement121 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ods_ordservico", "ods_dataentregareal", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ods_ordservico", "ods_dataentregareal", c => c.DateTime(nullable: false));
        }
    }
}
