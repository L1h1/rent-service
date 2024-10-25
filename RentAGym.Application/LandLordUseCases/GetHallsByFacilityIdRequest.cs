using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.LandLordUseCases
{
    public sealed record GetHallsByFacilityIdRequest (int id) : IRequest<IEnumerable<Hall>>
    {
    }
}
