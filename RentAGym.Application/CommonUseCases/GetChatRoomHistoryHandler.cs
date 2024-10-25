using RentAGym.Application.Interfaces;
using RentAGym.Application.Specifications.ChatSpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.CommonUseCases
{
    internal class GetChatRoomHistoryHandler : IRequestHandler<GetChatRoomHistoryRequest, IEnumerable<ChatMessage>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetChatRoomHistoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ChatMessage>> Handle(GetChatRoomHistoryRequest request, CancellationToken cancellationToken)
        {
            var resp = await _unitOfWork.ChatMessageRepository.ListAsync(new ChatRoomHistoryByNameSpec(request.name));
            return resp;
        }
    }
}
