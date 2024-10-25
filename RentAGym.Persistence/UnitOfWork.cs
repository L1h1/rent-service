using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using RentAGym.Application.Interfaces;
using RentAGym.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private readonly Lazy<IRepositoryBase<Landlord>> _landLordRepository;
        private readonly Lazy<IRepositoryBase<Tenant>> _tenantRepository;
        private readonly Lazy<IRepositoryBase<Facility>> _facilityRepository;
        private readonly Lazy<IRepositoryBase<Hall>> _hallRepository;
        private readonly Lazy<IRepositoryBase<HallType>> _hallTypeRepository;
        private readonly Lazy<IRepositoryBase<ImageData>> _imageRepository;
        private readonly Lazy<IRepositoryBase<Option>> _optionRepository;
        private readonly Lazy<IRepositoryBase<Region>> _regionRepository;
        private readonly Lazy<IRepositoryBase<Review>> _reviewRepository;
        private readonly Lazy<IRepositoryBase<ReservedSchedule>> _scheduleRepository;
        private readonly Lazy<IRepositoryBase<WorkSchedulePiece>> _workSchedule;
        private readonly Lazy<IRepositoryBase<ChatMessage>> _chatMessages;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _landLordRepository = new Lazy<IRepositoryBase<Landlord>>(
                                            () => new EfRepository<Landlord>(context));
            _tenantRepository = new Lazy<IRepositoryBase<Tenant>>(
                                            () => new EfRepository<Tenant>(context));
            _facilityRepository = new Lazy<IRepositoryBase<Facility>>(
                ()=> new EfRepository<Facility>(context));
            _hallRepository = new Lazy<IRepositoryBase<Hall>>(
                ()=> new EfRepository<Hall>(context));
            _hallTypeRepository = new Lazy<IRepositoryBase<HallType>>(
                ()=>new EfRepository<HallType>(context));
            _imageRepository = new Lazy<IRepositoryBase<ImageData>>(
                ()=> new EfRepository<ImageData>(context));
            _optionRepository = new Lazy<IRepositoryBase<Option>>(
                ()=> new EfRepository<Option>(context));
            _regionRepository = new Lazy<IRepositoryBase<Region>>(
                ()=> new EfRepository<Region>(context));

            _reviewRepository = new Lazy<IRepositoryBase<Review>>(
                () => new EfRepository<Review>(context));
            _scheduleRepository = new Lazy<IRepositoryBase<ReservedSchedule>>(
                () => new EfRepository<ReservedSchedule>(context));
            _workSchedule = new Lazy<IRepositoryBase<WorkSchedulePiece>>(
                ()=> new EfRepository<WorkSchedulePiece>(context));
            _chatMessages = new Lazy<IRepositoryBase<ChatMessage>>(
                ()=>new EfRepository<ChatMessage>(context));
        }

        public IRepositoryBase<Landlord> LandLordRepository 
            => _landLordRepository.Value;
        public IRepositoryBase<Tenant> TenantRepository
            => _tenantRepository.Value;
        public IRepositoryBase<Facility> FacilityRepository
            => _facilityRepository.Value;
        public IRepositoryBase<Hall> HallRepository
            => _hallRepository.Value;
        public IRepositoryBase<HallType> HallTypeRepository
            => _hallTypeRepository.Value;
        public IRepositoryBase<ImageData> ImageRepository
            => _imageRepository.Value;
        public IRepositoryBase<Option> OptionRepository
            => _optionRepository.Value;
        public IRepositoryBase<Region> RegionRepository
            => _regionRepository.Value;

        public IRepositoryBase<Review> ReviewRepository => _reviewRepository.Value;

        public IRepositoryBase<ReservedSchedule> ReservedScheduleRepository => _scheduleRepository.Value;

        public IRepositoryBase<WorkSchedulePiece> WorkScheduleRepository => _workSchedule.Value;

        public IRepositoryBase<ChatMessage> ChatMessageRepository => _chatMessages.Value;
    }
}
