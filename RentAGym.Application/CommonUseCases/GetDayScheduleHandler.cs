using RentAGym.Application.Dto;
using RentAGym.Application.Interfaces;
using RentAGym.Application.Specifications.HallSpecification;
using RentAGym.Application.Specifications.ScheduleSpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.CommonUseCases
{

    internal sealed class GetDayScheduleHandler : IRequestHandler<GetDayScheduleRequest, IEnumerable<TimeOnly>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetDayScheduleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TimeOnly>> Handle(GetDayScheduleRequest request, CancellationToken cancellationToken)
        {
            var hall = await _unitOfWork.HallRepository.GetBySpecAsync(new GetHallWithScheduleSpecification(request.hallId));
            var reservedSchedule = await _unitOfWork.ReservedScheduleRepository.ListAsync(new DayScheduleSpecification(request.hallId, request.date), cancellationToken);
            var regularSchedule = await _unitOfWork.ReservedScheduleRepository.ListAsync(new RegularScheduleSpecification(request.hallId), cancellationToken);

            int selectedDay = ((int)request.date.DayOfWeek == 0) ? 6 : (int)request.date.DayOfWeek-1; 

            var allWorkHours = Enumerable
                                .Range(0, hall.WorkSchedule[selectedDay].WorkFrom.Hour).Concat(Enumerable.Range(hall.WorkSchedule[selectedDay].WorkTo.Hour + 1, 23 - hall.WorkSchedule[selectedDay].WorkTo.Hour))
                                .Select(hr => TimeOnly.Parse($"{hr}:00")).ToList();

            if(request.date == DateOnly.FromDateTime(DateTime.Now.Date))
            {
                allWorkHours=allWorkHours.Concat(Enumerable.Range(hall.WorkSchedule[selectedDay].WorkFrom.Hour, DateTime.Now.Hour - hall.WorkSchedule[selectedDay].WorkFrom.Hour + 1)
                            .Select(hr => TimeOnly.Parse($"{hr}:00")).ToList()).ToList();
            }

            return allWorkHours.Concat(reservedSchedule.Select(rs => TimeOnly.FromDateTime(rs.ReservedHour)))
                               .Concat(regularSchedule.Where(sc=>sc.ReservedHour.DayOfWeek==request.date.DayOfWeek && DateOnly.FromDateTime(sc.ReservedHour.Date) <=request.date)
                               .Select(rs => TimeOnly.FromDateTime(rs.ReservedHour)));
        }
    }
}
