namespace CurrencyExchange.APIService.DataAccess
{
    public class Constants
    {
        public const string ApiKey = "apikey";
        public const string ApiKeyValue = "x6mZ63yFan2RNtnXyykdKek4C9FIIImM";
        public const string baseURI = "https://api.apilayer.com";
        public const string ExternalApiLatestRateRoute = "/fixer/latest?symbols={0}&base={1}";
        public const string ExternalApiGivenDateRateRoute = "/fixer/{0}?symbols={1}&base={2}";
    }
}
