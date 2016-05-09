namespace MidTermSolution.Contracts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddOfferCodeString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BookingOffers", "OfferCode", c => c.String(maxLength: 10));
            AlterColumn("dbo.Offers", "OfferCode", c => c.String(maxLength: 10));
        }

        public override void Down()
        {
            AlterColumn("dbo.Offers", "OfferCode", c => c.Int(nullable: false));
            AlterColumn("dbo.BookingOffers", "OfferCode", c => c.String());
        }
    }
}
