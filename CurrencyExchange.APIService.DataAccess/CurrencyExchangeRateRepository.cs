using Dapper;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using CurrencyExchange.APIService.Models.ViewModels;
using CurrencyExchange.APIService.Models;
using CurrencyExchange.APIService.Contracts;
using CurrencyExchange.APIService.DataAccess.Helpers;

namespace CurrencyExchange.APIService.DataAccess
{
    public class CurrencyExchangeRateRepository : ICurrencyExchangeRateRepository
    {
        private readonly IDbConnection _connection;

        public CurrencyExchangeRateRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<CurrencyExchangeRateModel?> GetCurrencyExchangeRate(string sourceCurrencyCode, string targetCurrencyCode, long amount, DateTime? date)
        {
            try
            {
                // Call API here
                string baseURI = "https://api.apilayer.com";
                string requestParam = date == null ? string.Format(Constants.ExternalApiLatestRateRoute, targetCurrencyCode, sourceCurrencyCode)
                    : string.Format(Constants.ExternalApiGivenDateRateRoute, date.Value.ToString("yyyy-MM-dd"), targetCurrencyCode, sourceCurrencyCode);

                string responseBody = HttpClientHelper.DoHttpRequest("GET", baseURI, requestParam);

                var currencyExchangeRateModel = JsonConvert.DeserializeObject<CurrencyExchangeRateModel>(responseBody);
                if (currencyExchangeRateModel == null)
                    return null;

                var keys = currencyExchangeRateModel.ExchangeRates.Keys;
                foreach (var key in keys)
                {
                    currencyExchangeRateModel.ExchangeRates[key] = Convert.ToString(Convert.ToDecimal(currencyExchangeRateModel.ExchangeRates[key]) * amount);
                }

                return currencyExchangeRateModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //public CurrencyExchangeRateOnPeriodModel? GetCurrencyExchangeRateByPeriod(string sourceCurrencyCode, DateTime fromDate, DateTime toDate)
        //{
        //    try
        //    {
        //        var sql = $"SELECT CER.*, SC.Code as SourceCurrencyCode, TC.Code as TargetCurrencyCode  " +
        //                $"FROM CurrencyExchangeRates CER " +
        //                $"INNER JOIN Currency SC ON SC.Id = CER.SourceCurrencyId AND SC.Code = '{sourceCurrencyCode}' " +
        //                $"INNER JOIN Currency TC  ON TC.Id = CER.TargetCurrencyId " +
        //                $"AND CER.RecordedOn BETWEEN '{fromDate.ToString("yyyy-MM-dd")}' AND '{toDate.ToString("yyyy-MM-dd")}'";

        //        var currencyExchangeRate = _connection.Query<CurrencyExchangeRate>(sql);

        //        var result = currencyExchangeRate.GroupBy(x => new { x.SourceCurrencyId, x.SourceCurrencyCode })
        //            .Select(x => new CurrencyExchangeRateOnPeriodModel
        //            {
        //                SourceCurrencyCode = x.Key.SourceCurrencyCode,
        //                StartDate = fromDate,
        //                EndDate = toDate,
        //                ExchangeRates = x.ToList().GroupBy(g => new { g.RecordedOn })
        //                                          .ToDictionary(v => v.Key.RecordedOn,
        //                                                        v => v.GroupBy(z => z.TargetCurrencyCode)
        //                                                              .Select(t => t.OrderByDescending(r => r.RecordedOn).FirstOrDefault())
        //                                                              .ToDictionary(d => d.TargetCurrencyCode, d => d.TargetCurrencyExchangeRate))
        //            }).FirstOrDefault();

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }

        //}
        public List<CurrencyExchangeRateOnPeriodModel?> GetCurrencyExchangeRateByPeriod(string sourceCurrencyCode, string targetCurrencyCode, DateTime fromDate, DateTime toDate)
        {
            try
            {
                var sql = $"SELECT SC.Code as SourceCurrencyCode, TC.Code as TargetCurrencyCode , CER.TargetCurrencyExchangerate as ExchangeRates,CER.RecordedON " +
                    $"FROM CurrencyExchangeRates CER " +
                    $"INNER JOIN Currency SC ON SC.Id = CER.SourceCurrencyId AND SC.Code = '{sourceCurrencyCode}'" +
                    $"INNER JOIN Currency TC  ON TC.Id = CER.TargetCurrencyId  AND TC.Code = '{targetCurrencyCode}' " +
                    $"AND CER.RecordedOn BETWEEN '{fromDate.ToString("yyyy-MM-dd")}' AND '{toDate.ToString("yyyy-MM-dd")}'";

                var currencyExchangeRate = _connection.Query<CurrencyExchangeRateOnPeriodModel>(sql);

                var result = currencyExchangeRate.GroupBy(x => x.RecordedON).Select(x => x.OrderByDescending(y => y.RecordedON).FirstOrDefault()).Select(x => new CurrencyExchangeRateOnPeriodModel
                {
                    SourceCurrencyCode = x.SourceCurrencyCode,
                    TargetCurrencyCode = x.TargetCurrencyCode,
                    RecordedON = Convert.ToDateTime(x.RecordedON),
                    ExchangeRates = x.ExchangeRates
                });
                return result.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<bool> SaveCurrencyExchangeRate(string sourceCurrencyCode, string targetCurrencyCode)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                var currencyExchangeRate = await GetCurrencyExchangeRate(sourceCurrencyCode, targetCurrencyCode, 1, null);

                var keys = currencyExchangeRate.ExchangeRates.Keys;
                foreach (var key in keys)
                {
                    var targetCurrExRate = currencyExchangeRate.ExchangeRates[key];

                    var sql = $"INSERT INTO dbo.CurrencyExchangeRates(SourceCurrencyId, TargetCurrencyId, TargetCurrencyExchangeRate, RecordedOn) " +
                            $"SELECT SC.Id, TC.Id, {targetCurrExRate}, '{DateTime.UtcNow.ToString("yyyy-MM-dd")}' " +
                            $"FROM Currency SC,Currency TC " +
                            $"WHERE SC.Code = '{sourceCurrencyCode}' AND TC.Code = '{key}' \n";

                    sb.AppendLine("\n" + sql);
                }
                var rowsAffteced = _connection.Execute(sb.ToString());
                return rowsAffteced > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Currency> GetCurrencyCodes()
        {
            try
            {
                const string sql = "select *  from Currency";
                var gunDetails = _connection.Query<Currency>(sql);
                return gunDetails.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<UserDetails> GetUserDetails()
        {
            try
            {
                const string sql = "select *  from UserDetails";
                var userDetails = _connection.Query<UserDetails>(sql);
                return userDetails.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}