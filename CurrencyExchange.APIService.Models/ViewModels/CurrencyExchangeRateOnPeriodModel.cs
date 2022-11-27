
namespace CurrencyExchange.APIService.Models.ViewModels
{
    public class CurrencyExchangeRateOnPeriodModel
    {
        public string? SourceCurrencyCode { get; set; }
        public string? TargetCurrencyCode { get; set; }

        public DateTime RecordedON { get; set; }
        //public Dictionary<DateTime, Dictionary<string, decimal>> ExchangeRates { get; set; }
        public decimal ExchangeRates { get; set; }
    }
}
