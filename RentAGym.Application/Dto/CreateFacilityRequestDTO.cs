namespace RentAGym.Application.Dto
{
    public class CreateFacilityRequestDTO
    {
        public string Name { get; set; }
        // Адрес
        public int RegionId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public double Latitude { get; set; } //= 0;       
        public double Longitude { get; set; }// = 0
        public string LandLordId { get; set; }
    }
}
