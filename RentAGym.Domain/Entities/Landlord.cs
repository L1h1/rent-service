using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Domain.Entities
{
    /// <summary>
    /// Арендодатель
    /// </summary>
    public class Landlord : IdentityUser
    {
        public string Name { get; set; }    
        public DateTime DateOfRegistration { get; set; }
        public List<Facility> Facilities { get; set; } = new List<Facility>();
    }
}
