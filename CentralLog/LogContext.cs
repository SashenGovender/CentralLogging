using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;


namespace CentralLog
{
  public class LogContext
  {
    private static readonly object _lock = new object();
    private static readonly AsyncLocal<LogContext> _logContext = new AsyncLocal<LogContext>();
    private readonly List<LogRecord> _logList = new List<LogRecord>(); 
    private LogContext() { }

    public static LogContext Context
    {
      get
      {
        if (_logContext.Value == null)
        {
          lock (_lock)
          {
            if (_logContext.Value == null)
            {
              _logContext.Value = new LogContext();
            }
          }
        }

        return _logContext.Value;
      }
    }

    public void AddLog(string message)
    {
      AddLog(LogLevel.Information, message, 0);
    }

    public void AddLog(LogLevel level, string message)
    {
      AddLog(level, message, 0);
    }

    public void AddLog(LogLevel level, string message, int eventId)
    {
      _logList.Add(new LogRecord(level, message, eventId));// do i need to new objects, gc?. perhaps just have string messages. would we care about filtering out certian levels?
    }

    public List<LogRecord> GetLogs(LogLevel level)
    {
      switch(level)
      {
        case LogLevel.Error: return _logList;
        case LogLevel.Warning: return _logList.Where(element => element.LogLevel >= level).ToList();
        case LogLevel.Information: return _logList.Where(element => element.LogLevel >= level).ToList();
        case LogLevel.Debug: return _logList.Where(element => element.LogLevel >= level).ToList();
      }
      return null;
    }


    public void WriteToFile(string fullPath)
    {
      using (StreamWriter sw = File.AppendText(fullPath))
      {
        foreach (var log in _logList)
        {
          sw.WriteLine(log.ToString());

        }
      }
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
