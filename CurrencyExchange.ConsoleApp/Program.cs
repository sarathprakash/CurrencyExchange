

DateTime? date = null;
if (args.Length == 0)
    return;

Connection dbConnection = new Connection();
var serviceProvider = new ServiceCollection()
     .AddLogging((loggingBuilder) => loggingBuilder
        .SetMinimumLevel(LogLevel.Trace)
        .AddConsole())
        .AddSingleton(dbConnection.InitializeDb())
        .AddSingleton<ICurrencyExchangeRateRepository, CurrencyExchangeRateRepository>()
        .BuildServiceProvider();

var repository = serviceProvider.GetService<ICurrencyExchangeRateRepository>();
var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();

var operationType = args[0];
if (operationType == "GET") //Get currency exchange rate
{
    var sCode = args[1];
    var tCode = args[2];
    var amount = Convert.ToInt64(args[3]);
    if (args.Length > 4)
    {
        date = Convert.ToDateTime(args[4]);
    }
    try
    {
        var model = await repository.GetCurrencyExchangeRate(sCode, tCode, amount, date);
        var result = JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.Indented);
        Console.WriteLine($"Exchange Rate are \n {result}");

        foreach (var item in model.ExchangeRates)
        {
            logger.LogInformation($"{amount} {sCode} equals {item.Value} {item.Key}  ");
        }
        Console.ReadKey();
    }
    catch (Exception ex)
    {
        Console.WriteLine("GET operation failed : \n\t" + ex);
    }
}
else if (operationType == "SAVE")  //Save currency exchange rates
{
    bool result = false;
    var sCode = args[1];
    var tCode = args[2];
    try
    {
        result = await repository.SaveCurrencyExchangeRate(sCode, tCode);
        logger.LogInformation($"Saved latest exchange rate of 1 {sCode} to {tCode} at {DateTime.Now}");
    }
    catch (Exception ex)
    {
        Console.WriteLine("save operation failed : \n\t" + ex);
    }
    if (result)
        Console.WriteLine($"Saved latest exchange rate of 1 {sCode} to {tCode} at {DateTime.Now}");
    else
        Console.WriteLine("Exchange rates creation failed - nothing got updated");
}
