using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TSearch.Data;
using TSearch.Data.EFCore;
using TSearch.DTO;
using TSearch.Models;

namespace TSearch.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertsController : TSearchController<Advert, EfCoreAdvertRepository>
    {
        public AdvertsController(EfCoreAdvertRepository repository) : base(repository)
        {

        }
    }
}
