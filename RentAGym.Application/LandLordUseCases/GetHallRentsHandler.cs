using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RentAGym.Application.Dto;
using RentAGym.Application.Interfaces;
using RentAGym.Application.Specifications.HallSpecification;
using RentAGym.Application.Specifications.ScheduleSpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.LandLordUseCases
{
    internal class GetHallRentsHandler : IRequestHandler<GetHallRentsRequest, IEnumerable<RentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceProvider _serviceProvider;
        public GetHallRentsHandler(IUnitOfWork u, IServiceProvider serviceProvider) {
            _unitOfWork = u;
            _serviceProvider = serviceProvider;
        }
        public async Task<IEnumerable<RentDTO>> Handle(GetHallRentsRequest request, CancellationToken cancellationToken)
        {
            using var userManager = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var rents = await _unitOfWork.ReservedScheduleRepository.ListAsync(new RentByLandlordSpecification(request.landlordId));
            List<RentDTO> result = new List<RentDTO>();
            bool isProcessing = false;
            RentDTO currentRent = new();
            foreach (var rent in rents)
            {
                if (rent.IsBorder)
                {
                    if (!isProcessing)
                    {
                        isProcessing = true;
                        currentRent = new();

                        var hall = await _unitOfWork.HallRepository.GetBySpecAsync(new HallDetailsByIdSpecification(rent.HallId));
                        var user = await userManager.FindByIdAsync(rent.TenantId);
                        currentRent.TenantId = user.Id;
                        currentRent.TenantName = user.UserName;
                        currentRent.DateFrom = rent.ReservedHour;
                        currentRent.HallId = hall.Id;
                        currentRent.HallName = hall.Name;
                        currentRent.PreviewImage = hall.Images[0];
                        currentRent.DateFrom = rent.ReservedHour;
                        currentRent.RentBorderId = rent.Id;
                    }
                    else if(isProcessing)
                    {
                        isProcessing = false;
                        currentRent.DateTo = rent.ReservedHour;
                        currentRent.IsDone = DateTime.Now>=rent.ReservedHour;
                        result.Add(currentRent);
                    }
                }
            }




            return result;
        }

    }
}
