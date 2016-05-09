using System;
namespace MidTermSolution.Contracts.Models
{
    public interface IBookingRoom
    {
        Guid BookingID { get; set; }
        int BookingRoomID { get; set; }
        IRoom IRoom { get; set; }
        int NumberOfRooms { get; set; }
        //Room Room { get; set; }
        int RoomID { get; set; }
    }
}
