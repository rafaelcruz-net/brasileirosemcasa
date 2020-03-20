using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Repository;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        public IEstadoRepository EstadoRepository { get; set; }

        public EstadoController(IEstadoRepository estadoRepository)
        {
            this.EstadoRepository = estadoRepository;
        }


        public async Task<IActionResult> GetEstados()
        {
            return Ok(await this.EstadoRepository.GetAll());
        }
    }
}