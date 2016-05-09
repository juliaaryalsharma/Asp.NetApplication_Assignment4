using System;
namespace MidTermSolution.Contracts.Models
{
    public interface IRoom
    {
        string ImageUrl { get; set; }
        decimal Price { get; set; }
        string RoomDescription { get; set; }
        int RoomID { get; set; }
        string Type { get; set; }
        string Unit { get; set; }
    }
}
