namespace BouNanny.WebUI.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdateDBWebUICompleted : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.AccessoryBrands");
            DropTable("dbo.Bikes");
            DropTable("dbo.CellPhones");
            DropTable("dbo.Colors");
            DropTable("dbo.TimePeriods");
            DropTable("dbo.Currencies");
            DropTable("dbo.Clients");
            DropTable("dbo.VehicleBrands");
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.Laptops");
            DropTable("dbo.Ads");

            CreateTable(
                "dbo.Levels",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Ads",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false),
                    Age = c.Int(nullable: false),
                    Sex = c.Int(nullable: false),
                    TimePeriodID = c.Int(nullable: false),
                    LevelID = c.Int(nullable: false),
                    Description = c.String(maxLength: 500),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    CountryID = c.Int(nullable: false),
                    StateID = c.Int(nullable: false),
                    CityID = c.Int(nullable: false),
                    ClientID = c.Int(nullable: false),
                    Slug = c.String(nullable: false),
                    PostingTime = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Levels", t => t.LevelID, cascadeDelete: true)
                .ForeignKey("dbo.Sexes", t => t.Sex, cascadeDelete: true)
                .ForeignKey("dbo.TimePeriods", t => t.TimePeriodID, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.ClientID, cascadeDelete: true)
                .Index(t => t.TimePeriodID)
                .Index(t => t.Sex)
                .Index(t => t.CountryID)
                .Index(t => t.StateID)
                .Index(t => t.CityID)
                .Index(t => t.ClientID)
                .Index(t => t.LevelID);
            AddForeignKey("dbo.Ads", "CityId", "dbo.Cities", "Id");
            AddForeignKey("dbo.Ads", "StateId", "dbo.States", "Id");
            AddForeignKey("dbo.Ads", "CountryId", "dbo.Countries", "Id");

            CreateTable(
                "dbo.Sexes",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.TimePeriods",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    TimePeriod = c.String(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Clients",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Email = c.String(nullable: false),
                    Username = c.String(nullable: false),
                    Name = c.String(nullable: false),
                    CountryID = c.Int(nullable: false),
                    StateID = c.Int(nullable: false),
                    CityID = c.Int(nullable: false),
                    MobileNumber = c.String(nullable: false),
                    JoinDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .Index(t => t.CountryID)
                .Index(t => t.StateID)
                .Index(t => t.CityID);
            AddForeignKey("dbo.Clients", "CityId", "dbo.Cities", "Id");
            AddForeignKey("dbo.Clients", "StateId", "dbo.States", "Id");
            AddForeignKey("dbo.Clients", "CountryId", "dbo.Countries", "Id");

            //AddColumn("dbo.Reviews", "ClientID", c => c.Int(nullable: false));
            //AddForeignKey("dbo.Reviews", "ClientId", "dbo.Clients", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Ads", "LevelID", "dbo.Levels");
            DropForeignKey("dbo.Ads", "StateID", "dbo.States");
            DropForeignKey("dbo.Ads", "Sex", "dbo.Sexes");
            DropForeignKey("dbo.Clients", "StateID", "dbo.States");
            DropForeignKey("dbo.Clients", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.Clients", "CityID", "dbo.Cities");
            DropForeignKey("dbo.Ads", "ClientID", "dbo.Clients");
            DropForeignKey("dbo.Ads", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.Ads", "TimePeriodID", "dbo.TimePeriods");
            DropForeignKey("dbo.Ads", "CityID", "dbo.Cities");
            DropForeignKey("dbo.Cities", "StateID", "dbo.States");
            DropForeignKey("dbo.States", "CountryID", "dbo.Countries");
            DropIndex("dbo.Ads", new[] { "Sex" });
            DropIndex("dbo.Ads", new[] { "LevelID" });
            DropIndex("dbo.Clients", new[] { "CityID" });
            DropIndex("dbo.Clients", new[] { "StateID" });
            DropIndex("dbo.Clients", new[] { "CountryID" });
            DropIndex("dbo.Images", new[] { "AdID" });
            DropIndex("dbo.States", new[] { "CountryID" });
            DropIndex("dbo.Cities", new[] { "StateID" });
            DropIndex("dbo.Ads", new[] { "ClientID" });
            DropIndex("dbo.Ads", new[] { "CityID" });
            DropIndex("dbo.Ads", new[] { "StateID" });
            DropIndex("dbo.Ads", new[] { "CountryID" });
            DropIndex("dbo.Ads", new[] { "TimePeriodID" });
            DropTable("dbo.Ads");
            DropTable("dbo.Levels");
            DropTable("dbo.Clients");
            DropTable("dbo.TimePeriods");
            DropTable("dbo.Countries");
            DropTable("dbo.States");
            DropTable("dbo.Cities");
            DropTable("dbo.Sexes");
        }
    }
}