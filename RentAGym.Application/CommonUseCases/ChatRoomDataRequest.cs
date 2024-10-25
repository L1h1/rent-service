using RentAGym.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.CommonUseCases
{
    public sealed record class ChatRoomDataRequest (int rentBorderId) : IRequest<RentDTO>
    {
    }
}
