using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.Specifications.ScheduleSpecification
{
    internal class ScheduleByDateTimeAndHallSpecification : Specification<ReservedSchedule>
    {
        public ScheduleByDateTimeAndHallSpecification(int hallId,DateTime from, DateTime to)
        {
            Query.Where(sc=>sc.HallId==hallId && sc.ReservedHour>=from && sc.ReservedHour <= to);
        }
    }
}
