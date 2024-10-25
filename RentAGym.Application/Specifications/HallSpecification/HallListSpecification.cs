using Ardalis.Specification;
using RentAGym.Application.Filters;
using RentAGym.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.Specifications.HallSpecification
{
    internal class HallListSpecification : Specification<Hall>
    {
        public HallListSpecification(HallListFilter filter)
        {

            Query
                .Include(h => h.Options)
                .Include(h => h.Images)
                .Where(h => filter.TypeId == 0 || h.HallTypeId == filter.TypeId)
                .Where(h => filter.RegionId == 0 || h.Facility.RegionId == filter.RegionId)
                .Where(h => h.Payment == filter.PaymentType
                        && h.Area >= filter.SquareFrom
                                        && h.Area <= (filter.SquareTo ?? double.MaxValue)
                        && h.BasePrice >= filter.PriceFrom
                                        && h.BasePrice <= (filter.PriceTo ?? double.MaxValue))
                .Where(h => filter.OptionIds == null ||
                            (h.Options.Any()
                                && (h.Options.Where(o => filter.OptionIds.Contains(o.Id)).Count() == filter.OptionIds.Count)))
                
                .Where(h=>filter.Timestamp == null || 
                !h.ReservedSchedules.Any(sc=>sc.ReservedHour == filter.Timestamp))
                
                .Where(h => (filter.TimeFrom == null && filter.TimeTo == null) ||
                !h.ReservedSchedules.Any(sc=>sc.ReservedHour>=filter.TimeFrom && sc.ReservedHour<=filter.TimeTo))
                
                
                ;
                

        }
    }
}
