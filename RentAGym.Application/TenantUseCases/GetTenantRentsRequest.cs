using RentAGym.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.TenantUseCases
{
    public sealed record GetTenantRentsRequest(string tenantId) : IRequest<IEnumerable<RentDTO>>
    {
    }
}
