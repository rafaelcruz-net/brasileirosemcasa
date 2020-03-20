using System;
using System.Collections.Generic;
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


            using (var transaction = this.PessoaRepository.CreateTransaction())
            {
                try
                {
                    var pessoa = new Pessoa();
                    pessoa.QuantidadePessoasCasa = pessoaRequest.QuantidadePessoasCasa;
                    pessoa.Cidade = new Cidade() { Id = pessoaRequest.IdCidade };
                    pessoa.DataInicioQuarentena = pessoaRequest.DataInicioQuarentena;

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

    }
}