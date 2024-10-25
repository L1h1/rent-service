using RentAGym.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.LandLordUseCases
{
    public sealed record CreateHallRequest(CreateHallRequestDTO requestDTO) :IRequest<bool>
    {
        
    }
}
