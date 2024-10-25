using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Domain.Entities
{
    /// <summary>
    /// Период, когда помещение зарезервировано 
    /// </summary>
    public class ReservedSchedule
    {
        public int Id { get; set; }
        /// <summary>
        /// Дата и время окончания аренды
        /// </summary>
        public DateTime ReservedHour { get; set; }
        /// <summary>
        /// Является ли аренда регулярной (прим. каждую неделю)
        /// </summary>
        public bool IsRegular { get; set; } = false;

        /// <summary>
        /// Сделано для разграничения записей, указывает границы одного промежутка
        /// </summary>
        public bool IsBorder { get;set; } = false;

        //Навигационные свойства
        public int HallId { get; set; }
        public Hall Hall { get; set; }

        public string TenantId { get; set; }
        
    }
}
