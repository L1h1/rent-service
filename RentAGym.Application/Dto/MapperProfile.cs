
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.Dto
{
    internal class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Hall, HallListRequestDTO>()
                .ForMember(dest => dest.Thumbnail,
                            dto => dto.MapFrom(h => h.Images.FirstOrDefault()))
                 .ForMember(dest => dest.Price,
                            dto => dto.MapFrom(h => h.BasePrice));
            

            CreateMap<Hall, HallDetailsRequestDTO>()
                .IncludeMembers(h => h.Facility, h => h.Facility.Landlord)
                 .ForMember(dest => dest.Price,
                            dto => dto.MapFrom(h => h.BasePrice));
            CreateMap<Facility, HallDetailsRequestDTO>();
            CreateMap<Landlord, HallDetailsRequestDTO>();

            CreateMap<CreateHallRequestDTO, Hall>()
                .ForMember(dest => dest.BasePrice,
                dto => dto.MapFrom(h => h.Price));


            CreateMap<IdentityUser, UserDTO>();
            CreateMap<Tenant, UserDTO>();
            CreateMap<Landlord, UserDTO>();

            CreateMap<CreateFacilityRequestDTO, Facility>();

            CreateMap<EditHallRequestDTO, Hall>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        }
    }



}
