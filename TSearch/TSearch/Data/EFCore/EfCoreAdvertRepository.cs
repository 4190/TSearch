using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSearch.Models;

namespace TSearch.Data.EFCore
{
    public class EfCoreAdvertRepository : EfCoreRepository<Advert, ApplicationDbContext>
    {
        public EfCoreAdvertRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
