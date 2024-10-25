using RentAGym.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.Dto
{
    public class HallListRequestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public ImageData Thumbnail { get; set; }
        public double OverallRating { get; set; } //пока не реализовано
    }
}
