using MidTermSolution.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidTermSolution.Modules.Offers.PercentOff
{
    public class eOffer
    {
        public void ProcessOffer(IOffer offer, IBooking booking, IBookingOffer bookingOffer)
        {
            if (offer.MinSpend < booking.BookingTotal())
            {
                bookingOffer.Value = offer.Value * (booking.BookingTotal() / 100);
                bookingOffer.OfferCode = offer.OfferCode;
                bookingOffer.OfferDescription = offer.OfferDescription;
                bookingOffer.OfferID = offer.OfferId;
                booking.AddBookingOffer(bookingOffer);
            }
        }
    }
}
