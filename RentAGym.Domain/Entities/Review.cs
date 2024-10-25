using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Domain.Entities
{
    /// <summary>
    /// Отзыв об арендованном помещении
    /// </summary>
    public class Review : Entity    
    {
        /// <summary>
        /// Оценка клиента
        /// </summary>
        public int Mark {  get; set; }
        /// <summary>
        /// Содержимое отзыва
        /// </summary>
        public string Username {  get; set; }
        public string Contents {  get; set; }
        
        //Навигационные свойства
        public int HallId {  get; set; }
        public Hall? Hall { get; set; }
    }
}
