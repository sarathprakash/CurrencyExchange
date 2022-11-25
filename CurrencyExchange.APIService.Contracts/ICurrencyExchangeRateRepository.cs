using CurrencyExchange.APIService.Models;
using CurrencyExchange.APIService.Models.ViewModels;

namespace CurrencyExchange.APIService.Contracts
{
    public interface ICurrencyExchangeRateRepository
    {
        Task<CurrencyExchangeRateModel?> GetCurrencyExchangeRate(string sourceCurrencyCode, string targetCurrencyCode, long amount, DateTime? date);
        //CurrencyExchangeRateOnPeriodModel? GetCurrencyExchangeRateByPeriod(string sourceCurrencyCode, DateTime fromDate, DateTime toDate);

        List<CurrencyExchangeRateOnPeriodModel?> GetCurrencyExchangeRateByPeriod(string sourceCurrencyCode, string targetCurrencyCode, DateTime fromDate, DateTime toDate);
        Task<bool> SaveCurrencyExchangeRate(string sourceCurrencyCode, string targetCurrencyCode);
        List<Currency> GetCurrencyCodes();
        List<UserDetails> GetUserDetails();
    }
}