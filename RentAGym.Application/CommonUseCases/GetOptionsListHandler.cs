using RentAGym.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.CommonUseCases
{
    public class GetOptionsListHandler :
        IRequestHandler<GetOptionsListRequest, IEnumerable<Option>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOptionsListHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        public async Task<IEnumerable<Option>> Handle(GetOptionsListRequest request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.OptionRepository.ListAsync();
        }
    }
}
