using MediatR;
using RentAGym.Application.Interfaces;
using RentAGym.Application.Specifications.HallSpecification;
using RentAGym.Application.Specifications.ScheduleSpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.TenantUseCases
{
    internal sealed class RegisterRentHandler : IRequestHandler<RegisterRentRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterRentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(RegisterRentRequest request, CancellationToken cancellationToken)
        {
            var alreadyExists = await _unitOfWork.ReservedScheduleRepository.AnyAsync(new ScheduleByDateTimeAndHallSpecification(request.hallId, request.from, request.to));
            var hall = await _unitOfWork.HallRepository.GetBySpecAsync(new GetHallWithScheduleSpecification(request.hallId));
            if (alreadyExists || request.tenantId == hall.LandlordId)
                return false;


            int dayCount = Math.Abs(request.to.DayOfYear - request.from.DayOfYear);
            var today = request.from;

            for (int i = 0; i <= dayCount; i++) //перебор каждого дня
            {
                List<ReservedSchedule> reservedHours = new List<ReservedSchedule>();
                int day = ((int)today.DayOfWeek == 0) ? 6 : (int)today.DayOfWeek-1; 

                int startingPoint = request.from.Date == request.to.Date || today.Date == request.from.Date? request.from.Hour : hall.WorkSchedule[day].WorkFrom.Hour;
                int endingPoint = request.from.Date == request.to.Date || today.Date == request.to.Date? request.to.Hour : hall.WorkSchedule[day].WorkTo.Hour;

                for (int j = startingPoint; j <= endingPoint; j++)
                {
                    reservedHours.Add(new ReservedSchedule
                    {
                        HallId = request.hallId,
                        TenantId = request.tenantId,
                        IsRegular = request.isRegular,
                        ReservedHour = new DateTime(today.Year, today.Month, today.Day, j, 0, 0),
                    });
                }

                if(i==0)
                    reservedHours.First().IsBorder = true;
                if(i==dayCount)
                    reservedHours.Last().IsBorder = true;


                await _unitOfWork.ReservedScheduleRepository.AddRangeAsync(reservedHours);
                today = today.AddDays(1);
            }
            await _unitOfWork.ReservedScheduleRepository.SaveChangesAsync();
            return true;
        }

    }
}
