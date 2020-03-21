using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Model
{
    public class Covid19PerState
    {
        [Index(0)]
        public string Date { get; set; }
        
        [Index(1)]
        public string Country { get; set; }
        
        [Index(2)]
        public string State { get; set; }
        
        [Index(3)]
        public string City { get; set; }
        
        [Index(4)]
        public string NewCases { get; set; }
        
        [Index(5)]
        public string TotalCase { get; set; }

    }
}
