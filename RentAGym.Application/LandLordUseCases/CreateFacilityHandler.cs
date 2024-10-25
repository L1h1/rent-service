using RentAGym.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.LandLordUseCases
{
    internal class CreateFacilityHandler : IRequestHandler<CreateFacilityRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFacilityHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CreateFacilityRequest request, CancellationToken cancellationToken)
        {
            var tempFac = _mapper.Map<Facility>(request.requestDTO);
            try
            {
                await _unitOfWork.FacilityRepository.AddAsync(tempFac);
                await _unitOfWork.FacilityRepository.SaveChangesAsync();

            }catch (Exception ex)
            {
                return false;
            }
            return true;

        }
    }
}
