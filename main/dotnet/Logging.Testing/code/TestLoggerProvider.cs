using System;
using Microsoft.Extensions.Logging;

namespace Util.Logging.Testing;

public sealed class TestLoggerProvider(TestLogs logs) : ILoggerProvider
{
  public static ILoggerFactory CreateFactory(out TestLogs logs)
  {
    logs = new TestLogs();
    return new LoggerFactory(
      new[]
      {
        new TestLoggerProvider(logs)
      });
  }

  void IDisposable.Dispose()
  {
  }

  ILogger ILoggerProvider.CreateLogger(string categoryName)
  {
    return new TestLogger(categoryName, logs);
  }
}
