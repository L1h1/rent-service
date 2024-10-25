using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.Specifications.ScheduleSpecification
{
    public class DayScheduleSpecification :Specification<ReservedSchedule>
    {
        public DayScheduleSpecification(int hallId,DateOnly date){
            Query
                .Where(sc => sc.HallId == hallId)
                .Where(sc => DateOnly.FromDateTime(sc.ReservedHour.Date) == date)
                
                ;
        
        }
    }
}
