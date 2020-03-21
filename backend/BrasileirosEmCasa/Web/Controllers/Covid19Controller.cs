using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Repository;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Covid19Controller : ControllerBase
    {
        public ICovid19Repository Covid19Repository { get; set; }

        public Covid19Controller(ICovid19Repository covid19Repository)
        {
            this.Covid19Repository = covid19Repository;
        }

        [Route("map")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = this.Covid19Repository.GetCovidInBrazilPerState();

            if (data == null)
                return await Task.FromResult(Ok());

            var perEstado = (from x in data
                             group x by x.Date into g
                             select new
                             {
                                 Date = $"{g.Key.ToString().ToLower()}",
                                 PerState = (from y in g
                                             group y by y.State into s
                                             select new
                                             {
                                                 UF = $"br-{s.Key.ToString().ToLower()}",
                                                 Total = s.Select(t => t.TotalCase)
                                             })
                             }).OrderBy(x =>
                             {
                                 DateTime dateTimeInput;
                                 DateTime.TryParseExact(x.Date, "yyyy-MM-dd", new CultureInfo("pt-br"), DateTimeStyles.None, out dateTimeInput);
                                 return dateTimeInput;
                             });

            return await Task.FromResult(Ok(new
            {
                Map = perEstado.LastOrDefault()
            }));

        }

    }
}