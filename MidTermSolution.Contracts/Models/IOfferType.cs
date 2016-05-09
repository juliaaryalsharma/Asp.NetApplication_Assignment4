using System;
namespace MidTermSolution.Contracts.Models
{
    public interface IOfferType
    {
        string Description { get; set; }
        string OfferModule { get; set; }
        int OfferTypeId { get; set; }
        string Type { get; set; }
    }
}
