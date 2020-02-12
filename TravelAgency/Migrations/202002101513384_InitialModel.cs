namespace TravelAgency.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArrangementPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HotelId = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                        price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotels", t => t.HotelId, cascadeDelete: true)
                .Index(t => t.HotelId);
            
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DistantDestinationId = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        URLAddress = c.String(),
                        MainImage = c.String(nullable: false),
                        image1 = c.String(),
                        image6 = c.String(),
                        image2 = c.String(),
                        image3 = c.String(),
                        image4 = c.String(),
                        image5 = c.String(),
                        stars = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DistantDestinations", t => t.DistantDestinationId, cascadeDelete: true)
                .Index(t => t.DistantDestinationId);
            
            CreateTable(
                "dbo.DistantDestinations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        MainImage = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArrangementPriceSpecials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                        price = c.Int(nullable: false),
                        HotelSpecialOfferId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HotelSpecialOffers", t => t.HotelSpecialOfferId, cascadeDelete: true)
                .Index(t => t.HotelSpecialOfferId);
            
            CreateTable(
                "dbo.HotelSpecialOffers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        SpecialOfferId = c.Int(nullable: false),
                        stars = c.Int(nullable: false),
                        mainImage = c.String(nullable: false),
                        image1 = c.String(),
                        image2 = c.String(),
                        image3 = c.String(),
                        image4 = c.String(),
                        image5 = c.String(),
                        image6 = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SpecialOffers", t => t.SpecialOfferId, cascadeDelete: true)
                .Index(t => t.SpecialOfferId);
            
            CreateTable(
                "dbo.SpecialOffers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        MainImage = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CruiseArrangements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                        CruiseId = c.Int(nullable: false),
                        InsideCabin = c.Int(nullable: false),
                        BalconyCabin = c.Int(nullable: false),
                        OceanViewCabin = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cruises", t => t.CruiseId, cascadeDelete: true)
                .Index(t => t.CruiseId);
            
            CreateTable(
                "dbo.Cruises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        image1 = c.String(),
                        image2 = c.String(),
                        image3 = c.String(),
                        image4 = c.String(),
                        image5 = c.String(),
                        image6 = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CruisePlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.Int(nullable: false),
                        Port = c.String(),
                        Departure = c.Time(nullable: false, precision: 7),
                        Arrival = c.Time(nullable: false, precision: 7),
                        CruiseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cruises", t => t.CruiseId, cascadeDelete: true)
                .Index(t => t.CruiseId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CruisePlans", "CruiseId", "dbo.Cruises");
            DropForeignKey("dbo.CruiseArrangements", "CruiseId", "dbo.Cruises");
            DropForeignKey("dbo.HotelSpecialOffers", "SpecialOfferId", "dbo.SpecialOffers");
            DropForeignKey("dbo.ArrangementPriceSpecials", "HotelSpecialOfferId", "dbo.HotelSpecialOffers");
            DropForeignKey("dbo.Hotels", "DistantDestinationId", "dbo.DistantDestinations");
            DropForeignKey("dbo.ArrangementPrices", "HotelId", "dbo.Hotels");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.CruisePlans", new[] { "CruiseId" });
            DropIndex("dbo.CruiseArrangements", new[] { "CruiseId" });
            DropIndex("dbo.HotelSpecialOffers", new[] { "SpecialOfferId" });
            DropIndex("dbo.ArrangementPriceSpecials", new[] { "HotelSpecialOfferId" });
            DropIndex("dbo.Hotels", new[] { "DistantDestinationId" });
            DropIndex("dbo.ArrangementPrices", new[] { "HotelId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.CruisePlans");
            DropTable("dbo.Cruises");
            DropTable("dbo.CruiseArrangements");
            DropTable("dbo.SpecialOffers");
            DropTable("dbo.HotelSpecialOffers");
            DropTable("dbo.ArrangementPriceSpecials");
            DropTable("dbo.DistantDestinations");
            DropTable("dbo.Hotels");
            DropTable("dbo.ArrangementPrices");
        }
    }
}
