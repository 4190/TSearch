using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TSearch.Models;

namespace TSearch.Data.EFCore
{
    public class EfCoreAdvertRepository : EfCoreRepository<Advert, ApplicationDbContext>
    {
        public EfCoreAdvertRepository(ApplicationDbContext context) : base(context)
        {

        }
        public override async Task<Advert> Get(int id)
        {
            return await context.Adverts.Include(x => x.GameCharacter).FirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<List<Advert>> GetAll()
        {
            return await context.Adverts.Include(x => x.GameCharacter).ToListAsync();
        }

        public Advert GetByGameCharacterId(int id)
        {
            return context.Adverts.SingleOrDefault(x => x.GameCharacterId == id);
        }
    }
}
