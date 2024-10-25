using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.Specifications.HallSpecification
{
    internal class GetLandlordHallsSpecification : Specification<Hall>
    {
        public GetLandlordHallsSpecification(string landlordId) {
            Query.Where(h => h.LandlordId == landlordId)
                .Include(h => h.Images)
                .Include(h => h.ReservedSchedules);
        }
    }
}
