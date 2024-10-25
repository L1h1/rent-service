using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RentAGym.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.CommonUseCases
{
    internal class GetUserInfoHandler : IRequestHandler<GetUserInfoRequest,UserDTO>
    {
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;
        public GetUserInfoHandler(IMapper mapper, IServiceProvider serviceProvider)
        {
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        public async Task<UserDTO> Handle(GetUserInfoRequest request, CancellationToken cancellationToken)
        {
            using var userManager = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var user = await userManager.FindByIdAsync(request.id);
            var claims = await userManager.GetClaimsAsync(user);
            if(claims.Any(c=>c.Type=="tenant" && c.Value == "true")){
                return _mapper.Map<UserDTO>(user as Tenant);
            }
            else
            {
                return _mapper.Map<UserDTO>(user as Landlord);
            }
            
        }
    }
}
