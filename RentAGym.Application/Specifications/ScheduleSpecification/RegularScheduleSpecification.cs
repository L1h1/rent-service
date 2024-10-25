using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RentAGym.Application.Specifications.ScheduleSpecification
{
    public class RegularScheduleSpecification : Specification<ReservedSchedule>
    {
        public RegularScheduleSpecification(int hallId)
        {
            Query
                .Where(sc => sc.HallId == hallId)
                .Where(sc => sc.IsRegular)
                ;

        }
    }
}
