using MidTermSolution.Contracts.Models;
using MidTermSolution.Contracts.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidTermSolution.Modules.Offers.MoneyOff
{
    public class eOffer : IeOffer
    {
        public void ProcessOffer(IOffer offer, IBooking booking, IBookingOffer bookingOffer)
        {
            if (offer.MinSpend < booking.BookingTotal())
            {
                bookingOffer.Value = offer.Value;
                bookingOffer.OfferCode = offer.OfferCode;
                bookingOffer.OfferDescription = offer.OfferDescription;
                bookingOffer.OfferID = offer.OfferId;
                booking.AddBookingOffer(bookingOffer);
            }
        }

    }
}
