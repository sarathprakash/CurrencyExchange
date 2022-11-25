﻿
namespace CurrencyExchange.APIService.Controllers
{
    [Route("api/rates")]
    public class CurrencyExchangeRateController : Controller
    {
        private readonly ICurrencyExchangeRateRepository currencyExchangeRateRepository;
        private readonly ILogger<CurrencyExchangeRateController> logger;

        public CurrencyExchangeRateController(ICurrencyExchangeRateRepository currencyExchangeRateRepository, ILogger<CurrencyExchangeRateController> _logger)
        {
            this.currencyExchangeRateRepository = currencyExchangeRateRepository;
            logger = _logger;
        }

        [HttpGet]
        [Route("Authorize")]
        public IResult AuthorizeUser(User user)
        {
            try
            {
                logger.LogInformation("AuthorizeUser started to executing");
                string username = user.username = "Sarath";
                string password = user.password = "Test1234";

                if (username == user.username && password == user.password)
                {
                    var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier,user.username),
                new Claim(ClaimTypes.NameIdentifier,user.password)
            };

                    var token = new JwtSecurityToken
                    (
                        issuer: "https://localhost:7050/",
                        audience: "https://localhost:7050/",
                        claims: claims,
                        expires: DateTime.UtcNow.AddDays(60),
                        notBefore: DateTime.UtcNow,
                        signingCredentials: new SigningCredentials(
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DhftOS5uphK3vmCJQrexST1RsyjZBjXWRgJMFPU4")),
                            SecurityAlgorithms.HmacSha256)
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    return Results.Ok(tokenString);
                }
                else
                {
                    return Results.NotFound();
                }
            }
            catch (Exception ex)
            {
                logger.LogError("AuthorizeUser failed");
                throw new Exception(ex.Message);
            }
        }

        public class User
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        [HttpGet]
        [Route("GetCurrencyCodes")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IEnumerable<Currency> GetCurrencyCodes()
        {
            try
            {
                logger.LogInformation("GetCurrencyCodes started to executing");
                var CurrencyCodesDetails = currencyExchangeRateRepository.GetCurrencyCodes();
                return CurrencyCodesDetails;
            }
            catch (Exception ex)
            {
                logger.LogError("GetCurrencyCodes failed");
                throw new Exception(ex.Message);
            }
        }


        [HttpGet]
        [Route("GetCurrencyExchangeRate/{sourceCurrency}/{targetCurrency}/{amount}")]
        public async Task<CurrencyExchangeRateModel?> GetCurrencyExchangeRate(string sourceCurrency, string targetCurrency, long amount, DateTime? date)
        {
            try
            {
                logger.LogInformation("GetCurrencyExchangeRate started to executing");
                var exchangeRates = await currencyExchangeRateRepository.GetCurrencyExchangeRate(sourceCurrency, targetCurrency, amount, date);
                return exchangeRates;
            }
            catch (Exception ex)
            {
                logger.LogError("GetCurrencyExchangeRate failed");
                throw new Exception(ex.Message);
            }
        }

        //[HttpGet]
        //[Route("GetCurrencyExchangeRateByPeriod/{sourceCurrency}/{fromDate}/{toDate}")]
        //public CurrencyExchangeRateOnPeriodModel? GetCurrencyExchangeRateByPeriod(string sourceCurrency, DateTime fromDate, DateTime toDate)
        //{
        //    try
        //    {
        //        logger.LogInformation("GetCurrencyExchangeRateByPeriod started to executing");
        //        var result = currencyExchangeRateRepository.GetCurrencyExchangeRateByPeriod(sourceCurrency, fromDate, toDate);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError("GetCurrencyExchangeRateByPeriod failed");
        //        throw new Exception(ex.Message);
        //    }
        //}
        [HttpGet]
        [Route("GetCurrencyExchangeRateByPeriod/{sourceCurrency}/{targetCurrency}/{fromDate}/{toDate}")]
        public IEnumerable<CurrencyExchangeRateOnPeriodModel?> GetCurrencyExchangeRateByPeriod(string sourceCurrency, string targetCurrency, DateTime fromDate, DateTime toDate)
        {
            try
            {
                logger.LogInformation("GetCurrencyExchangeRateByPeriod started to executing");
                var result = currencyExchangeRateRepository.GetCurrencyExchangeRateByPeriod(sourceCurrency, targetCurrency, fromDate, toDate);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError("GetCurrencyExchangeRateByPeriod failed");
                throw new Exception(ex.Message);
            }
        }
    }
}