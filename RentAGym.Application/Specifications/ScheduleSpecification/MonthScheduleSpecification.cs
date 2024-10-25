using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.Specifications.ScheduleSpecification
{
    public class MonthScheduleSpecification : Specification<ReservedSchedule>
    {
        public MonthScheduleSpecification(int hallId, DateOnly yearMonth)
        {
            Query.Where(sc => sc.HallId == hallId)
                .Where(sc=>sc.ReservedHour.Month == yearMonth.Month)
                .Where(sc=>sc.ReservedHour.Year == yearMonth.Year);
        }
    }
}
