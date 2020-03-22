using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Web.Repository;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Covid19Controller : ControllerBase
    {
        private ICovid19Repository Covid19Repository { get; set; }

        private IMemoryCache MemoryCache { get; set; }

        public Covid19Controller(ICovid19Repository covid19Repository, IMemoryCache memoryCache)
        {
            this.Covid19Repository = covid19Repository;
            this.MemoryCache = memoryCache;
        }

        [Route("map")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await this.MemoryCache.GetOrCreateAsync<IActionResult>($"{nameof(Covid19Controller)}_${nameof(Covid19Controller.Get)}", async (e) =>
             {
                 e.AbsoluteExpiration = DateTime.Now.AddMinutes(30);

                 var data = await this.Covid19Repository.GetCovidInBrazilPerState();

                 if (data == null)
                     return await Task.FromResult(Ok());

                 var perEstado = (from x in data
                                  group x by x.Date into g
                                  select new
                                  {
                                      Date = $"{g.Key.ToString().ToLower()}",
                                      PerState = (from y in g.Where(w => w.State.ToLower() != "total")
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
             });

        }

    }
}