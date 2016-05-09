using System;
namespace MidTermSolution.Contracts.Models
{
    public interface IBooking
    {
        Guid BookingID { get; set; }

        System.Collections.Generic.ICollection<IBookingOffer> IBookingOffers { get; }
        System.Collections.Generic.ICollection<IBookingRoom> IBookingRooms { get; }

        DateTime BookingDate { get; set; }
        void AddBookingOffer(IBookingOffer offer);
        void AddBookingRoom(IBookingRoom item);
        decimal BookingTotal();
        decimal BookingRoomCount();


        //System.Collections.Generic.ICollection<BookingOffer> BookingOffers { get; set; }
        //System.Collections.Generic.ICollection<BookingRoom> BookingRooms { get; set; }

    }
}
