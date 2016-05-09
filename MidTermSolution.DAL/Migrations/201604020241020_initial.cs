namespace MidTermSolution.Contracts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BasketItems", "ProductID", "dbo.Products");
            DropForeignKey("dbo.BasketItems", "BasketID", "dbo.Baskets");
            DropForeignKey("dbo.OrderItems", "Order_OrderID", "dbo.Orders");
            DropIndex("dbo.BasketItems", new[] { "BasketID" });
            DropIndex("dbo.BasketItems", new[] { "ProductID" });
            DropIndex("dbo.OrderItems", new[] { "Order_OrderID" });
            CreateTable(
                "dbo.BookingRooms",
                c => new
                    {
                        BookingRoomID = c.Int(nullable: false, identity: true),
                        BookingID = c.Guid(nullable: false),
                        RoomID = c.Int(nullable: false),
                        NumberOfRooms = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookingRoomID)
                .ForeignKey("dbo.Rooms", t => t.RoomID, cascadeDelete: true)
                .ForeignKey("dbo.Bookings", t => t.BookingID, cascadeDelete: true)
                .Index(t => t.BookingID)
                .Index(t => t.RoomID);

            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomID = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(maxLength: 255),
                        Unit = c.String(),
                        Type = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RoomDescription = c.String(),
                    })
                .PrimaryKey(t => t.RoomID);

            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingID = c.Guid(nullable: false),
                        BookingDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BookingID);

            CreateTable(
                "dbo.ReservationRooms",
                c => new
                    {
                        ReservationRoomId = c.Int(nullable: false, identity: true),
                        RoomID = c.Int(nullable: false),
                        NumberOfRooms = c.Int(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Reservation_ReservationID = c.Int(),
                    })
                .PrimaryKey(t => t.ReservationRoomId)
                .ForeignKey("dbo.Reservations", t => t.Reservation_ReservationID)
                .Index(t => t.Reservation_ReservationID);

            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationID = c.Int(nullable: false, identity: true),
                        ReservationDate = c.DateTime(nullable: false),
                        CustomerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationID);

            DropTable("dbo.BasketItems");
            DropTable("dbo.Products");
            DropTable("dbo.Baskets");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Orders");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        CustomerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID);

            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        OrderItemId = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Order_OrderID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderItemId);

            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        BasketID = c.Guid(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BasketID);

            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(maxLength: 255),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CostPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductDescription = c.String(),
                    })
                .PrimaryKey(t => t.ProductID);

            CreateTable(
                "dbo.BasketItems",
                c => new
                    {
                        BasketItemID = c.Int(nullable: false, identity: true),
                        BasketID = c.Guid(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BasketItemID);

            DropForeignKey("dbo.ReservationRooms", "Reservation_ReservationID", "dbo.Reservations");
            DropForeignKey("dbo.BookingRooms", "BookingID", "dbo.Bookings");
            DropForeignKey("dbo.BookingRooms", "RoomID", "dbo.Rooms");
            DropIndex("dbo.ReservationRooms", new[] { "Reservation_ReservationID" });
            DropIndex("dbo.BookingRooms", new[] { "RoomID" });
            DropIndex("dbo.BookingRooms", new[] { "BookingID" });
            DropTable("dbo.Reservations");
            DropTable("dbo.ReservationRooms");
            DropTable("dbo.Bookings");
            DropTable("dbo.Rooms");
            DropTable("dbo.BookingRooms");
            CreateIndex("dbo.OrderItems", "Order_OrderID");
            CreateIndex("dbo.BasketItems", "ProductID");
            CreateIndex("dbo.BasketItems", "BasketID");
            AddForeignKey("dbo.OrderItems", "Order_OrderID", "dbo.Orders", "OrderID");
            AddForeignKey("dbo.BasketItems", "BasketID", "dbo.Baskets", "BasketID", cascadeDelete: true);
            AddForeignKey("dbo.BasketItems", "ProductID", "dbo.Products", "ProductID", cascadeDelete: true);
        }
    }
}
