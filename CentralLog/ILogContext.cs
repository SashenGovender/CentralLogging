using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CentralLog
{
  public interface ILogContext
  {
    void AddLog(LogLevel level, string message, int eventId);
    IReadOnlyList<LogRecord> GetLogs(LogLevel level);
  }
}
