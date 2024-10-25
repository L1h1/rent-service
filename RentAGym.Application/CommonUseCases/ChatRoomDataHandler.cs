using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RentAGym.Application.Dto;
using RentAGym.Application.Interfaces;
using RentAGym.Application.Specifications.HallSpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.CommonUseCases
{
    internal class ChatRoomDataHandler : IRequestHandler<ChatRoomDataRequest, RentDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceProvider _serviceProvider;
        public ChatRoomDataHandler(IUnitOfWork u, IServiceProvider serviceProvider) 
        {
            _unitOfWork = u;
            _serviceProvider = serviceProvider;
        }
        public async Task<RentDTO> Handle(ChatRoomDataRequest request, CancellationToken cancellationToken)
        {
            var rentPiece = await _unitOfWork.ReservedScheduleRepository.GetByIdAsync(request.rentBorderId);
            var additionalData = await _unitOfWork.HallRepository.GetBySpecAsync(new HallDetailsByIdSpecification(rentPiece.HallId));
            using var userManager = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var tenant = await userManager.FindByIdAsync(rentPiece.TenantId);
            var landlord = await userManager.FindByIdAsync(rentPiece.Hall.LandlordId);
            RentDTO dto = new() { LandlordName = landlord.UserName, TenantName = tenant.UserName, RentBorderId = rentPiece.Id, HallId = rentPiece.HallId };
            return dto;
        }
    }
}
