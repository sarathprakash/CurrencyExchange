namespace CurrencyExchange.Xunit.Test
{
    public class CurrencyExchangeTest
    {
        [Fact]
        public async Task GetCurrecyExchangeRate()
        {
            try
            {
                CurrencyExchangeRateRepository currencyExchangeRateRepository = new CurrencyExchangeRateRepository(InitializeDb());
                string sourceCurrency = "USD";
                string targerCurrency = "INR";
                long amount = 100;
                DateTime date = DateTime.Now;
                var result = await currencyExchangeRateRepository.GetCurrencyExchangeRate(sourceCurrency, targerCurrency, amount, date);
                Assert.Single(result.ExchangeRates);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Fact]
        public async Task SaveCurrencyExchange()
        {
            try
            {
                CurrencyExchangeRateRepository currencyExchangeRateRepository = new CurrencyExchangeRateRepository(InitializeDb());
                string sourceCurrency = "USD";
                string targetCurrency = "INR";
                var result = await currencyExchangeRateRepository.SaveCurrencyExchangeRate(sourceCurrency, targetCurrency);
                Assert.True(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private IDbConnection InitializeDb()
        {
            SqlConnection connection = new SqlConnection("Server= SARATHPRAKASH; Database= DB_CurrencyExchange; Integrated Security=True;");
            connection.Open();
            return connection;
        }
    }
}
