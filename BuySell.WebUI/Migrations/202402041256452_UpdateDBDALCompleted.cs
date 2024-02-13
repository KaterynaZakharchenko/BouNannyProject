namespace BouNanny.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdateDBDALCompleted : DbMigration
    {
        public override void Up()
        {
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
                .ForeignKey("dbo.Cities", t => t.CityID, cascadeDelete: true)
                .ForeignKey("dbo.Levels", t => t.LevelID, cascadeDelete: true)
                .ForeignKey("dbo.Sexes", t => t.Sex, cascadeDelete: true)
                .ForeignKey("dbo.TimePeriods", t => t.TimePeriodID, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.CountryID, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.ClientID, cascadeDelete: true)
                .ForeignKey("dbo.States", t => t.StateID, cascadeDelete: true)
                .Index(t => t.TimePeriodID)
                .Index(t => t.Sex)
                .Index(t => t.CountryID)
                .Index(t => t.StateID)
                .Index(t => t.CityID)
                .Index(t => t.ClientID)
                .Index(t => t.LevelID);

            CreateTable(
                "dbo.Sexes",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Cities",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                    StateID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.States", t => t.StateID, cascadeDelete: false)
                .Index(t => t.StateID);

            CreateTable(
                "dbo.States",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                    CountryID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Countries", t => t.CountryID, cascadeDelete: false)
                .Index(t => t.CountryID);

            CreateTable(
                "dbo.Countries",
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
                "dbo.Images",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Path = c.String(nullable: false, maxLength: 255),
                    AdID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Ads", t => t.AdID, cascadeDelete: true)
                .Index(t => t.AdID);

            CreateTable(
                "dbo.Reviews",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Content = c.String(nullable: false),
                    PostingTime = c.DateTime(nullable: false),
                    ReviewStars = c.Int(nullable: false),
                    AdID = c.Int(nullable: false),
                    ClientID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Ads", t => t.AdID, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.ClientID, cascadeDelete: false)
                .Index(t => t.AdID)
                .Index(t => t.ClientID);

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
                .ForeignKey("dbo.Cities", t => t.CityID, cascadeDelete: false)
                .ForeignKey("dbo.Countries", t => t.CountryID, cascadeDelete: false)
                .ForeignKey("dbo.States", t => t.StateID, cascadeDelete: false)
                .Index(t => t.CountryID)
                .Index(t => t.StateID)
                .Index(t => t.CityID);

            CreateTable(
                "dbo.Years",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    YearNo = c.String(nullable: false),
                })
                .PrimaryKey(t => t.ID);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Ads", "LevelID", "dbo.Levels");
            DropForeignKey("dbo.Ads", "StateID", "dbo.States");
            DropForeignKey("dbo.Ads", "Sex", "dbo.Sexes");
            DropForeignKey("dbo.Clients", "StateID", "dbo.States");
            DropForeignKey("dbo.Reviews", "ClientID", "dbo.Clients");
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
            DropIndex("dbo.States", new[] { "CountryID" });
            DropIndex("dbo.Cities", new[] { "StateID" });
            DropIndex("dbo.Ads", new[] { "ClientID" });
            DropIndex("dbo.Ads", new[] { "CityID" });
            DropIndex("dbo.Ads", new[] { "StateID" });
            DropIndex("dbo.Ads", new[] { "CountryID" });
            DropIndex("dbo.Ads", new[] { "TimePeriodID" });
            DropTable("dbo.Ads");
            DropTable("dbo.Clients");
            DropTable("dbo.TimePeriods");
            DropTable("dbo.Countries");
            DropTable("dbo.States");
            DropTable("dbo.Cities");
            DropTable("dbo.Sexes");
            DropTable("dbo.Levels");
        }
    }
}
