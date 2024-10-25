using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Domain.Entities
{
    public class WorkSchedulePiece
    {
        public int Id { get; set; }
        public TimeOnly WorkFrom { get; set; }
        public TimeOnly WorkTo { get; set; }

        public int? HallId { get; set; }
    }
}
