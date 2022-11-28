
namespace CurrencyExchange.APIService.Controllers
{
    [Route("api/rates")]
    public class CurrencyExchangeRateController : Controller
    {
        private readonly ICurrencyExchangeRateRepository _currencyExchangeRateRepository;
        private readonly ILogger<CurrencyExchangeRateController> _logger;

        public CurrencyExchangeRateController(ICurrencyExchangeRateRepository currencyExchangeRateRepository, ILogger<CurrencyExchangeRateController> logger)
        {
            _currencyExchangeRateRepository = currencyExchangeRateRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("Authorize")]
        public IResult AuthorizeUser(UserDetails user)
        {
            try
            {
                _logger.LogInformation("AuthorizeUser started to executing");
                var userdetails= _currencyExchangeRateRepository.GetUserDetails();
                string? username = userdetails[0].Username;
                string? password = userdetails[0].Password;

                if (username == user.Username && password == user.Password)
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.Username),
                        new Claim(ClaimTypes.NameIdentifier,user.Password)
                    };

                    var Config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Jwt");
                    var token = new JwtSecurityToken
                    (
                        issuer: Config["Issuer"],
                        audience: Config["Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddDays(60),
                        notBefore: DateTime.UtcNow,
                        signingCredentials: new SigningCredentials(
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Key"])),
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
                _logger.LogError("AuthorizeUser failed");
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetCurrencyCodes")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IEnumerable<Currency> GetCurrencyCodes()
        {
            try
            {
                _logger.LogInformation("GetCurrencyCodes started to executing");
                var CurrencyCodesDetails = _currencyExchangeRateRepository.GetCurrencyCodes();
                return CurrencyCodesDetails;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetCurrencyCodes failed");
                throw new Exception(ex.Message);
            }
        }


        [HttpGet]
        [Route("GetCurrencyExchangeRate/{sourceCurrency}/{targetCurrency}/{amount}")]
        public async Task<CurrencyExchangeRateModel?> GetCurrencyExchangeRate(string sourceCurrency, string targetCurrency, long amount)
        {
            try
            {
                _logger.LogInformation("GetCurrencyExchangeRate started to executing");
                var exchangeRates = await _currencyExchangeRateRepository.GetCurrencyExchangeRate(sourceCurrency, targetCurrency, amount,null);
                return exchangeRates;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetCurrencyExchangeRate failed");
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetCurrencyExchangeRateByDate/{sourceCurrency}/{targetCurrency}/{amount}/{date}")]
        public async Task<CurrencyExchangeRateModel?> GetCurrencyExchangeRateByDate(string sourceCurrency, string targetCurrency, long amount, DateTime? date)
        {
            try
            {
                _logger.LogInformation("GetCurrencyExchangeRateByDate started to executing");
                var exchangeRates = await _currencyExchangeRateRepository.GetCurrencyExchangeRate(sourceCurrency, targetCurrency, amount, date);
                return exchangeRates;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetCurrencyExchangeRateByDate failed");
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetCurrencyExchangeRateByPeriod/{sourceCurrency}/{targetCurrency}/{fromDate}/{toDate}")]
        public IEnumerable<CurrencyExchangeRateOnPeriodModel?> GetCurrencyExchangeRateByPeriod(string sourceCurrency, string targetCurrency, DateTime fromDate, DateTime toDate)
        {
            try
            {
                _logger.LogInformation("GetCurrencyExchangeRateByPeriod started to executing");
                var result = _currencyExchangeRateRepository.GetCurrencyExchangeRateByPeriod(sourceCurrency, targetCurrency, fromDate, toDate);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetCurrencyExchangeRateByPeriod failed");
                throw new Exception(ex.Message);
            }
        }
    }
}
