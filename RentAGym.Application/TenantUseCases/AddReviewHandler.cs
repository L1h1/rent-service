using RentAGym.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.TenantUseCases
{
    internal class AddReviewHandler : IRequestHandler<AddReviewRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddReviewHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(AddReviewRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.ReviewRepository.AddAsync(request.review);
                var hall = await _unitOfWork.HallRepository.GetByIdAsync(request.review.HallId);

                hall.OverallRating = (hall.OverallRating * hall.ReviewCount++ + request.review.Mark) / hall.ReviewCount;
                await _unitOfWork.HallRepository.UpdateAsync(hall);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
