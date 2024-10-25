using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.Dto
{
    public class EditHallRequestDTO
    {
        public int Id { get;set; }
        public double? Area { get; set; }
        public double? BasePrice { get; set; }  
        public bool? Payment { get; set; }
        public List<ImageData>? Images { get; set; }
        public List<WorkSchedulePiece>? WorkSchedulePieces { get; set; }
        public string? Description { get; set; }
        public int? HallTypeId { get; set; }
        public List<Option>? Options { get; set; }

    }
}
