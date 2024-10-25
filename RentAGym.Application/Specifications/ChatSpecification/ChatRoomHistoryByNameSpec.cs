using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.Specifications.ChatSpecification
{
    internal class ChatRoomHistoryByNameSpec : Specification<ChatMessage>
    {
        public ChatRoomHistoryByNameSpec(string name) {
            Query.Where(r => r.ChatName == name);
        }
    }
}
