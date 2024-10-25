using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.Dto
{
    public class CreateHallRequestDTO
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double Area { get; set; }
        public bool Payment { get; set; }
        public List<string> ImagePaths { get; set; } = new();
        public List<ImageData> Images { get; set; } = new();
        public string Description { get; set; }

        public List<Option> Options { get; set; } = new();
        public List<WorkSchedulePiece> WorkSchedule { get; set; } = new();

        public int FacilityId { get; set; }
        public int HallTypeId { get; set; }

    }
}
