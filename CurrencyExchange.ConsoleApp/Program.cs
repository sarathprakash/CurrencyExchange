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


DateTime? date = null;
if (args.Length == 0)
{
    logger.LogError("Command line arguments are not provided ");
    return;
}
var operationType = args[0];
if (operationType == "GET") //Get currency exchange rate
{
    var sourceCode = args[1];
    var targetCode = args[2];
    var amount = Convert.ToInt64(args[3]);
    if (args.Length > 4)
    {
        date = Convert.ToDateTime(args[4]);
    }
    try
    {
        var model = await repository.GetCurrencyExchangeRate(sourceCode, targetCode, amount, date);
        var result = JsonConvert.SerializeObject(model, Formatting.Indented);
        Console.WriteLine($"Exchange Rate are  {result}");

        foreach (var item in model.ExchangeRates)
        {
            logger.LogInformation($"{amount} {sourceCode} equals {item.Value} {item.Key}  ");
        }
        Console.ReadKey();
    }
    catch (Exception ex)
    {
        Console.WriteLine("GET operation failed : " + ex);
    }
}
else if (operationType == "SAVE")  //Save currency exchange rates
{
    bool result = false;
    var sourceCode = args[1];
    var targetCode = args[2];
    try
    {
        result = await repository.SaveCurrencyExchangeRate(sourceCode, targetCode);
        logger.LogInformation($"Saved latest exchange rate of 1 {sourceCode} to {targetCode} at {DateTime.Now}");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Save operation failed : " + ex);
    }
    if (result)
        Console.WriteLine($"Saved latest exchange rate of 1 {sourceCode} to {targetCode} at {DateTime.Now}");
    else
        Console.WriteLine("Exchange rates creation failed - nothing got updated");

}
