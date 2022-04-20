using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.WindowsServices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
//using Serilog;
//using Serilog.Sinks.RollingFileAlternate;
using System;
using System.Diagnostics;
using System.IO;

namespace Logging.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                //Log.Logger = new LoggerConfiguration()
                //    .MinimumLevel.Debug()
                //    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
                //    .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
                //    .WriteTo.Console()
                //    .WriteTo.RollingFileAlternate(
                //        Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName),
                //        logFilePrefix: "AllianceLogger",
                //        fileSizeLimitBytes: 50000000,
                //        outputTemplate: "{Timestamp:yyyy-MM-dd:mm:ss} [{Level}] {Message}{NewLine}{Exception}"
                //    ).CreateLogger();

                //Log.Information("Current Working Directory: " + Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName));

                CreateHostBuilder(args).Build().Run();
            }
            catch(Exception ex)
            {

            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole(); ;

                    logging.AddEventLog(new EventLogSettings()
                    {
                        LogName = "AllianceLoggerLog",
                        SourceName = "Api"
                    });


                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.UseSerilog();
                    webBuilder.UseStartup<Startup>();
                });
    }
}
