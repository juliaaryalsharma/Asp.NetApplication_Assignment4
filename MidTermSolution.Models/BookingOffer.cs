using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MidTermSolution.Contracts.Models;

namespace MidTermSolution.Models
{
    public class BookingOffer : IBookingOffer
    {
        public int BookingOfferID { get; set; }
        public int OfferID { get; set; }
        public Guid BookingID { get; set; }
        [MaxLength(10)]
        public string OfferCode { get; set; }
        [MaxLength(100)]
        public string OfferType { get; set; }
        public decimal Value { get; set; }
        [MaxLength(150)]
        public string OfferDescription { get; set; }
        public int AppliesToRoomID { get; set; }

    }
}
