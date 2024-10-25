using RentAGym.Application.Dto;


namespace RentAGym.Application.CommonUseCases
{
    public sealed record GetHallByIdRequest (int id) :IRequest<HallDetailsRequestDTO>
    {
    }
}
