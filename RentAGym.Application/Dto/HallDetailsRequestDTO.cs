using RentAGym.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.Dto
{
    public class HallDetailsRequestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool PaymentType { get; set; }
        public List<WorkSchedulePiece> WorkSchedule { get; set; }
        public List<ImageData> Images { get; set; }
        public List<Review>? Reviews { get; set; }
        public double OverallRating { get; set; } //пока не реализовано
        public int ReviewCount { get; set; }

        //Facility
        public double Latitude { get; set; } //= 0;       
        public double Longitude { get; set; }// = 0;
        public string Address { get; set; }
        public string City { get; set; }



        //Landlord
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }


    }
}
