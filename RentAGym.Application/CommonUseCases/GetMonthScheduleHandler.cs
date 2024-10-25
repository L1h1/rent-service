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
    internal sealed class GetMonthScheduleHandler : IRequestHandler<GetMonthScheduleRequest, IEnumerable<DateOnly>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetMonthScheduleHandler(IUnitOfWork unitOfwork)
        {
            _unitOfWork = unitOfwork;
        }

        public async Task<IEnumerable<DateOnly>> Handle(GetMonthScheduleRequest request, CancellationToken cancellationToken)
        {
            var hall = await _unitOfWork.HallRepository.GetBySpecAsync(new GetHallWithScheduleSpecification(request.hallId), cancellationToken);
            var monthSchedule = await _unitOfWork.ReservedScheduleRepository.ListAsync(new MonthScheduleSpecification(request.hallId, request.yearMonth), cancellationToken);
            var regularSchedule = await _unitOfWork.ReservedScheduleRepository.ListAsync(new RegularScheduleSpecification(request.hallId), cancellationToken);

            var daysInMonth = DateTime.DaysInMonth(request.yearMonth.Year, request.yearMonth.Month);
            //var datesOfMonth = Enumerable.Range(1, daysInMonth).Select(i => new DateOnly(request.yearMonth.Year, request.yearMonth.Month, i)).ToList();

    

            var groupedRegular = regularSchedule.GroupBy(sc => DateOnly.FromDateTime(sc.ReservedHour.Date))
                                    .Select(g => new {  Date =g.Key,
                                                        ScheduleCount = g.Count()
                                    }).ToList();


            Func<DateOnly, int> getDayOfWeek = date => date.DayOfWeek == 0 ? 6 : (int)date.DayOfWeek - 1;
            
            var datesWithThreshold = monthSchedule.GroupBy(sc => DateOnly.FromDateTime(sc.ReservedHour.Date))
                                    .Select(g => new {
                                        Date = g.Key,
                                        ScheduleCount = g.Select(p => p).Count() + (groupedRegular.Exists(reg=>reg.Date.DayOfWeek == g.Key.DayOfWeek)?groupedRegular.First(reg=>reg.Date.DayOfWeek == g.Key.DayOfWeek).ScheduleCount:0),
                                        Threshold = hall.WorkSchedule[getDayOfWeek(g.Key)].WorkTo.Hour - hall.WorkSchedule[getDayOfWeek(g.Key)].WorkFrom.Hour+1
                                    }).ToList();

            return datesWithThreshold.Where(g => g.ScheduleCount >= g.Threshold).Select(g => g.Date);


        }
    }
}
