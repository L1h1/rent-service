using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Domain.Entities
{
    //описывает область (Минская, Брестская ... 
    public class Region : Entity
    {
        public List<Facility> Objects { get; set; } = new List<Facility>();
    }
}
