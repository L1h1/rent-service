using Ardalis.Specification.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Persistence
{
    public class EfRepository<T> : RepositoryBase<T> where T : class
    {
        public EfRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
    
}
