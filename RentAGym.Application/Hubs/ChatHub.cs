using Microsoft.AspNetCore.SignalR;
using RentAGym.Application.CommonUseCases;
using RentAGym.Application.Interfaces;

namespace RentAGym.UI.rc2.Components.Pages.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;
        public ChatHub(IMediator mediator) {
           _mediator = mediator;
        
        }



        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }



        public async Task SendPrivate(string user, string groupName, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
            
            await _mediator.Send(new SaveMessageRequest(new ChatMessage()
               {
                   ChatName = groupName,
                   Message = $"{user}: {message}",
                   SenderName =user,
                   DateTime = DateTime.Now
               }));
             
        }

        public async Task ConnectToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }


    }
}
