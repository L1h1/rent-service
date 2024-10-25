using RentAGym.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.LandLordUseCases
{
    internal sealed class CreateHallHandler : IRequestHandler<CreateHallRequest,bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateHallHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateHallRequest request, CancellationToken cancellationToken)
        {
            var tempHall = _mapper.Map<Hall>(request.requestDTO);
            try
            {
                await _unitOfWork.HallRepository.SaveChangesAsync();
                tempHall.LandlordId = (await _unitOfWork.FacilityRepository.GetByIdAsync(request.requestDTO.FacilityId)).LandLordId;    //!!!
                var hall = await _unitOfWork.HallRepository.AddAsync(tempHall);
                await _unitOfWork.HallRepository.SaveChangesAsync();

                foreach(var preFile in request.requestDTO.ImagePaths)
                {
                    hall.Images.Add(new ImageData() {Name=preFile, ImageUri = preFile, HallId = hall.Id });
                }
                await _unitOfWork.HallRepository.UpdateAsync(tempHall);
                await _unitOfWork.HallRepository.SaveChangesAsync();

            }catch (Exception ex)
            {
                return false;
            }

            



            return true;
        }
    }
}
