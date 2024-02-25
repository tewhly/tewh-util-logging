using Microsoft.Extensions.Logging;

namespace Util.Logging;

public static class LoggerExtensions
{
  public static LogBuilder FromEmpty(this ILogger logger)
  {
    return new LogBuilder(logger);
  }
}
