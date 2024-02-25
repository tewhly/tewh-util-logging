using System.Collections.Generic;
using System.Collections.Immutable;

namespace Util.Logging.Testing;

public class TestLogs
{
  private ImmutableList<TestLog> _entries = ImmutableList<TestLog>.Empty;

  public IReadOnlyList<TestLog> Peek()
  {
    return _entries;
  }

  public IReadOnlyList<TestLog> Clear()
  {
    return Clear(out _);
  }

  public IReadOnlyList<TestLog> Clear(out IReadOnlyList<TestLog> entries)
  {
    entries = _entries;
    _entries = ImmutableList<TestLog>.Empty;
    return entries;
  }

  public void Add(TestLog testLog)
  {
    _entries = _entries.Add(testLog);
  }
}
