
using MediatR.Pipeline;
using RentAGym.Application.Interfaces;
using RentAGym.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.CommonUseCases
{
    internal sealed class GetHallTypesHandler : IRequestHandler<GetHallTypesRequest, IEnumerable<HallType>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetHallTypesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<HallType>> Handle(GetHallTypesRequest request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.HallTypeRepository;
            return await repository.ListAsync();
        }
    }
}
