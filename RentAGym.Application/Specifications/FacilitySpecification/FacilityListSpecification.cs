using Ardalis.Specification;
using RentAGym.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.Specifications
{
    internal class FacilityListSpecification : Specification<Facility>
    {
        public FacilityListSpecification(string id)
        {
            Query.Where(f=>f.LandLordId.Equals(id)).Include(f=>f.Halls)
                .Include(f=>f.Halls).ThenInclude(h=>h.Images);
        }
    }
}
