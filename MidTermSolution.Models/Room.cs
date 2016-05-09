using MidTermSolution.Contracts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidTermSolution.Models
{
    public class Room : IRoom
    {
        public int RoomID { get; set; }
        [MaxLength(255)]
        public string ImageUrl { get; set; }
        public string Unit { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public string RoomDescription { get; set; }
    }
}
