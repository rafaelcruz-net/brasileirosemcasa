using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NHibernate.Linq;
using Web.Repository;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private IEstadoRepository EstadoRepository { get; set; }

        private IMemoryCache MemoryCache { get; set; }

        public EstadoController(IEstadoRepository estadoRepository, IMemoryCache memoryCache)
        {
            this.EstadoRepository = estadoRepository;
            this.MemoryCache = memoryCache;
        }


        public async Task<IActionResult> GetEstados()
        {
            return await this.MemoryCache.GetOrCreateAsync<IActionResult>($"{nameof(EstadoController)}_${nameof(EstadoController.GetEstados)}", async (e) =>
            {
                e.AbsoluteExpiration = DateTime.Now.AddDays(1);
                return Ok(await this.EstadoRepository.GetAll());
            });
        }
        [Route("cidade/{uf}")]
        public async Task<IActionResult> GetCidade(string uf)
        {
            return await this.MemoryCache.GetOrCreateAsync<IActionResult>($"{nameof(EstadoController)}_${nameof(EstadoController.GetCidade)}_${uf}", async (e) =>
            {
                e.AbsoluteExpiration = DateTime.Now.AddDays(1);

                return Ok(await this.EstadoRepository.Query.Where(x => x.UF == uf).SelectMany(x => x.Cidades).ToListAsync());
                
            });
        }

    }
}