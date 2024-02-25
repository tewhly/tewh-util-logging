using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Util.Logging.Testing;
using Xunit;

namespace Util.Logging.Tests.Unit;

public sealed class LoggerExtensionsTests : IDisposable
{
  private readonly IDisposable _onDispose;
  private readonly TestLogs _testLogs;

  private readonly ILogger _logger;

  public LoggerExtensionsTests()
  {
    var factory = TestLoggerProvider.CreateFactory(out var logs);
    _onDispose = factory;
    _testLogs = logs;

    _logger = factory.CreateLogger("TestCategory");
  }

  [Theory]
  [MemberData(nameof(BaseHeaders_data))]
  public void BaseHeaders(BaseHeadersData data)
  {
    var log = _logger.FromEmpty();
    data.Setup(log);
    log.Log();

    var expected = data.Expected;
    _testLogs.Peek().Should().BeEquivalentTo(
      [new TestLog("TestCategory", expected.LogLevel, expected.EventId, null, expected.Exception, string.Empty)],
      x => x.Excluding(x1 => x1.State));
  }

  public static IEnumerable<object[]> BaseHeaders_data()
  {
    yield return
    [
      new BaseHeadersData(
        _ => { },
        new BaseHeadersExpected(default, default, null))
    ];

    var exception = new TestException();
    var eventId = new EventId(1, "TestName");
    yield return
    [
      new BaseHeadersData(
        x => x
          .WithLogLevel(LogLevel.Information)
          .WithEventId(eventId)
          .WithException(exception),

        new BaseHeadersExpected(LogLevel.Information, eventId, exception))
    ];
  }

  void IDisposable.Dispose()
  {
    _onDispose.Dispose();
  }

  public record BaseHeadersData(Action<LogBuilder> Setup, BaseHeadersExpected Expected);

  public record BaseHeadersExpected(LogLevel LogLevel, EventId EventId, Exception? Exception);

  public class TestException : Exception;
}
