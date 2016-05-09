using MidTermSolution.Contracts.Models;
using System;
namespace MidTermSolution.Contracts.Modules
{
    public interface IeOffer
    {
        void ProcessOffer(IOffer offer, IBooking booking, IBookingOffer bookingOffer);
    }
}
