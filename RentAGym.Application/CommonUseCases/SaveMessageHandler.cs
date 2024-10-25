using RentAGym.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.CommonUseCases
{
    internal class SaveMessageHandler : IRequestHandler<SaveMessageRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public SaveMessageHandler(IUnitOfWork unitofwork) { 
            _unitOfWork = unitofwork;
        }
        public async Task<bool> Handle(SaveMessageRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.ChatMessageRepository.AddAsync(request.msg);
                return true;
            }catch(Exception ex)
            {

                return false;
            }
        }
    }
}
