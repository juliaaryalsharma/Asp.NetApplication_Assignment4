using System;
namespace MidTermSolution.Contracts.Models
{
    public interface IBookingOffer
    {
        int AppliesToRoomID { get; set; }
        Guid BookingID { get; set; }
        int BookingOfferID { get; set; }
        string OfferCode { get; set; }
        string OfferDescription { get; set; }
        int OfferID { get; set; }
        string OfferType { get; set; }
        decimal Value { get; set; }
    }
}
