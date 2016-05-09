using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidTermSolution.Models
{
    public class ReservationRoom
    {
        public int ReservationRoomId { get; set; }
        public int RoomID { get; set; }
        public int NumberOfRooms { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
