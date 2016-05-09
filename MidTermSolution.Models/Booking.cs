using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MidTermSolution.Contracts.Models;

namespace MidTermSolution.Models
{
    public class Booking : IBooking
    {
        private List<BookingRoom> _bookingRooms;
        private List<BookingOffer> _bookingOffers;

        public Guid BookingID { get; set; }
        public DateTime BookingDate { get; set; }

        public Booking()
        {
            this.BookingRooms = new List<BookingRoom>();
            this.BookingOffers = new List<BookingOffer>();
        }

        public decimal BookingTotal()
        {
            decimal? total = (from item in BookingRooms select (int?)item.NumberOfRooms * item.Room.Price).Sum();
            return total ?? decimal.Zero;
        }

        public decimal BookingRoomCount()
        {
            return _bookingRooms.Count();
        }

        public virtual ICollection<IBookingRoom> IBookingRooms { get { return _bookingRooms.ConvertAll(i => (IBookingRoom)i); } }
        public virtual ICollection<BookingRoom> BookingRooms { get { return _bookingRooms; } set { _bookingRooms = value.ToList(); } }
        public virtual ICollection<IBookingOffer> IBookingOffers { get { return _bookingOffers.ConvertAll(i => (IBookingOffer)i); } }
        public virtual ICollection<BookingOffer> BookingOffers { get { return _bookingOffers; } set { _bookingOffers = value.ToList(); } }

        public void AddBookingRoom(IBookingRoom item) { _bookingRooms.Add((BookingRoom)item); }
        public void AddBookingOffer(IBookingOffer offer) { _bookingOffers.Add((BookingOffer)offer); }
    }
}
