using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Util.Logging;

public class LogBuilder
{
  private readonly ILogger _logger;

  public LogLevel LogLevel { get; set; }

  public EventId EventId { get; set; }

  public Exception? Exception { get; set; }

  internal LogBuilder(ILogger logger)
  {
    _logger = logger;
  }

  public LogBuilder WithLogLevel(LogLevel value)
  {
    LogLevel = value;

    return this;
  }

  public LogBuilder WithEventId(EventId value)
  {
    EventId = value;

    return this;
  }

  public LogBuilder WithException(Exception? value)
  {
    Exception = value;

    return this;
  }

  public void Log()
  {
    _logger.Log(
      LogLevel,
      EventId,
      Enumerable.Empty<KeyValuePair<string, object?>>(),
      Exception,
      (_, _) => string.Empty);
  }
}
