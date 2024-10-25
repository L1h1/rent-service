using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.Specifications.ScheduleSpecification
{
    internal class RentByLandlordSpecification : Specification<ReservedSchedule>
    {
        public RentByLandlordSpecification(string landlordId) { 
            Query.Where(r=>r.Hall.LandlordId == landlordId);
        }
    }
}
