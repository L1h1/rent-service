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
    internal sealed class GetHallByIdHandler :IRequestHandler<GetHallByIdRequest,HallDetailsRequestDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetHallByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<HallDetailsRequestDTO> Handle(GetHallByIdRequest request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.HallRepository;

            var response = await repository.GetBySpecAsync(new HallDetailsByIdSpecification(request.id),cancellationToken);

            return _mapper.Map<HallDetailsRequestDTO>(response);
        }
    }
}
