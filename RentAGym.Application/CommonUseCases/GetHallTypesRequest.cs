using MediatR;
using RentAGym.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.CommonUseCases
{
    public sealed record GetHallTypesRequest : IRequest<IEnumerable<HallType>>
    {
    }
}
