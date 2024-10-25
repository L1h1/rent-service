using MediatR;
using RentAGym.Application.Dto;
using RentAGym.Application.Interfaces;
using RentAGym.Application.Specifications.HallSpecification;
using RentAGym.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.CommonUseCases
{
    internal sealed class GetHallListHandler : IRequestHandler<GetHallListRequest, IEnumerable<HallListRequestDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetHallListHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HallListRequestDTO>> Handle(GetHallListRequest request, CancellationToken cancellationToken)
        {

            var repository = _unitOfWork.HallRepository;

            var response = await repository.ListAsync(
                new HallListSpecification(request.filter), cancellationToken);

            return _mapper.Map<IEnumerable<HallListRequestDTO>>(response);
        }
    }
}
