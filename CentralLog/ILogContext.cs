using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CentralLog
{
  public interface ILogContext
  {
    void AddLog(LogLevel level, string message, int eventId, Exception except = null);
    IReadOnlyList<LogRecord> GetLogs(LogLevel level);
  }
}
