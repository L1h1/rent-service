using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Domain.Entities
{
    /// <summary>
    /// Объект с залами
    /// </summary>
    public class Facility : Entity
    {


        // Адрес
        public int RegionId { get; set; }
        public Region Region { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }


        public double Latitude { get; set; } //= 0;       
        public double Longitude { get; set; }// = 0;




        // Навигационные свойства
        public List<Hall> Halls { get; set; } = new List<Hall>();

        public string LandLordId { get; set; }
        public Landlord Landlord { get; set; }
    }
}
