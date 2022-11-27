using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace CurrencyExchange.APIService.Models.ViewModels
{
    public class CurrencyExchangeRateModel
    {
        [JsonProperty("base")]
        public string? SourceCurrencyCode { get; set; }

        [JsonProperty("rates")]
        public Dictionary<string, string>? ExchangeRates { get; set; }

        [JsonProperty("date")]
        [DataType(DataType.Date)]
        public DateTime RecordedOn { get; set; }
    }
}
