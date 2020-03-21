using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModel
{
    public class PessoaRequest
    {
        [Required]
        public String DataInicioQuarentena { get; set; }

        [Required]
        public int QuantidadePessoasCasa { get; set; }

        [Required]
        public int Cidade { get; set; }
    }
}
