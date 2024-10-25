using MediatR;
using RentAGym.Application.Interfaces;
using RentAGym.Application.Specifications;
using RentAGym.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.LandLordUseCases
{
    internal sealed class GetFacilitiesListHandler : IRequestHandler<GetFacilitiesListRequest, IEnumerable<Facility>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFacilitiesListHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Facility>> Handle(GetFacilitiesListRequest request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.FacilityRepository;
            return await repository.ListAsync(new FacilityListSpecification(request.Id), cancellationToken);           
        }
    }
}
