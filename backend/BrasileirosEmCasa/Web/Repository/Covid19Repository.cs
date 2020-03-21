using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Web.Model;

namespace Web.Repository
{
    public class Covid19Repository : ICovid19Repository
    {
        public IEnumerable<Covid19PerState> GetCovidInBrazilPerState()
        {
            var data = this.MakeRequest("https://raw.githubusercontent.com/wcota/covid19br/master/cases-brazil-states.csv");
            return data;
        }

        public IEnumerable<Covid19PerState> MakeRequest(string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            try
            {
                using (HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (Stream stream = httpWebReponse.GetResponseStream())
                    {

                        StreamReader reader = new StreamReader(stream);
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            var records = csv.GetRecords<Covid19PerState>().ToList();
                            return records;
                        }
                    }
                }
            }
            catch
            {
                return null;
            }

        }


    }
}
