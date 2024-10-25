using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Application.Dto
{
    public class RentDTO
    {
        public DateTime DateFrom;
        public DateTime DateTo;
        public int HallId;
        public string? TenantId;
        public string? TenantName;
        public string? LandlordId;
        public string? LandlordName;
        public string? HallName;
        public ImageData? PreviewImage;
        public bool IsDone = false;
        public bool IsReviewed = false;
        public int RentBorderId;
    }
}
