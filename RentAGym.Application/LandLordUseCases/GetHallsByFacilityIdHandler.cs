using RentAGym.Application.Interfaces;
using RentAGym.Application.Specifications.HallSpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.LandLordUseCases
{
    internal class GetHallsByFacilityIdHandler : IRequestHandler<GetHallsByFacilityIdRequest,IEnumerable<Hall>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetHallsByFacilityIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Hall>> Handle(GetHallsByFacilityIdRequest request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.HallRepository;
            return await repository.ListAsync(new GetHallsByFacilitySpecification(request.id));
        }
    }
}
