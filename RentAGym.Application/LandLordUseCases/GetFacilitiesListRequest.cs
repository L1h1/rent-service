using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RentAGym.Domain.Entities;

namespace RentAGym.Application.LandLordUseCases
{
    public sealed record GetFacilitiesListRequest(string Id) : IRequest<IEnumerable<Facility>>
    {
    }
}
