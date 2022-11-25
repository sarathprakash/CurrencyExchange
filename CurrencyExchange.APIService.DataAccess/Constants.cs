using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.APIService.DataAccess
{
    public class Constants
    {
        public const string ApiKey = "apikey";
        public const string ExternalApiLatestRateRoute = "/fixer/latest?symbols={0}&base={1}";
        public const string ExternalApiGivenDateRateRoute = "/fixer/{0}?symbols={1}&base={2}";
    }
}
