using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Advent.Common
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Logging
    {
        public static ILogger SetupLogging(Type type)
        {
            return LoggerFactory
                   .Create(builder =>
                       {
                           builder.SetMinimumLevel(LogLevel.Debug).AddFilter("Microsoft", LogLevel.Warning)
                                .AddFilter("System", LogLevel.Warning)
                                .AddFilter("NonHostConsoleApp.Program", LogLevel.Debug).AddDebug()
                                .AddSimpleConsole(
                                    x =>
                                        {
                                            x.ColorBehavior = LoggerColorBehavior.Enabled;
                                            x.SingleLine = true;
                                            x.IncludeScopes = false;
                                        });
                       })
                   .CreateLogger(type);
        }
    }
}
