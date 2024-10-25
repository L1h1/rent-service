using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.Filters
{
    public class HallListFilter
    {
        public int TypeId { get; set; }
        public bool PaymentType { get; set; } = false;
        public int RegionId { get; set; }
        public double SquareFrom { get; set; } = 0;
        public double? SquareTo { get; set; }
        public double PriceFrom { get; set; } = 0;
        public double? PriceTo { get; set; }
        //Timestamp для поиска на конкретный день
        public DateTime? Timestamp { get; set; }
        //Для поиска окна во временном промежутке
        public DateTime? TimeFrom { get; set; }
        public DateTime? TimeTo { get; set; }

        public List<int>? OptionIds { get; set; }
    }
}
