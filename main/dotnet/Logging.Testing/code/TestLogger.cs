using System;
using Microsoft.Extensions.Logging;

namespace Util.Logging.Testing;

public class TestLogger(string categoryName, TestLogs logs) : ILogger
{
  void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
  {
    var logEntry = new TestLog(categoryName, logLevel, eventId, state, exception, formatter(state, exception));
    logs.Add(logEntry);
  }

  bool ILogger.IsEnabled(LogLevel logLevel)
  {
    return true;
  }

  IDisposable ILogger.BeginScope<TState>(TState state)
  {
    return default!;
  }
}
