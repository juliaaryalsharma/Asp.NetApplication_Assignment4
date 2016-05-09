using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidTermSolution.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public DateTime ReservationDate { get; set; }
        public int CustomerID { get; set; }
        public ICollection<ReservationRoom> ReservationRooms { get; set; }
    }
}
