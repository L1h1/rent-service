using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Domain.Entities
{
    public class Tenant:IdentityUser
    {
        public DateTime DateOfRegistration { get; set; }

    }
}
