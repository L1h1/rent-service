using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Domain.Entities
{
    //Дополнительные параметры зала
    //Душ, зеркала, раздевалки и т.д.
    public class Option :Entity
    {     
        public List<Hall> Halls { get; set; } = new List<Hall>();
    }
}
