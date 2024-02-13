namespace BouNanny.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Laptops");
            
            RenameColumn(name: "SexID", table: "Ads", newName: "Sex");
            
            AlterColumn("dbo.Ads", "Sex", c => c.String());
        }
    
        
        public override void Down()
        {
            
        }
    }
}
