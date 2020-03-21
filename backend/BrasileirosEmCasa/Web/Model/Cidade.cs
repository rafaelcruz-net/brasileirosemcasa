using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Web.Model
{
    public class Cidade
    {
        public virtual int Id { get; set; }
        public virtual String Nome { get; set; }

        [JsonIgnore]
        public virtual Estado Estado { get; set; }
    }
}
