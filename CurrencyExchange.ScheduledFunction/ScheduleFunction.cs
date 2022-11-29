using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CurrencyExchange.ScheduledFunction
{
    public class ScheduleFunction
    {
        [FunctionName("ScheduledFunction")]
        // Cron experssion to run every day for once - 0 0 0 * * *
        public void Run([TimerTrigger("*/5 * * * * *")] TimerInfo myTimer, ILogger log)
        {

            try
            {
                Process process = new Process(); // Provides access to local and remote and Process Enables to start and stop system processes.
                                                 //string fileName = Environment.GetEnvironmentVariable(@"FileName");
                string fileName = @"C:\Users\sarat\source\repos\CurrencyExchange\CurrencyExchange.ConsoleApp\bin\Debug\net6.0\CurrencyExchange.ConsoleApp.exe";
                //string fileName1 = @"C:\Users\sarat\source\repos\CurrencyExchange\CurrencyExchange.ConsoleApp\bin\Debug\net6.0\CurrencyExchange.ConsoleApp.exe";
                log.LogInformation(fileName);
                process.StartInfo.FileName = fileName;
                string arguments = Environment.GetEnvironmentVariable("Arguments");
                //string arguments = "SAVE NOK INR";
                process.StartInfo.Arguments = arguments;
                log.LogInformation(arguments);
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                log.LogInformation($"{output}: {DateTime.Now}");
                string err = process.StandardError.ReadToEnd();
                if (err.Length > 0)
                {
                    log.LogError($"errors are \n\t{output}");
                }
                process.WaitForExit();

                log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            }
            catch (Exception ex)
            {
                log.LogError("Scheduler failed");
                throw new Exception(ex.Message);
            }
        }
    }
}
