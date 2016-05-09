using System;
namespace MidTermSolution.Contracts.Models
{
    public interface IOffer
    {
        int AppliesToRoomID { get; set; }
        string AssignedTo { get; set; }
        decimal MinSpend { get; set; }
        bool MultipleUse { get; set; }
        string OfferCode { get; set; }
        string OfferDescription { get; set; }
        int OfferId { get; set; }
        int OfferTypeID { get; set; }
        decimal Value { get; set; }
    }
}
