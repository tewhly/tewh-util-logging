using System;
using Microsoft.Extensions.Logging;

namespace Util.Logging.Testing;

public record TestLog(
  string CategoryName,
  LogLevel LogLevel,
  EventId EventId,
  object? State,
  Exception? Exception,
  string Message);
