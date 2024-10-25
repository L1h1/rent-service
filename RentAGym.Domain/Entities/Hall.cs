using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Domain.Entities
{
    /// <summary>
    /// Зал
    /// </summary>
    public class Hall : Entity
    {
        /// <summary>
        /// Площадь, м2
        /// </summary>
        public double Area { get; set; }
        /// <summary>
        /// Стоимость аренды в час
        /// </summary>
        public double BasePrice { get; set; }
        /// <summary>
        /// Доступные варианты аренды
        /// </summary>
        public bool Payment { get; set; }
        /// <summary>
        /// Альбом фотографий
        /// </summary>
        public List<ImageData> Images { get; set; }    = new List<ImageData>();
        public List<WorkSchedulePiece> WorkSchedule { get; set; } = new List<WorkSchedulePiece>();

        public string Description {  get; set; }

        public double OverallRating { get; set; } = 0;
        public int ReviewCount { get; set; } = 0;

        //навигационные свойства
        public int FacilityId { get; set; }
        public Facility Facility { get; set; }

        public int HallTypeId {  get; set; }
        public HallType HallType { get; set; }

        public List<Review>? Reviews { get; set; } = new List<Review>();

        /// <summary>
        /// Доп. опции и тех. наполнение
        /// </summary>
        public List<Option>? Options { get; set; }=new List<Option>();
    
        /// <summary>
        /// Список зарезервированных окон
        /// </summary>
        public List<ReservedSchedule>? ReservedSchedules { get; set; } =new List<ReservedSchedule>();

        public string LandlordId { get; set; }
        public Landlord? Landlord { get; set; }
    
    }
}
