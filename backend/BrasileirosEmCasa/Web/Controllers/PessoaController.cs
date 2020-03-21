using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Model;
using Web.Repository;
using Web.ViewModel;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        public IPessoaRepository PessoaRepository { get; set; }

        public PessoaController(IPessoaRepository pessoaRepository)
        {
            this.PessoaRepository = pessoaRepository;
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PessoaRequest pessoaRequest)
        {
            if (!ModelState.IsValid)
                return await Task.FromResult<IActionResult>(BadRequest(ModelState));

            DateTime dateTimeInput;

            if (!DateTime.TryParseExact(pessoaRequest.DataInicioQuarentena, "dd/MM/yyyy", new CultureInfo("pt-br"), DateTimeStyles.None, out dateTimeInput))
            {
                return await Task.FromResult(BadRequest(new
                {
                    Error = "Data de Inicio de Quarentena Invalida"
                }));
            }

            using (var transaction = this.PessoaRepository.CreateTransaction())
            {
                try
                {
                    var pessoa = new Pessoa();
                    pessoa.QuantidadePessoasCasa = pessoaRequest.QuantidadePessoasCasa;
                    pessoa.Cidade = new Cidade() { Id = pessoaRequest.Cidade };
                    pessoa.DataInicioQuarentena = dateTimeInput;

                    await this.PessoaRepository.Save(pessoa);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }


            return await Task.FromResult(Created("", null));
        }

        [Route("counter")]
        [HttpGet]
        public async Task<IActionResult> GetCounter()
        {
            var count = this.PessoaRepository.Query.Select(x => x.QuantidadePessoasCasa)?.Sum();

            return await Task.FromResult(Ok(new
            {
                Count = count,

            }));
        }
        [Route("map")]
        [HttpGet]
        public async Task<IActionResult> GetMap()
        {
            var perEstado = (from x in this.PessoaRepository.Query
                             group x by x.Cidade.Estado.UF into g
                             select new
                             {
                                 UF = $"br-{g.Key.ToString().ToLower()}",
                                 Qtd = g.Select(x => x.QuantidadePessoasCasa).Sum()
                            });

            return await Task.FromResult(Ok(new
            {
                Map = perEstado
            }));
        }



    }
}