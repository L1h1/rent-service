using RentAGym.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.CommonUseCases
{
    public sealed record GetUserInfoRequest(string id) :IRequest<UserDTO>
    {
    }
}
