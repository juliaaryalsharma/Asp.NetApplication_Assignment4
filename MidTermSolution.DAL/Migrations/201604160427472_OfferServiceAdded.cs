namespace MidTermSolution.Contracts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class OfferServiceAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookingOffers",
                c => new
                    {
                        BookingOfferID = c.Int(nullable: false, identity: true),
                        OfferID = c.Int(nullable: false),
                        BookingID = c.Guid(nullable: false),
                        OfferCode = c.String(),
                        OfferType = c.String(maxLength: 100),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OfferDescription = c.String(maxLength: 150),
                        AppliesToRoomID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookingOfferID)
                .ForeignKey("dbo.Bookings", t => t.BookingID, cascadeDelete: true)
                .Index(t => t.BookingID);

            CreateTable(
                "dbo.Offers",
                c => new
                    {
                        OfferId = c.Int(nullable: false, identity: true),
                        OfferCode = c.Int(nullable: false),
                        OfferTypeID = c.Int(nullable: false),
                        OfferDescription = c.String(maxLength: 150),
                        AppliesToRoomID = c.Int(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MinSpend = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MultipleUse = c.Boolean(nullable: false),
                        AssignedTo = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.OfferId);

            CreateTable(
                "dbo.OfferTypes",
                c => new
                    {
                        OfferTypeId = c.Int(nullable: false, identity: true),
                        OfferModule = c.String(),
                        Type = c.String(maxLength: 30),
                        Description = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.OfferTypeId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.BookingOffers", "BookingID", "dbo.Bookings");
            DropIndex("dbo.BookingOffers", new[] { "BookingID" });
            DropTable("dbo.OfferTypes");
            DropTable("dbo.Offers");
            DropTable("dbo.BookingOffers");
        }
    }
}
