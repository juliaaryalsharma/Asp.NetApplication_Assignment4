using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MidTermSolution.Contracts.Models;

namespace MidTermSolution.Models
{
    public class OfferType : IOfferType
    {
        public int OfferTypeId { get; set; }
        public string OfferModule { get; set; }
        [MaxLength(30)]
        public string Type { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }
    }
}
