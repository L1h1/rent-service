using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentAGym.Domain.Entities;

namespace RentAGym.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryBase<Landlord> LandLordRepository { get; }
        IRepositoryBase<Tenant> TenantRepository { get; }
        IRepositoryBase<Facility> FacilityRepository { get; }
        IRepositoryBase<Hall> HallRepository { get; }
        IRepositoryBase<HallType> HallTypeRepository { get; }
        IRepositoryBase<ImageData> ImageRepository { get; }
        IRepositoryBase<Option> OptionRepository { get; }
        IRepositoryBase<Region> RegionRepository { get; }
        IRepositoryBase<Review> ReviewRepository { get; }
        IRepositoryBase<ReservedSchedule> ReservedScheduleRepository { get; }
        IRepositoryBase<WorkSchedulePiece> WorkScheduleRepository { get; }

        IRepositoryBase<ChatMessage> ChatMessageRepository { get; } 
    }
}
