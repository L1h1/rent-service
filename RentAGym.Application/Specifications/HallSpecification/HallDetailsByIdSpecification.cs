using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.Specifications.HallSpecification
{
    public class HallDetailsByIdSpecification : Specification<Hall>
    {
        public HallDetailsByIdSpecification(int id)
        {
            Query.Where(h=>h.Id == id)
                .Include(h=>h.Facility)
                .Include(h=>h.Facility.Landlord)
                .Include(h=>h.Images)
                .Include(h=>h.Reviews);

        }
    }
}
