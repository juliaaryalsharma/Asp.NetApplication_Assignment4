using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MidTermSolution.Contracts.Models;

namespace MidTermSolution.Models
{
    public class Offer : IOffer
    {
        public int OfferId { get; set; }
        [MaxLength(10)]
        public string OfferCode { get; set; }
        public int OfferTypeID { get; set; }
        [MaxLength(150)]
        public string OfferDescription { get; set; }
        public int AppliesToRoomID { get; set; }//to apply to specific Room, based on ID
        public decimal Value { get; set; }
        public decimal MinSpend { get; set; }
        public bool MultipleUse { get; set; }
        [MaxLength(255)]
        public string AssignedTo { get; set; }

    }
}
