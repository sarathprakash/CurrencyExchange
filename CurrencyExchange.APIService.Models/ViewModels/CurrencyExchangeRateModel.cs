using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.APIService.Models.ViewModels
{
    public class CurrencyExchangeRateModel
    {
        [JsonProperty("base")]
        public string SourceCurrencyCode { get; set; }

        [JsonProperty("rates")]
        public Dictionary<string, string> ExchangeRates { get; set; }

        [JsonProperty("date")]
        [DataType(DataType.Date)]
        public DateTime RecordedOn { get; set; }
    }
}
