using MidTermSolution.Contracts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidTermSolution.Models
{
    public class BookingRoom : IBookingRoom
    {
        private Room _room;

        public int BookingRoomID { get; set; }
        public Guid BookingID { get; set; }
        public int RoomID { get; set; }
        public int NumberOfRooms { get; set; }

        public virtual IRoom IRoom { get { return _room as IRoom; } set { _room = value as Room; } }
        public virtual Room Room { get { return _room; } set { _room = value; } }

    }
}
