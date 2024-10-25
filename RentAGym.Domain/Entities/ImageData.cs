using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Domain.Entities
{
    public class ImageData : Entity
    {
        public string ImageUri { get; set; }
        public int HallId { get; set; }
    }
}
