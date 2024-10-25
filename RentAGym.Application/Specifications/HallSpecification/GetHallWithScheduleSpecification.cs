using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.Specifications.HallSpecification
{
    public class GetHallWithScheduleSpecification :Specification<Hall>
    {
        public GetHallWithScheduleSpecification(int id)
        {
            Query.Where(h => h.Id == id)
                .Include(h=>h.WorkSchedule)
                ;

        }
    }
}
