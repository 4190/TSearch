using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TSearch.Data;

namespace TSearch.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class TSearchController<TEntity, TRepository> : ControllerBase
        where TEntity : class, IEntity
        where TRepository : IRepository<TEntity>
    {
        private readonly TRepository repository;

        public TSearchController(TRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            return await repository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            var advert = await repository.Get(id);
            if (advert == null)
            {
                return NotFound();
            }
            return advert;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TEntity advert)
        {
            if (id != advert.Id)
            {
                return BadRequest();
            }
            await repository.Update(advert);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(int id)
        {
            var advert = await repository.Delete(id);
            if (advert == null)
            {
                return NotFound();
            }
            return advert;
        }
    }
}
