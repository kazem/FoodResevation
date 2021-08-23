using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Food_Resevation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //Set deafult proxy
            //WebRequest.DefaultWebProxy = new WebProxy("http://127.0.0.1:8118", true) { UseDefaultCredentials = true };
            var logger = LogManager.GetCurrentClassLogger();

            try
            {
                logger.Debug("init main");
                await CreateHostBuilder(args).Build().RunAsync();
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Flush();
                LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging(options => options.ClearProviders())
            .UseNLog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
