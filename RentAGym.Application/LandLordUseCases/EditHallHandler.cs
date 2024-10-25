using RentAGym.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.LandLordUseCases
{
    public sealed class EditHallHandler : IRequestHandler<EditHallRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EditHallHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Handle(EditHallRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var oldHall = await _unitOfWork.HallRepository.GetByIdAsync(request.dto.Id);
                _mapper.Map(request.dto,oldHall);
                await _unitOfWork.HallRepository.SaveChangesAsync();
            }catch (Exception ex)
            {
                return false;
            }


            return true;
        }
    }
}
