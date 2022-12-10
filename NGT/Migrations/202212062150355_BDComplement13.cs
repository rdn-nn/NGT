namespace NGT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BDComplement13 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ods_ordservico", "ods_numticketos", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ods_ordservico", "ods_numticketos", c => c.String(nullable: false));
        }
    }
}
