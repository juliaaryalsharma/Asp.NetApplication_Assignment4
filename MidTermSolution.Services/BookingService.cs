using MidTermSolution.Contracts.Repositories;
using MidTermSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MidTermSolution.Services
{
    public class BookingService
    {
        IRepositoryBase<Booking> bookings;
        IRepositoryBase<BookingRoom> bookingrooms;

        private IRepositoryBase<Offer> offers;
        private IRepositoryBase<OfferType> offerTypes;
        private IRepositoryBase<BookingOffer> bookingOffers;

        public const string BookingSessionName = "eShoppingBooking";

        public BookingService(  IRepositoryBase<Booking> bookings,
                                IRepositoryBase<BookingRoom> bookingrooms,
                                IRepositoryBase<Offer> offers,
                                IRepositoryBase<OfferType> offerTypes,
                                IRepositoryBase<BookingOffer> bookingOffers)

        {
            this.bookings = bookings;
            this.bookingrooms = bookingrooms;
            this.offers = offers;
            this.offerTypes = offerTypes;
            this.bookingOffers = bookingOffers;
        }

        private Booking createNewBooking(HttpContextBase httpContext)
        {
            //create a new booking.
            //first create a new cookie.
            HttpCookie cookie = new HttpCookie(BookingSessionName);
            //now create a new booking and set the creation date.
            Booking booking = new Booking();
            booking.BookingDate = DateTime.Now;
            booking.BookingID = Guid.NewGuid();
            //add and persist in the dabase.
            bookings.Insert(booking);
            bookings.Commit();
            //add the booking id to a cookie
            cookie.Value = booking.BookingID.ToString();
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);
            return booking;
        }

        public bool AddToBooking(HttpContextBase httpContext, int roomId, int numberOfRooms)
        {
            bool success = true;
            Booking booking = GetBooking(httpContext);
            BookingRoom item = booking.BookingRooms.FirstOrDefault(i => i.RoomID == roomId);
            if (item == null)
            {
                item = new BookingRoom()
                {
                    BookingID = booking.BookingID,
                    RoomID = roomId,
                    NumberOfRooms = numberOfRooms
                };
                booking.BookingRooms.Add(item);
            }
            else
            {
                item.NumberOfRooms = item.NumberOfRooms + numberOfRooms;
            }
            bookings.Commit();
            return success;
        }

        public Booking GetBooking(HttpContextBase httpContext)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BookingSessionName);
            Booking booking;
            Guid bookingId;
            if (cookie != null)//checks if cookie is null
            {
                if (Guid.TryParse(cookie.Value, out bookingId))
                {
                    booking = bookings.GetById(bookingId);
                    if (booking == null)//booking not found in database
                    {
                        booking = createNewBooking(httpContext);
                    }
                }
                else
                {
                    booking = createNewBooking(httpContext);
                }//end inner if-else
            }//end outer if
            else
            {
                booking = createNewBooking(httpContext);
            }

            return booking;
        }
        // new services
        public int QuantityInBooking(HttpContextBase httpContext)
        {
            int numberOfRoom = 0;
            Booking booking = GetBooking(httpContext);
            if (booking == null) return 0;
            numberOfRoom = booking.BookingRooms.Select(c => c.NumberOfRooms).Sum();
            return numberOfRoom;
        }
        public decimal AmountInBooking(HttpContextBase httpContext)
        {
            decimal total = 0;
            Booking booking = GetBooking(httpContext);
            if (booking == null) return 0;
            var itemtotal = booking.BookingRooms.Select(c => new { amount = c.NumberOfRooms * c.Room.Price });
            total = itemtotal.Select(c => c.amount).Sum();

            decimal off = 0;
            var itemoff = booking.BookingOffers.Select(c => new { amount = c.Value});
            off = itemoff.Select(c => c.amount).Sum();
            return total+off;
        }
        public BookingRoom GetBookingRoomById(int BookingRoomID)
        {
            return bookingrooms.GetById(BookingRoomID);
        }
        public bool RemoveFromBooking(int BookingRoomID)
        {
            bookingrooms.Delete(bookingrooms.GetById(BookingRoomID));
            bookingrooms.Commit();

            return true;
        }
        public BookingRoom GetBookingRoom(HttpContextBase httpContext, int itemId)
        {
            Booking booking = GetBooking(httpContext);

            foreach (var b in booking.BookingRooms)
            {
                if (b.BookingRoomID == itemId)
                    return b;
            }
            return null;
        }

        public void AddOffer(string offerCode, HttpContextBase httpContext)
        {
            Booking booking = GetBooking(httpContext);
            Offer offer = offers.GetAll().FirstOrDefault(c => c.OfferCode == offerCode);
            if (offer != null)
            {
                OfferType offerType = offerTypes.GetById(offer.OfferTypeID);
                if (offerType != null)
                {
                    BookingOffer bookingOffer = new BookingOffer();
                    if (offerType.Type == "MoneyOff")
                    {
                        MoneyOff(offer, booking, bookingOffer);
                    }
                    if (offerType.Type == "PercentOff")
                    {
                        PercentOff(offer, booking, bookingOffer);
                    }
                    bookings.Commit();
                }//end offerType if
            }//end offer if
        }//end addOffer

        private void MoneyOff(Offer offer, Booking booking, BookingOffer bookingOffer)
        {
            decimal bookingTotal = booking.BookingTotal();
            if (offer.MinSpend < bookingTotal)
            {
                bookingOffer.Value = offer.Value * -1;
                bookingOffer.OfferCode = offer.OfferCode;
                bookingOffer.OfferDescription = offer.OfferDescription;
                bookingOffer.OfferID = offer.OfferId;
                bookingOffer.OfferType = "Money Off";
                booking.AddBookingOffer(bookingOffer);
            }
        }

        private void PercentOff(Offer offer, Booking booking, BookingOffer bookingOffer)
        {
            if (offer.MinSpend < booking.BookingTotal())
            {
                bookingOffer.Value = (offer.Value * (booking.BookingTotal() / 100)) * -1;
                bookingOffer.OfferCode = offer.OfferCode;
                bookingOffer.OfferDescription = offer.OfferDescription;
                bookingOffer.OfferID = offer.OfferId;
                bookingOffer.OfferType = "Percent Off";
                booking.AddBookingOffer(bookingOffer);
            }
        }

        public void DeleteOffer(int id, HttpContextBase httpContext)
        {
            bookingOffers.Delete(id);
            bookingOffers.Commit();
        }
    }
}
