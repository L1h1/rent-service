using MediatR;
using RentAGym.Application.Dto;
using RentAGym.Application.Filters;
using RentAGym.Domain.Entities;


namespace RentAGym.Application.CommonUseCases
{
    //public sealed record GetHallListRequest(
    //    int typeId = -1,
    //    bool paymentType = false,
    //    int regionId = -1,
    //    double squareFrom = 0,
    //    double squareTo = double.MaxValue,
    //    double priceFrom = 0,
    //    double priceTo = double.MaxValue,
    //    List<int>? optionIds = null) : IRequest<IEnumerable<HallListRequestDTO>>
    //{
    //}

    public sealed record GetHallListRequest(
            HallListFilter filter) : IRequest<IEnumerable<HallListRequestDTO>>
    {
    }
}
