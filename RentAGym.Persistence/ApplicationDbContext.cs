using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentAGym.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Persistence
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        {
            
        }

        public DbSet<Landlord> LandLords {  get; set; }
        public DbSet<Tenant> Tenants { get; set;}
        public DbSet<Hall> Halls { get; set; }
        public DbSet<HallType> HallTypes { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<ImageData> Images { get; set; }
        public DbSet<ReservedSchedule> ReservedSchedules { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

    }
}
