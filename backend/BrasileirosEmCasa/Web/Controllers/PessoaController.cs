using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Web.Model;
using Web.Repository;
using Web.ViewModel;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private IPessoaRepository PessoaRepository { get; set; }

        private IConfiguration Configuration { get; }

        private ICovid19Repository Covid19Repository { get; set; }

        public PessoaController(IPessoaRepository pessoaRepository, IConfiguration configuration, ICovid19Repository covid19Repository)
        {
            this.PessoaRepository = pessoaRepository;
            this.Configuration = configuration;
            this.Covid19Repository = covid19Repository;
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
            var covid = (await this.Covid19Repository.GetCovidInBrazilPerState()).LastOrDefault();

            DateTime dateTimeInput;
            DateTime.TryParseExact(covid.Date, "yyyy-MM-dd", new CultureInfo("pt-br"), DateTimeStyles.None, out dateTimeInput);

            return await Task.FromResult(Ok(new
            {
                Count = count,
                TotalCase = covid.TotalCase,
                NewCases = new
                {
                    Total = covid.NewCases,
                    Data = (dateTimeInput == null ? DateTime.Now.ToString("dd/MM/yyyy") : dateTimeInput.ToString("dd/MM/yyyy"))
                }

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

        [Route("CounterByStateAndMonth")]
        [HttpGet]
        public async Task<IActionResult> GetQuantidadePessoaEmCasaPorEstadoEMes()
        {
            List<dynamic> GetValorAgrupador(List<dynamic> list, List<dynamic> estates)
            {
                List<dynamic> result = new List<dynamic>();

                estates.ForEach(e =>
                {
                    var value = list.FirstOrDefault(x => x.Nome == e);

                    if (value == null)
                    {
                        result.Add(new
                        {
                            Total = 0,
                            Nome = e
                        });
                    }
                    result.Add(value);
                });

                return result;
            }

            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("ConnectionString")))
            {
                var command = @"SELECT sum(QuantidadePessoasEmCasa) as Total, DATEPART(Year, DataInicioQuarentena) Ano, DATEPART(Month, DataInicioQuarentena) Mes, e.Nome  FROM PESSOA p
                                INNER JOIN CIDADE c on p.idcidade = c.id
	                            INNER JOIN ESTADO e on e.id = c.uf
                                WHERE DATEPART(Year, DataInicioQuarentena) >= 2020
                                AND DATEPART(Month, DataInicioQuarentena) >= 3
                                AND e.id <> 99
                                GROUP BY DATEPART(Year, DataInicioQuarentena), DATEPART(Month, DataInicioQuarentena), e.Nome
                                Order by DATEPART(Year, DataInicioQuarentena), DATEPART(Month, DataInicioQuarentena), e.Nome";

                var resultQuery = connection.Query(command);
                var estates = resultQuery.ToList().Select(x => x.Nome).Distinct().OrderBy(x => x).ToList();

                resultQuery = (from x in resultQuery
                               group x by new { x.Ano, x.Mes } into g
                               select new
                               {
                                   Periodo = $"{g.Key.Mes.ToString().PadLeft(2, '0')}/{g.Key.Ano}",
                                   Valor = GetValorAgrupador(g.ToList(), estates).Where(x => x != null).Select(m => new
                                   {
                                       Total = Convert.ToInt32(m.Total),
                                       Nome  = m.Nome
                                   })
                               });

                return await Task.FromResult(Ok(new
                {
                    Result = resultQuery.ToList(),
                    Category = estates

                })) ;

            }
        }

        
    }
}