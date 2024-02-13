namespace BouNanny.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update01 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Bikes");
            DropTable("dbo.CellPhones");

            AddColumn("dbo.Reviews", "ClientID", c => c.Int(nullable: false));
            AddForeignKey("dbo.Reviews", "ClientID", "dbo.Clients", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "ClientID", "dbo.Clients");
            DropColumn("dbo.Reviews", "ClientID");
        }
    }
}
