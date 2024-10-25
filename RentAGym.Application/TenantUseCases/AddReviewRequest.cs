using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.TenantUseCases
{
    public sealed record AddReviewRequest(Review review) : IRequest<bool>
    {
    }
}
