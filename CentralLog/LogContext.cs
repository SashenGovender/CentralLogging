using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;


namespace CentralLog
{
  public class LogContext : ILogContext
  {
    private static readonly object _lock = new object();
    private static readonly AsyncLocal<LogContext> _logContext = new AsyncLocal<LogContext>();
    private readonly List<LogRecord> _logList = new List<LogRecord>();

    public static LogContext Context
    {
      get
      {
        lock (_lock)
        {
          if (_logContext.Value == null)
          {
            _logContext.Value = new LogContext();
          }
          return _logContext.Value;
        }
      }
    }

    public void AddLog(LogLevel level, string message, int eventId = 0, Exception except = null)
    {
      _logList.Add(new LogRecord(level, message, eventId, except));
    }

    /// <summary>
    /// Get log context
    /// </summary>
    /// <param name="contextLevel">The level at which the context must be returned. ie If warning, return the full context</param>
    /// <returns></returns>
    public IReadOnlyList<LogRecord> GetLogs(LogLevel contextLevel)
    {
      foreach(var log in _logList)
      {
        if(log.LogLevel == contextLevel)
        {
          return _logList;
        }
      }
      return null;
    }


    /*
     * Nlog Log Levels - https://github.com/NLog/NLog/wiki/Configuration-file#log-levels
     * Level -	Typical Use
     * Fatal -	Something bad happened; application is going down
     * Error -	Something failed; application may or may not continue
     * Warn  -	Something unexpected; application will continue
     * Info  -	Normal behavior like mail sent, user updated profile etc.
     * Debug -	For debugging; executed query, user authenticated, session expired
     * Trace -	For trace debugging; begin method X, end method X
     * 
     * 
     * Microsoft Log Levels - https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-3.1#log-level
     * 
     * None       - 6 - Specifies that a logging category should not write any messages.
     * Critical   - 5 - For failures that require immediate attention. Examples: data loss scenarios, out of disk space.
     * Error      - 4 - For errors and exceptions that cannot be handled. These messages indicate a failure in the current operation or request, not an app-wide failure.
     * Warning    - 3 - For abnormal or unexpected events. Typically includes errors or conditions that don't cause the app to fail.
     * Information- 2 - Tracks the general flow of the app. May have long-term value.
     * Debug      - 1 - For debugging and development. Use with caution in production due to the high volume.
     * Trace      - 0 - Contain the most detailed messages. These messages may contain sensitive app data. These messages are disabled by default and should not be enabled in production.
     * 
     */


  }
}
